using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class FuncionarioViewModel
    {
        public FuncionarioViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Facebook { get;  set; }
        public string Cargo { get; set; }
        public string Sexo { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid EquipeId { get; set; }
    }
}
