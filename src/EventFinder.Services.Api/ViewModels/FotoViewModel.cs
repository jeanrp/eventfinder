using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class FotoViewModel
    {

        public FotoViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public byte[] Imagem { get; set; }
        public Guid? EventoId { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid? EmpresaId { get; set; }
        public string ImagemBase64 { get; set; }
    }
}
