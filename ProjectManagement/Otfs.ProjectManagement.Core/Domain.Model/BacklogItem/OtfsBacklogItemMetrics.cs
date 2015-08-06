using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Domain.Model.BacklogItem
{
    public class OtfsBacklogItemMetrics
    {
        public double PendingBacklogItens { get; private set; }
        public double StartedBacklogItens { get; private set; }
        public double FinishedBacklogItens { get; private set; }
        public double TotalBacklogItens { get; private set; }

        public double PendingBacklogItensPercent { get; private set; }
        public double StartedBacklogItensPercent { get; private set; }
        public double FinishedBacklogItensPercent { get; private set; }


        public OtfsBacklogItemMetrics(int pending, int started, int finished)
        {
            this.PendingBacklogItens = pending;
            this.StartedBacklogItens = started;
            this.FinishedBacklogItens = finished;

            double total = pending + started + finished;
            this.TotalBacklogItens = total;

            this.PendingBacklogItensPercent = pending > 0 ? pending / total * 100 : 0;
            this.StartedBacklogItensPercent = started > 0 ? started / total * 100 : 0;
            this.FinishedBacklogItensPercent = finished > 0 ? finished / total * 100 : 0; 
        }

    }
}
