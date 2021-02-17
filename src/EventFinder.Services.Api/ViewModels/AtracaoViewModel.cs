using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class AtracaoViewModel
    {
        public AtracaoViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid  Id { get; set; }

        public string Nome { get; set; }

        public string Estilo { get; set; }

        public Guid EventoId { get; set; }
    }
}
