using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class EstadoViewModel
    {

        [Key]
        public Guid Id { get; set; }
        public string Uf { get; set; }
    }
}
