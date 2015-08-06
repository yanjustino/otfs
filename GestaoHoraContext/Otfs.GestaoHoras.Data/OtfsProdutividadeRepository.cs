using Otfs.GestaoHoras.Core.Aplication;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;

namespace Otfs.GestaoHoras.Data
{
    public class OtfsProdutividadeRepository : IOtfsHorasTrabalhadasRepository
    {
        private DataContextGestaoHora _dbContext;

        public OtfsProdutividadeRepository(DataContextGestaoHora dbContext)
        {
            _dbContext = dbContext;
        }

        public Dictionary<DateTime, double> ListarHorasMes(Guid userId, int mes)
        {
            DateTime dataInicial = new DateTime(DateTime.Today.Year, mes, 1);
            DateTime dataFinal = dataInicial.UltimoDiaDoMes();

            var g = from d in _dbContext.LancamentoHora
                    where d.UserId == userId &&
                          (d.DataLancamento >= dataInicial && d.DataLancamento <= dataFinal)
                    group d by d.DataLancamento into z
                    select new
                    {
                        data = z.Key,
                        total = z.Sum(w => w.HoraTrabalhada)
                    };
            
            return g.ToDictionary(t => t.data, t => t.total);
        }
    }

}