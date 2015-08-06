using System;
using System.Collections.Generic;

namespace Otfs.GestaoHoras.Core.Aplication
{
    public interface IOtfsHorasTrabalhadasRepository
    {
        Dictionary<DateTime, double> ListarHorasMes(Guid userId, int mes);
    }
}
