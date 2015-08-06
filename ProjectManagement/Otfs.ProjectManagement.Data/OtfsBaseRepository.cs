using Common.Data;
using Common.Domain.Model;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using Otfs.ProjectManagement.Core.Domain.Model.Project;

namespace Otfs.ProjectManagement.Data
{
    public abstract class OtfsBaseRepository
    {
        protected TeamFoundationDataContext Context { get; private set; }

        protected OtfsBaseRepository() { }

        public OtfsBaseRepository(IOtfsContainer container)
        {
            Context = container.Resolver<TeamFoundationDataContext>();
        }

        protected ProjectCollection GetProjectCollection()
        {
            var workItemStore = Context.TeamProjectCollection.GetService<WorkItemStore>();
            return workItemStore.Projects;
        }

        protected Project GetProject(int id)
        {
            return GetProjectCollection().GetById(id);
        }

        protected OtfsIteration GenerateFromNode(OtfsProjectId otfsProjectId, Node node)
        {
            var infoNodes = GetNodeInfo(node);
            return new OtfsIteration(otfsProjectId, node.Name, node.Path, infoNodes.StartDate, infoNodes.FinishDate);
        }

        protected NodeInfo GetNodeInfo(Node node)
        {
            var service = Context.TeamProjectCollection.GetService<ICommonStructureService4>();
            var projectNameIndex = node.Path.IndexOf("\\", 2);
            var fullPath = node.Path.Insert(projectNameIndex, "\\Iteration");
            return service.GetNodeFromPath(fullPath);
        }

        protected string UsuarioAutenticado()
        {
            return this.Context.TeamProjectCollection.AuthorizedIdentity.DisplayName;
        }
    }
}
