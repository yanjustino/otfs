using System;
using System.Collections.Generic;

namespace Otfs.GestaoHoras.Core.Aplication
{
    public class OtfsProdutividadeService<T> : IOtfsProdutividadeService<T> where T: IOtfsHorasTrabalhadasRepository
    {
        private IOtfsHorasTrabalhadasRepository _otfsHorasTrabalhasdasRepository;

        public OtfsProdutividadeService(T otfsHorasTrabalhasdasRepository)
        {
            _otfsHorasTrabalhasdasRepository = otfsHorasTrabalhasdasRepository;
        }
        
        public Dictionary<DateTime, double> ListarHorasMes(Guid userId, int mes)
        {
            return _otfsHorasTrabalhasdasRepository.ListarHorasMes(userId, mes);
        }
    }
}