using Otfs.GestaoHoras.Core.Aplication;
using System;
using System.Collections.Generic;

namespace Otfs.GestaoHoras.Data.Externa
{
    public class OtfsHorasPontoRepository : IOtfsHorasTrabalhadasRepository
    {
        public Dictionary<DateTime, double> ListarHorasMes(Guid userId, int mes)
        {   
            DateTime dataInicial = DateTime.Parse("01/03/2015");
            Dictionary<DateTime,double> horasPonto = new Dictionary<DateTime,double>();
            horasPonto.Add(dataInicial.AddDays(1),8);
            horasPonto.Add(dataInicial.AddDays(2),7);
            horasPonto.Add(dataInicial.AddDays(3),6);
            horasPonto.Add(dataInicial.AddDays(4),5);
            horasPonto.Add(dataInicial.AddDays(5),8);
            horasPonto.Add(dataInicial.AddDays(6),7);
            horasPonto.Add(dataInicial.AddDays(7),8);
            return horasPonto;
        }
    }
}
