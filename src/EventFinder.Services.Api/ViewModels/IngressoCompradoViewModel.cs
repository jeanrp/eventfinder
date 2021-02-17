using System;

namespace EventFinder.Services.Api.ViewModels
{
    public class IngressoCompradoViewModel
    {                 
        public int Quantidade { get; set; }
        public string NomeEvento { get; set; }
        public double ValorTotal { get; set; }
        public Guid ClienteId { get; set; }
        public Guid IngressoId { get; set; }
    }
}
