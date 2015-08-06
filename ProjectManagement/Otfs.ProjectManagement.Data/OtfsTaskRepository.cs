using Common.Domain.Model;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using Otfs.ProjectManagement.Core.Domain.Model.Task;
using Otfs.ProjectManagement.Data.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.ProjectManagement.Data
{
    public class OtfsTaskRepository : OtfsBaseRepository, IOtfsTaskRepository
    {
        public OtfsTaskRepository(IOtfsContainer context) : base(context) { }

        public IEnumerable<OtfsTask> BuscarTarefasDesenvolvedor()
        {
            string query = "Select * " +
                           "From WorkItems " +
                           "Where ( [Work Item Type] = @Type01 || " + 
                           "[Work Item Type] = @Type02 ) and [Assigned To] = @User";


            var param = new Dictionary<string, string>();
            param.Add("User", this.UsuarioAutenticado());
            param.Add("Type01", "Task");
            param.Add("Type02", "Bug");

            return Execute(query, param);
        }

        private IEnumerable<OtfsTask> Execute(string query, Dictionary<string, string> param)
        {
            var workItem = Context.TeamProjectCollection.GetService<WorkItemStore>();
            var result = new Query(workItem, query, param).RunQuery();

            return IterarWorkItemsCollection(result).ToList();
        }

        private IEnumerable<OtfsTask> IterarWorkItemsCollection(WorkItemCollection collection, OtfsIteration iteration = null)
        {
            foreach (WorkItem item in collection)
            {
                var task = WorkItenFactory<OtfsTask>.Build(item);
                task.Comitar(iteration);

                yield return task;
            }
        }
    }
}
