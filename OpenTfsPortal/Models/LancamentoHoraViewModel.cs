using System;
using System.ComponentModel.DataAnnotations;

namespace OpenTfsPortal.Models
{
    public class LancamentoHoraViewModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }

        [Required(ErrorMessage="Informe a hora trabalhada")]
        [Display(Name = "Horas Trabalhadas")]
        [DataType(DataType.Currency)]
        public double HoraTrabalhada { get; set; }

        [DataType(DataType.MultilineText)]
        public string Historico { get; set; }

        [Required(ErrorMessage = "Informe a data de lançamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }

        public DateTime Data { get; set; }
    }
}