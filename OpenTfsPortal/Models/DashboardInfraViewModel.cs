using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System.Collections.Generic;
using System.Linq;

namespace OpenTfsPortal.Models
{
    public class DashboardInfraViewModel
    {
        public Dictionary<OtfsProject, int> ProjetosParaCompilacao { get; set; }

        public DashboardInfraViewModel(IEnumerable<OtfsProject> projetos, IEnumerable<OtfsBacklogItem> itensBacklog)
        {
            var projetosItens = from projeto in projetos
                                where itensBacklog.Select(item => item.ProjectId.Id).Contains(projeto.ProjectId.Id)
                                select projeto;


            ProjetosParaCompilacao = projetosItens.ToDictionary(
                                        k => k, 
                                        v => itensBacklog.Count(x => x.ProjectId.Id == v.ProjectId.Id));

        }
    }
} 