using System.Collections.Generic;

namespace Otfs.ProjectManagement.Core.Domain.Model.Task
{
    public interface IOtfsTaskRepository
    {
        IEnumerable<OtfsTask> BuscarTarefasDesenvolvedor();
    }
}
