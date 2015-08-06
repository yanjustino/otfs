using Otfs.IdentityAccess.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using System.Collections.Generic;
using System.Linq;

namespace OpenTfsPortal.Models
{
    public class ProjectManagementViewModel
    {
        public int SelectedProjectId { get; set; }
        public IEnumerable<OtfsProject> Projects { get; set; }
        public UsuarioAutorizado Usuario { get; set; }

        public string GetUserRoute()
        {
            return InRoleIfraestructure() ? "DashboardInfraestrutura" : "DashboardAuditorFiscal";
        }

        private bool InRoleIfraestructure()
        {
            return Usuario.PertenceAoGrupo("Infraestrutura");
        }
    }
}