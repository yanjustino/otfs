using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Application
{
    public interface IOtfsBacklogItemServices
    {
        IEnumerable<OtfsBacklogItem> RetornarBacklogsItemsParaBuild();
        OtfsBacklogItem AprovarBackLogItemParaCompilacao(OtfsBacklogItemId backlogItemId, string iteration);
        OtfsBacklogItem RejeitarBackLogItem(OtfsBacklogItemId backlogItemId, string iteration, string motivo);
        OtfsBacklogItemCollection RetorarBacklogItens(OtfsProjectId projectId, string iteration = null);
        OtfsBacklogItem ConcluirBackLogItemParaCompilacao(OtfsBacklogItemId backlogItemId, string iteration);

        void MarcarBackLogItemPublicado(OtfsProjectId projetoId);
    }
}
