using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class PublicacaoViewModel
    {
        public PublicacaoViewModel()
        {

        }
        
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public byte[] Anexo { get; set; }
        public Guid ClienteId { get; set; }
        public Guid EventoId { get; set; }


    }
}
