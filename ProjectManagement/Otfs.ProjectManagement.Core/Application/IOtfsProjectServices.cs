using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System;
using System.Collections.Generic;

namespace Otfs.ProjectManagement.Core.Application
{
    public interface IOtfsProjectServices
    {
        IEnumerable<OtfsProject> RetornarTodosProjetos();
        IEnumerable<OtfsProject> RetornarProjetosPorUsuario(Guid userId);
    }
}
