using EventFinder.Domain.Eventos;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class PromocaoViewModel
    {

        public PromocaoViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public string Situacao { get; set; }

        public string NomeVencedor { get; set; }

        public Guid EventoId { get; set; } 

    }
}
