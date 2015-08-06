using System;

namespace Otfs.GestaoHoras.Core.Domain.Model
{
    public class HistoricoHora
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public double HorasTrabalhadas { get; set; }
        public double HorasIncidente { get; set; }
        public double HorasProjetos { get; set; }
        public DateTime DataPonto { get; set; }
    }
}