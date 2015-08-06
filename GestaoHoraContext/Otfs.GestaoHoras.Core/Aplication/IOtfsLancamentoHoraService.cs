using Otfs.GestaoHoras.Core.Domain.Model;
using System.Collections.Generic;

namespace Otfs.GestaoHoras.Core.Aplication
{
    public interface IOtfsLancamentoHoraService
    {
        List<LancamentoHora> ListarHoras(int taskId);
        
        LancamentoHora BuscarHoraPorId(int id);

        void AtualizarHora(LancamentoHora model);
        void ExcluirHora(int id);
        void InserirLancamentoHora(LancamentoHora historico);
    }
}
