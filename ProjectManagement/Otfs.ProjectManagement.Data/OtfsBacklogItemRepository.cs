using Common.Domain.Model;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using Otfs.ProjectManagement.Data.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.ProjectManagement.Data
{
    public class OtfsBacklogItemRepository : OtfsBaseRepository, IOtfsBacklogItemRepository
    {
        public OtfsBacklogItemRepository(IOtfsContainer context) : base(context) { }


        public IEnumerable<OtfsBacklogItem> LocalizarPorStatus(OtfsBacklogItemState state, OtfsIteration iteration = null)
        {
            return QueryOtfsBacklogItems(state, iteration);
        }

        public IEnumerable<OtfsBacklogItem> ItensParaBuild(OtfsIteration iteration = null)
        {
            var backlogItens = QueryIterationByStatus(OtfsBacklogItemState.Committed, iteration);
            return backlogItens.Where(OtfsBacklogItem.QueryBacklogItemParaBuild);
        }

        public IEnumerable<OtfsBacklogItem> ItensEmAndamento(OtfsIteration[] iterations)
        {
            var backlogItems = new List<OtfsBacklogItem>();

            foreach (var iteration in iterations)
            {
                var queryBacklogItems = QueryOtfsBacklogItems(OtfsBacklogItemState.Approved, iteration);
                backlogItems.InsertRange(0, queryBacklogItems);
            }

            return backlogItems;
        }

        public IEnumerable<OtfsBacklogItem> ItensParaBuild(OtfsIteration[] iterations)
        {
            var backlogItems = new List<OtfsBacklogItem>();

            foreach (var iteration in iterations)
            {
                var queryBacklogItems = QueryIterationByStatus(OtfsBacklogItemState.Committed, iteration);
                var itens = queryBacklogItems.Where(OtfsBacklogItem.QueryBacklogItemParaBuild);

                backlogItems.InsertRange(0, itens);
            }

            return backlogItems;
        }

        public IEnumerable<OtfsBacklogItem> ItensConcluidos(OtfsIteration[] iterations)
        {
            var backlogItems = new List<OtfsBacklogItem>();

            foreach (var iteration in iterations)
            {
                var queryBacklogItems = QueryOtfsBacklogItems(OtfsBacklogItemState.Done, iteration, true);
                backlogItems.InsertRange(0, queryBacklogItems);
            }

            return backlogItems;
        }

        private IEnumerable<OtfsBacklogItem> QueryIterationByStatus(OtfsBacklogItemState state, OtfsIteration iteration)
        {
            string query = "Select * " +
                           "From WorkItems " +
                           "Where [Work Item Type] = @Type and " +
                           "      [State] = @State and " + 
                           "      [Iteration Path] under @Path";

            var param = new Dictionary<string, string>();
            param.Add("State", state.ToString());
            param.Add("Type", "Product Backlog Item");
            param.Add("Path", iteration.Path);

            return Execute(query, param, iteration);
        }

        private IEnumerable<OtfsBacklogItem> QueryOtfsBacklogItems(OtfsBacklogItemState state, OtfsIteration iteration = null, bool all = false, OtfsProjectId projectId = null)
        {
            var user = all ? string.Empty : "[Assigned To] = @User and ";
            var iterator = iteration == null ? string.Empty : " and [Iteration Path] under @Path";
            var project_Id = projectId == null ? string.Empty : " and [ProjectId] under @ProjectId";

            string query = "Select * " +
                           "From WorkItems " +
                           "Where [Work Item Type] = @Type and " + user +
                           "      [State] = @State" + iterator;

            var param = new Dictionary<string, string>();
            param.Add("State", state.ToString());
            param.Add("User", this.UsuarioAutenticado());
            param.Add("Type", "Product Backlog Item");
            param.Add("Path", iteration.Path);
            if (projectId != null)
               param.Add("ProjectId", projectId.Id.ToString());

            return Execute(query, param, iteration);
        }
        
        public OtfsBacklogItem Localizar(OtfsBacklogItemId backlogItemId)
        {
            var item = this.Context.TeamProjectCollection.GetService<WorkItemStore>().GetWorkItem(backlogItemId.Id);
            return WorkItenFactory<OtfsBacklogItem>.Build(item);
        }

        internal OtfsBacklogItemMetrics GerarMetricasDaIteracao(OtfsIteration iteration)
        {
            var backlogItens = LocalizarPorIteracao(iteration);
            return backlogItens.GerarMetricas();
        }

        public OtfsBacklogItemCollection LocalizarPorIteracao(OtfsIteration iteration)
        {
            string query = "Select * " +
                           "From WorkItems " +
                           "Where [Work Item Type] = @Type"+
                           "      and [Assigned To] = @User" +
                           "      and [State] <> @State" +
                           "      and [Iteration Path] under @Path";

            var param = new Dictionary<string, string>();
            param.Add("User", this.UsuarioAutenticado());
            param.Add("Type", "Product Backlog Item");
            param.Add("State", "Removed");
            param.Add("Path", iteration.Path);

            var backlogItens = Execute(query, param, iteration);

            return new OtfsBacklogItemCollection
            {
                BacklogsItemsPendentes = backlogItens.Where(OtfsBacklogItem.QueryBacklogItemPendente),
                BacklogsItemsAndamento = backlogItens.Where(OtfsBacklogItem.QueryBacklogItemAndamento),
                BacklogsItemsParaTestes = backlogItens.Where(OtfsBacklogItem.QueryBacklogItemParaTestes),
                BacklogsItemsConcluidas = backlogItens.Where(OtfsBacklogItem.QueryBacklogItemConcluidas),
                BacklogsItemsParaBuild = backlogItens.Where(OtfsBacklogItem.QueryBacklogItemParaBuild),
                BacklogsItemsPublicados = backlogItens.Where(OtfsBacklogItem.QueryBacklogItemPublicado)
            };
        }

        private IEnumerable<OtfsBacklogItem> Execute(string query, Dictionary<string, string> param, OtfsIteration iteration)
        {
            var workItem = Context.TeamProjectCollection.GetService<WorkItemStore>();
            var result = new Query(workItem, query, param).RunQuery();

            return IterarWorkItemsCollection(result, iteration).ToList();
        }

        private IEnumerable<OtfsBacklogItem> IterarWorkItemsCollection(WorkItemCollection collection, OtfsIteration iteration)
        {
            foreach (WorkItem item in collection)
            {
                var backlogItem = WorkItenFactory<OtfsBacklogItem>.Build(item);

                backlogItem.AdicionarTags(item.Tags.Replace(" ", ""));
                backlogItem.Comitar(iteration);

                yield return backlogItem;
            }
        }

        public void Atualizar(OtfsBacklogItem otfsBacklogItem)
        {
            WorkItemStore workItemStore = Context.TeamProjectCollection.GetService<WorkItemStore>();
            WorkItem workItem = workItemStore.GetWorkItem(otfsBacklogItem.BacklogItemId.Id);

            workItem.Tags = otfsBacklogItem.Tag;
            workItem.State = otfsBacklogItem.State.ToString();
            workItem.History = otfsBacklogItem.History;
            workItem.Save();
        }
    
    }
}
