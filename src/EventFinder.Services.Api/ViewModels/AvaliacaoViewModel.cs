using EventFinder.Domain.Clientes;
using EventFinder.Domain.Eventos;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class AvaliacaoViewModel
    {
        public AvaliacaoViewModel()
        {
            Id = Guid.NewGuid();            
        }
        [Key]
        public Guid Id { get; set; }
        public decimal Nota { get; set; }
        public string Descricao { get; set; }        
        public Guid EventoId { get; set; }
        public Guid ClienteId { get; set; }         
    }
}
