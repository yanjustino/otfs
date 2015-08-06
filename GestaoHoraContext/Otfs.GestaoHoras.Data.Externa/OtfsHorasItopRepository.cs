using Otfs.GestaoHoras.Core.Aplication;
using System;
using System.Collections.Generic;

namespace Otfs.GestaoHoras.Data.Externa
{
    public class OtfsHorasItopRepository : IOtfsHorasTrabalhadasRepository
    {
        public Dictionary<DateTime, double> ListarHorasMes(Guid userId, int mes)
        {
            DateTime dataInicial = DateTime.Parse("01/03/2015");
            Dictionary<DateTime, double> horasPonto = new Dictionary<DateTime, double>();
            horasPonto.Add(dataInicial.AddDays(3), 4);
            horasPonto.Add(dataInicial.AddDays(5), 3);
            horasPonto.Add(dataInicial.AddDays(9), 2);
            horasPonto.Add(dataInicial.AddDays(7), 1);
            horasPonto.Add(dataInicial.AddDays(15), 6);
            horasPonto.Add(dataInicial.AddDays(10), 7);
            horasPonto.Add(dataInicial.AddDays(11), 5);
            return horasPonto;
        }
    }
}
