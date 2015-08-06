using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.ProjectManagement.Core.Application
{
    public class OtfsProjectServices : IOtfsProjectServices
    {
        private IOtfsProjectRepository _otfsProjectRepository;

        public OtfsProjectServices(IOtfsProjectRepository otfsProjectRepository)
        {
            _otfsProjectRepository = otfsProjectRepository;
        }

        public IEnumerable<OtfsProject> RetornarTodosProjetos()
        {
            return _otfsProjectRepository.Todos();
        }

        public IEnumerable<OtfsProject> RetornarProjetosPorUsuario(Guid userId)
        {
            return _otfsProjectRepository.ProjetosUsuario(userId);
        }
    }
}
