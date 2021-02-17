using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class AtividadeViewModel
    {
        public AtividadeViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Descricao { get;  set; }
        public string Nome { get;  set; }
        public DateTime DataHoraInicio { get;  set; }
        public DateTime DataHoraFim { get;  set; }
        public Guid? FuncionarioId { get;  set; }
        public Guid? EmpresaId { get;  set; } 
    }
}
