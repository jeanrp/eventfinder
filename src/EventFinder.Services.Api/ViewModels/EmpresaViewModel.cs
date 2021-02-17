using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class EmpresaViewModel 
    {
        public EmpresaViewModel()
        {
            Id = Guid.NewGuid();
            Endereco = new EnderecoViewModel();
            Usuario = new UsuarioViewModel();
        }

        [Key]
        public Guid Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Facebook { get; set; }
        public string Senha { get; set; }
        public string Cnpj { get; set; }

        public Guid? EnderecoId { get; set; }
        public virtual EnderecoViewModel Endereco { get; set; }

        public Guid UsuarioId { get; set; }
        public virtual UsuarioViewModel Usuario { get; set; }


    }
}
