using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenTfsPortal.Models
{
    public class DashboardDesenvolvedorViewModel
    {
        public Guid UserId { get; set; }
        public string Desenvolvedor { get; set; }
        public IEnumerable<OtfsTask> Tarefas { get; set; }

        public Dictionary<string, List<OtfsTask>> AgruparTarefasPorProjeto()
        {
            var tasks = from t in Tarefas
                        group t by t.NomeProjeto into g
                        select new { projetos = g.Key, tarefas = g.ToList() };

            return tasks.ToDictionary(x => x.projetos, x => x.tarefas);
        }
    }
}