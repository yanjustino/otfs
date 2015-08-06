using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Application
{
    public interface IOtfsTaskServices
    {
        IEnumerable<OtfsTask> RetornarTarefas();
    }
}
