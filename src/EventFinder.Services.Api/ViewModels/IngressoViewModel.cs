using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class IngressoViewModel
    {
        public IngressoViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; }
        public short Lote { get; set; }
        public Guid EventoId { get; set; }
    }
}
