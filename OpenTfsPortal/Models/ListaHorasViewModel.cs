using Otfs.GestaoHoras.Core.Domain.Model;
using System.Collections.Generic;

namespace OpenTfsPortal.Models
{
    public class ListaHorasViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public List<LancamentoHora> HorasLancadas { get; set; }

    }
}