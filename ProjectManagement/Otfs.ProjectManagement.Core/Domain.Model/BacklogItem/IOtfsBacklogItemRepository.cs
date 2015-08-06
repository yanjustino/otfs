using Otfs.ProjectManagement.Core.Domain.Model.Iteration;
using System.Collections.Generic;

namespace Otfs.ProjectManagement.Core.Domain.Model.BacklogItem
{
    public interface IOtfsBacklogItemRepository
    {
        IEnumerable<OtfsBacklogItem> ItensEmAndamento(OtfsIteration[] iterations);
        IEnumerable<OtfsBacklogItem> ItensConcluidos(OtfsIteration[] iterations);
        IEnumerable<OtfsBacklogItem> ItensParaBuild(OtfsIteration[] iterations);
        IEnumerable<OtfsBacklogItem> ItensParaBuild(OtfsIteration iteration = null);
        IEnumerable<OtfsBacklogItem> LocalizarPorStatus(OtfsBacklogItemState state, OtfsIteration iteration = null);
        
        OtfsBacklogItemCollection LocalizarPorIteracao(OtfsIteration iteration);
        OtfsBacklogItem Localizar(OtfsBacklogItemId backlogItemId);

        void Atualizar(OtfsBacklogItem otfsBacklogItem);
    }
}
