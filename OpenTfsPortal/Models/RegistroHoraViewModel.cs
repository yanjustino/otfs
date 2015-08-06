using System;

namespace OpenTfsPortal.Models
{
    public class RegistroHoraViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double HorasProjetos { get; set; }
        public double HorasIncidente { get; set; }
        public double HorasTotais { get; set; }
        public double HoraPonto { get; set; }
    }
}