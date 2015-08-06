using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System.Collections.Generic;

namespace Otfs.ProjectManagement.Core.Domain.Model.Iteration
{
    public interface IOtfsIterationRepository
    {
        IEnumerable<OtfsIteration> Agendados(OtfsProjectId projectId);
        IEnumerable<OtfsIteration> Correntes();
        OtfsIteration PorIteracao(OtfsProjectId projectId, string path);
    }
}
