using System;

namespace EventFinder.Services.Api.ViewModels
{
    public class MensagemOrganizacaoEventoViewModel
    {        
        public string Descricao { get; set; }
        public byte[] Anexo { get; set; } 
        public Guid ClienteId { get; set; } 
        public Guid EmpresaId { get; set; } 
    }
}
