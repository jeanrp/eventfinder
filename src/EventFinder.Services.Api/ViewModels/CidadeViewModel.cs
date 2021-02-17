using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class CidadeViewModel
    {

        public CidadeViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Nome { get;  set; }
        public Guid EstadoId { get;  set; }
    }
}
