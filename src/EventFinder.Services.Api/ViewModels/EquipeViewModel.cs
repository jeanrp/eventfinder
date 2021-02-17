using EventFinder.Domain.Empresas;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class EquipeViewModel
    {
        public EquipeViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        public string Nome { get;  set; }
        public string Descricao { get;  set; }
        public Guid EmpresaId { get;  set; }
    }
}


 
