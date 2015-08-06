using System;
using System.Collections.Generic;

namespace Otfs.GestaoHoras.Core.Aplication
{
    public interface IOtfsProdutividadeService<T> where T : IOtfsHorasTrabalhadasRepository
    {
         Dictionary<DateTime,double> ListarHorasMes(Guid userId, int mes);
    }
}
