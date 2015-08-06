using Common.Domain.Model;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.ProjectManagement.Data
{
    public class OtfsIterationRepository : OtfsBaseRepository, IOtfsIterationRepository
    {
        public OtfsIterationRepository(IOtfsContainer context) : base(context) { }

        public IEnumerable<OtfsIteration> Agendados(OtfsProjectId otfsProjectId)
        {
            var project = GetProject(otfsProjectId.Id);
            var iterations = new List<OtfsIteration>();

            foreach (Node parentNode in project.IterationRootNodes)
                AdicionarIteracoes(otfsProjectId, iterations, parentNode);

            return iterations;
        }

        private void AdicionarIteracoes(OtfsProjectId otfsProjectId, List<OtfsIteration> iterations, Node parentNode)
        {
            iterations.Add(GenerateFromNode(otfsProjectId, parentNode));
            foreach (Node childNode in parentNode.ChildNodes)
                AdicionarIteracoes(otfsProjectId, iterations, parentNode);
        }

        public OtfsIteration PorIteracao(OtfsProjectId otfsProjectId, string path)
        {
            return Agendados(otfsProjectId)
                .Where(x => x.Path == path)
                .FirstOrDefault();
        }

        public IEnumerable<OtfsIteration> Correntes()
        {
            var projects = this.GetProjectCollection();

            for (int i = 0; i < projects.Count; i++)
            {
                var iterations = Agendados(new OtfsProjectId(projects[i].Id))
                                 .Where(OtfsIteration.IteracaoCorrente);

                foreach (var iteration in iterations)
                    yield return iteration;
            }
        }
    }
}
