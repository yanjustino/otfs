using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using Otfs.ProjectManagement.Core.Domain.Model.Task;

namespace Otfs.ProjectManagement.Data.Factory
{
    internal static class WorkItenFactory<T> where T : class
    {
        public static T Build(WorkItem workItem)
        {

            if (typeof(T) == typeof(OtfsBacklogItem))
            {
                var backlogItem = BuildBackLogItem(workItem);
                return backlogItem as T;
            }

            if (typeof(T) == typeof(OtfsTask))
            {
                var task = BuildTask(workItem);
                return task as T;
            }


            return null;
        }

        private static OtfsBacklogItem BuildBackLogItem(WorkItem workItem)
        {

            OtfsBacklogItemState state = OtfsBacklogItemState.New;

            switch (workItem.State)
            {
                case "New": break;
                case "Approved": state = OtfsBacklogItemState.Approved; break;
                case "Committed": state = OtfsBacklogItemState.Committed; break;
                case "Done": state = OtfsBacklogItemState.Done; break;
                default: break;
            }


            var backlogItem = new OtfsBacklogItem
                (
                    new OtfsProjectId(workItem.Project.Id),
                    new OtfsBacklogItemId(workItem.Id),
                    workItem.Title,
                    workItem["Assigned To"].ToString(),
                    workItem.ChangedDate,
                    state
                );



            return backlogItem;
        }

        private static OtfsTask BuildTask(WorkItem workItem)
        {
            var task = new OtfsTask
                (
                    new OtfsProjectId(workItem.Project.Id),
                    new OtfsTaskId(workItem.Id),
                    workItem.Title
                );

            task.AssinarResponsavel(workItem["Assigned To"].ToString());
            task.NomeProjeto = workItem.AreaPath;
            task.Status = workItem.State;
            return task;
        }
    }
}
