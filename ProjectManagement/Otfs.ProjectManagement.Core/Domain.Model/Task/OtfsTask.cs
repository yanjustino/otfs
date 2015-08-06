
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
namespace Otfs.ProjectManagement.Core.Domain.Model.Task
{
    public class OtfsTask
    {
        public OtfsTaskId TaskId { get; private set; }
        public OtfsProjectId ProjectId { get; private set; }
        public OtfsIteration Iteration { get; private set; }
        public string AssignedTo { get; private set; }
        public string Title { get; private set; }
        public string NomeProjeto { get; set; }
        public string Status { get; set; }

        public OtfsTask(OtfsProjectId projectId, OtfsTaskId taskId, string title)
        {
            this.Title = title;
            this.TaskId = taskId;
            this.ProjectId = projectId;
        }

        public void AssinarResponsavel(string name)
        {
            this.AssignedTo = name;
        }

        public void Comitar(OtfsIteration iteration)
        {
            this.Iteration = iteration;
        }
    }
}
