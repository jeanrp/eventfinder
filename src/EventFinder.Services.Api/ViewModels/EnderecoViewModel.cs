using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class EnderecoViewModel
    {

        public EnderecoViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public Guid CidadeId { get; set; }
        public CidadeViewModel Cidade { get; set; }
        public Guid? EventoId { get; set; }
        public string EnderecoFormatado { get; set; }
        public Guid? EmpresaId { get; set; }
    }
}
