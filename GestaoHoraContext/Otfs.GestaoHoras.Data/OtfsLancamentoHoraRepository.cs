using Otfs.GestaoHoras.Core.Domain.Model;
using Otfs.GestaoHoras.Core.Domain.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.GestaoHoras.Data
{
    public class OtfsLancamentoHoraRepository : IOtfsLancamentoHoraRepository
    {
        private DataContextGestaoHora _dbContext;

        public OtfsLancamentoHoraRepository(DataContextGestaoHora dbContext)
        {
            _dbContext = dbContext;
        }

        public void Inserir(LancamentoHora model)
        {
            _dbContext.LancamentoHora.Add(model);
            _dbContext.SaveChanges();
        }

        public List<LancamentoHora> Listar(int taskId)
        {
            return _dbContext.LancamentoHora.Where(dado => dado.TaskId == taskId).ToList();
        }

        public void Excluir(int id)
        {
            LancamentoHora lancamentoHora = _dbContext.LancamentoHora.Where(dado => dado.Id == id).FirstOrDefault();
            _dbContext.LancamentoHora.Remove(lancamentoHora);
            _dbContext.SaveChanges();
        }

        public void Atualizar(LancamentoHora model)
        {
            _dbContext.Entry<LancamentoHora>(model).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public LancamentoHora BucarPorId(int id)
        {
            return _dbContext.LancamentoHora.Find(id);
        }
    }
}
