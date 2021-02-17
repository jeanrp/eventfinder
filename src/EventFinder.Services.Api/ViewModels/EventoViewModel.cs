using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class EventoViewModel
    {
        public EventoViewModel()
        {
            this.Id = Guid.NewGuid();
            this.Endereco = new EnderecoViewModel();
            this.Ingressos = new List<IngressoViewModel>();
            this.Atracoes = new List<AtracaoViewModel>();
            this.Fotos = new List<FotoViewModel>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string SubDescricao { get; set; }
        public string DescPatrocinadores { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Situacao { get;  set; }
        public Guid CategoriaId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid EnderecoId { get; set; }
        public virtual EnderecoViewModel Endereco { get; set; }
        public virtual IEnumerable<IngressoViewModel> Ingressos { get; set; }
        public virtual IEnumerable<AtracaoViewModel> Atracoes { get; set; }
        public virtual IEnumerable<FotoViewModel> Fotos { get; set; }

    }

}
