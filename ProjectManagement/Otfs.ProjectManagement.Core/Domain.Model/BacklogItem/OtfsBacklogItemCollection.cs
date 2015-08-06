using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Domain.Model.BacklogItem
{
    public class OtfsBacklogItemCollection
    {
        public IEnumerable<OtfsBacklogItem> BacklogsItemsPendentes { get; set; }
        public IEnumerable<OtfsBacklogItem> BacklogsItemsAndamento { get; set; }
        public IEnumerable<OtfsBacklogItem> BacklogsItemsParaTestes { get; set; }
        public IEnumerable<OtfsBacklogItem> BacklogsItemsConcluidas { get; set; }
        public IEnumerable<OtfsBacklogItem> BacklogsItemsParaBuild { get; set; }
        public IEnumerable<OtfsBacklogItem> BacklogsItemsPublicados { get; set; }


        public OtfsBacklogItemMetrics GerarMetricas()
        {
            var pending = this.BacklogsItemsPendentes.Count();
            var started = (this.BacklogsItemsAndamento.Count() + this.BacklogsItemsParaTestes.Count());
            var finished = (this.BacklogsItemsConcluidas.Count() + this.BacklogsItemsPublicados.Count());

            return new OtfsBacklogItemMetrics(pending, started, finished);
        }
    }
}
