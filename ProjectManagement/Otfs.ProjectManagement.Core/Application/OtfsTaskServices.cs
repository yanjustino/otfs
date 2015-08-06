using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using Otfs.ProjectManagement.Core.Domain.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otfs.ProjectManagement.Core.Application
{
    public class OtfsTaskServices : IOtfsTaskServices
    {
        private IOtfsProjectRepository _otfsProjectRepository;
        private IOtfsTaskRepository _otfsTaskRepository;

        public OtfsTaskServices(
            IOtfsTaskRepository otfsTaskRepository,
            IOtfsProjectRepository otfsProjectRepository)
        {
            _otfsTaskRepository = otfsTaskRepository;
            _otfsProjectRepository = otfsProjectRepository;
        }

        public IEnumerable<OtfsTask> RetornarTarefas()
        {
            var projetos = _otfsProjectRepository.Todos();
            return _otfsTaskRepository.BuscarTarefasDesenvolvedor();
        }
    }
}
