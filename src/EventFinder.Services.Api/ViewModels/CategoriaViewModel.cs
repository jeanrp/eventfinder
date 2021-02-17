using System;
using System.ComponentModel.DataAnnotations;

namespace EventFinder.Services.Api.ViewModels
{
    public class CategoriaViewModel
    {

        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public short Classificacao { get; set; }
    }
}
