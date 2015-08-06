using OpenTfsPortal.Models;
using Otfs.ProjectManagement.Core.Application;
using System.Web.Mvc;
using System.Linq;

namespace OpenTfsPortal.Controllers
{
    [Authorize]
    public class ProjectManagementController : ApplicationController
    {
        private IOtfsProjectServices _OtfsProjectServices;

        public ProjectManagementController(IOtfsProjectServices otfsProjectServices)
        {
            _OtfsProjectServices = otfsProjectServices;
        }

        public ActionResult Index(int projectId = 0)
        {
            var projects = _OtfsProjectServices.RetornarProjetosPorUsuario(UsuarioAutorizadoSession.Id);

            var model = new ProjectManagementViewModel
            {
                Projects = projects.ToList(),
                SelectedProjectId = projectId,
                Usuario = this.UsuarioAutorizadoSession
            };

            return View(model);
        }
    }
}