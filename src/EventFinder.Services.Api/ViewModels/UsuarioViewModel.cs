using System;

namespace EventFinder.Services.Api.ViewModels
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            Id = Guid.NewGuid(); 
        }

        public Guid Id { get; set; }

        public string NomeRazaoSocial { get; set; }

        public DateTime DataHoraCadastro { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
