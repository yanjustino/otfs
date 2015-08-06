using Common.Domain.Model;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.ProjectManagement.Data
{
    public class OtfsProjectRepository : OtfsBaseRepository, IOtfsProjectRepository
    {
        private OtfsBacklogItemRepository backlogItemRepository;

        public OtfsProjectRepository(IOtfsContainer container)
            : base(container)
        {
            this.backlogItemRepository = new OtfsBacklogItemRepository(container);
        }

        public IEnumerable<OtfsProject> Todos()
        {
            foreach (Project project in GetProjectCollection())
                yield return BuildProjectWithSprints(project);
        }

        public OtfsProject Localizar(OtfsProjectId otfsProjectId)
        {
            var project = GetProject(otfsProjectId.Id);

            return project != null ? BuildProjectWithSprints(project) : null;
        }

        private OtfsProject BuildProjectWithSprints(Project project)
        {
            var otfsProject = new OtfsProject(new OtfsProjectId(project.Id), project.Name);
            ScheduleSprintOnProject(project, otfsProject);

            return otfsProject;
        }

        private void ScheduleSprintOnProject(Project project, OtfsProject otfsProject)
        {
            foreach (Node parentNode in project.IterationRootNodes)
                AddIterationNode(otfsProject, parentNode);
        }

        private void AddIterationNode(OtfsProject otfsProject, Node parentNode)
        {
            if (parentNode == null) return;

            var iteration = GenerateFromNode(otfsProject.ProjectId, parentNode);
            var metrics = backlogItemRepository.GerarMetricasDaIteracao(iteration);

            iteration.InformarMetricas(metrics);
            otfsProject.AgendarIteracao(iteration);
            
            foreach (Node childNode in parentNode.ChildNodes)
                AddIterationNode(otfsProject, childNode);
        }

        public IEnumerable<OtfsProject> ProjetosUsuario(Guid userId)
        {
            var servico = Context.TeamProjectCollection.GetService<IGroupSecurityService>();

            foreach (Project item in GetProjectCollection())
            {
                List<Identity> membros = RecuperarMembros(servico, item);
                var usuario = membros.Where(x => x.TeamFoundationId == userId).FirstOrDefault();

                if (usuario != null)
                {
                    var project = BuildProjectWithSprints(item);
                    if (project.PossuiIteracaoCorrente()) yield return project;
                }
            }
        }

        private static List<Identity> RecuperarMembros(IGroupSecurityService servico, Project item)
        {
            var grupos = servico.ListApplicationGroups(item.Uri.ToString());
            var contribuitors = grupos.FirstOrDefault(o => o.AccountName.Contains("Contributors"));
            var sidsMembers = servico.ReadIdentity(SearchFactor.Sid, contribuitors.Sid, QueryMembership.Expanded);

            return servico.ReadIdentities(SearchFactor.Sid, sidsMembers.Members, QueryMembership.Expanded).ToList();
        }
    }
}
