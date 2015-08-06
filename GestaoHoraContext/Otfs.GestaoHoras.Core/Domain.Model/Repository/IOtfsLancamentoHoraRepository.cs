using System.Collections.Generic;

namespace Otfs.GestaoHoras.Core.Domain.Model.Repository
{
    public interface IOtfsLancamentoHoraRepository
    {
        List<LancamentoHora> Listar(int taskId);

        LancamentoHora BucarPorId(int id);
        
        void Inserir(LancamentoHora historico);
        void Atualizar(LancamentoHora model);
        void Excluir(int id);
    }
}
