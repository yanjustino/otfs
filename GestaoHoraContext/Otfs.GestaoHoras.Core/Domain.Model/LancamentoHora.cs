using System;

namespace Otfs.GestaoHoras.Core.Domain.Model
{
    public class LancamentoHora
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Guid UserId { get; set; }
        public double HoraTrabalhada { get; set; }
        public string  Historico { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime Data { get; set; }
    }
}