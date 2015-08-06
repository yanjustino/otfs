using Otfs.GestaoHoras.Core.Domain.Model;
using Otfs.GestaoHoras.Core.Domain.Model.Repository;
using System.Collections.Generic;

namespace Otfs.GestaoHoras.Core.Aplication
{
    public class OtfsLancamentoHoraService : IOtfsLancamentoHoraService
    {
        private IOtfsLancamentoHoraRepository _otfsLancamentoHoraRepository;


        public OtfsLancamentoHoraService(IOtfsLancamentoHoraRepository otfsLancamentoHoraRepository)
        {
            _otfsLancamentoHoraRepository = otfsLancamentoHoraRepository;
        }

        public void InserirLancamentoHora(LancamentoHora lancamentoHora)
        {
            _otfsLancamentoHoraRepository.Inserir(lancamentoHora);
        }

        public void AtualizarHora(LancamentoHora model)
        {
            _otfsLancamentoHoraRepository.Atualizar(model);
        }

        public void ExcluirHora(int id)
        {
            _otfsLancamentoHoraRepository.Excluir(id);
        }

        public LancamentoHora BuscarHoraPorId(int id)
        {
            return _otfsLancamentoHoraRepository.BucarPorId(id);
        }

        public List<LancamentoHora> ListarHoras(int taskId)
        {
            return _otfsLancamentoHoraRepository.Listar(taskId);
        }
    }
}
