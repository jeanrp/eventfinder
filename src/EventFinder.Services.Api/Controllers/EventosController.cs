using AutoMapper;
using EventFinder.Domain.Clientes.Commands;
using EventFinder.Domain.Core.Notifications;
using EventFinder.Domain.Empresas.Repository;
using EventFinder.Domain.Eventos;
using EventFinder.Domain.Eventos.Commands;
using EventFinder.Domain.Eventos.Repository;
using EventFinder.Domain.Interfaces;
using EventFinder.Infra.CrossCutting.IoC.Security;
using EventFinder.Services.Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EventFinder.Services.Api.Controllers
{
    public class EventosController : BaseController
    {

        private readonly IEventoRepository _eventoRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public EventosController(
            INotificationHandler<DomainNotification> notifications,
            IMapper mapper,
            IUser user,
            IEventoRepository eventoRepository,
            IEmpresaRepository empresaRepository,
            IMediatorHandler mediator) : base(notifications, user, mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _eventoRepository = eventoRepository;
            _empresaRepository = empresaRepository;
        }


        [HttpGet]
        [Route("eventos/estados")]
        public IEnumerable<EstadoViewModel> ObterEstados()
        {
            return _mapper.Map<IEnumerable<EstadoViewModel>>(_eventoRepository.ObterEstados());
        }

        [HttpGet]
        [Route("eventos/categorias")]
        public IEnumerable<CategoriaViewModel> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(_eventoRepository.ObterCategorias());
        }

        [HttpGet]
        [Route("eventos/meus-eventos")]
        public IEnumerable<EventoViewModel> ObterMeusEventos()
        {
            var eventos = _empresaRepository.BuscarEventosPorEmpresa(UsuarioLogadoId);
            foreach (var item in eventos)
            {
                foreach (var foto in item.Fotos) foto.AtribuirImagemBase64();
                foreach (var atracaoEvento in item.AtracoesEventos) {
                    atracaoEvento.Atracao.AtribuirAtracaoId(atracaoEvento.AtracaoId);
                    item.Atracoes.Add(atracaoEvento.Atracao);
                }
            }

            eventos = eventos.ToList().GroupBy(x => x.Id).Select(group => group.First());
            foreach (var item in eventos)
            {
                item.Atracoes = item.Atracoes.GroupBy(x => x.Id).Select(group => group.First()).ToList();
                item.DistinctFotos(item.Fotos.GroupBy(x => x.Id).Select(group => group.First()).ToList());
                item.DistinctIngresso(item.Ingressos.GroupBy(x => x.Id).Select(group => group.First()).ToList());
            }

            IEnumerable<EventoViewModel> eventosVM = _mapper.Map<IEnumerable<EventoViewModel>>(eventos);

            return eventosVM;
        }

        [HttpPost]
        [Route("fotos")]
        public IActionResult Post(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                _mediator.PublicarEvento(new DomainNotification("Erro", "Imagem com formato incorreto"));
                return Response();
            }
            FotoViewModel fotoViewModel = new FotoViewModel
            {
                Id = Guid.NewGuid()
            };

            using (Stream stream = image.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)image.Length);
                    fotoViewModel.Imagem = fileContent;
                }
            }

            var fotoCommand = _mapper.Map<AdicionarFotoCommand>(fotoViewModel);
            _mediator.EnviarComando(fotoCommand);
            fotoCommand.AtribuirImagem(fotoCommand.Imagem);
            return Response(fotoCommand);
        }


        [HttpDelete]
        [Authorize]
        [Route("fotos/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var fotoViewModel = new FotoViewModel { Id = id };
            var fotoCommand = _mapper.Map<ExcluirFotoCommand>(fotoViewModel);

            _mediator.EnviarComando(fotoCommand);
            return Response(fotoCommand);
        }


        [HttpGet]
        [Route("eventos")]
        public IEnumerable<EventoViewModel> ObterEventos()
        {
            var eventos = _eventoRepository.ObterTodos();

            foreach (var item in eventos)
            {
                foreach (var foto in item.Fotos) foto.AtribuirImagemBase64();
                foreach (var atracaoEvento in item.AtracoesEventos)
                {
                    atracaoEvento.Atracao.AtribuirAtracaoId(atracaoEvento.AtracaoId);
                    item.Atracoes.Add(atracaoEvento.Atracao);
                }               
            }

            eventos = eventos.ToList().GroupBy(x => x.Id).Select(group => group.First());
            foreach (var item in eventos)
            {
                item.Atracoes = item.Atracoes.GroupBy(x => x.Id).Select(group => group.First()).ToList();
                item.DistinctFotos(item.Fotos.GroupBy(x => x.Id).Select(group => group.First()).ToList());
                item.DistinctIngresso(item.Ingressos.GroupBy(x => x.Id).Select(group => group.First()).ToList());
            }





            return _mapper.Map<IEnumerable<EventoViewModel>>(eventos);
        }


        [HttpPost]
        [Route("eventos")]
        public IActionResult Post([FromBody]EventoViewModel eventoViewModel)
        {
            if (!ModelStateValida())
            {
                return Response();
            }


            var eventoCommand = _mapper.Map<IncluirEventoCommand>(eventoViewModel);

            _mediator.EnviarComando(eventoCommand);
            return Response(eventoCommand);
        }


        private bool ModelStateValida()
        {
            if (ModelState.IsValid) return true;

            NotificarErroModelInvalida();
            return false;
        }
    }
}
