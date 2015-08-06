using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System;
using System.Collections.Generic;

namespace Otfs.ProjectManagement.Core.Domain.Model.Iteration
{
    public partial class OtfsIteration
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public OtfsProjectId ProjectId { get; private set; }
        public OtfsBacklogItemMetrics Metrics { get; private set; }

        public OtfsIteration(OtfsProjectId projectId, string name, string path)
        {
            this.Path = path;
            this.ProjectId = projectId;
            this.Name = name;
        }

        public OtfsIteration(OtfsProjectId projectId, string name, string path, DateTime? start, DateTime? end)
            : this(projectId, name, path)
        {
            this.EndDate = end;
            this.StartDate = start;
        }


        public void InformarMetricas(OtfsBacklogItemMetrics metrics)
        {
            this.Metrics = metrics;
        }
    }
}
