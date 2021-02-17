using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            this.Id = Guid.NewGuid();
            Usuario = new UsuarioViewModel();
        }

        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get;  set; }
        public string Facebook { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string AtracaoPreferida { get; set; }
        public string EstiloPreferido { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual UsuarioViewModel Usuario { get; set; }
    }
}
