using System;
using System.Collections.Generic;

namespace Otfs.ProjectManagement.Core.Domain.Model.Project
{
    public interface IOtfsProjectRepository
    {
        IEnumerable<OtfsProject> Todos();
        OtfsProject Localizar(OtfsProjectId otfsProjectId);
        IEnumerable<OtfsProject> ProjetosUsuario(Guid userId);
    }
}
