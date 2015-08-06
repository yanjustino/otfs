using Otfs.IdentityAccess.Core.Application;
using Otfs.IdentityAccess.Core.Application.Command;
using System.Web.Mvc;
using System.Web.Security;

namespace OpenTfsPortal.Controllers
{
    [Authorize]
    public class IdentityAccessController : ApplicationController
    {
        private IIdentityAccessServices _identityAccessServices;

        public IdentityAccessController(IIdentityAccessServices identityAccessServices)
        {
            _identityAccessServices = identityAccessServices;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string inputUser, string inputPassword)
        {
            var url = System.Web.Configuration.WebConfigurationManager.AppSettings["TfsUrl"];
            var collection = System.Web.Configuration.WebConfigurationManager.AppSettings["TfsCollection"];
            var usuario = _identityAccessServices.SignIn(new AutenticacaoCommand(inputUser, inputPassword, url, collection));

            FormsAuthentication.SetAuthCookie(usuario.Nome, false);
            OtfsContainerSession.SetUser(usuario);

            if (UsuarioAutorizadoSession.PertenceAoGrupo("Desenvolvedores"))
                return RedirectToAction("DashboardDesenvolvedor", "Dashboard");
            else if (UsuarioAutorizadoSession.PertenceAoGrupo("Infraestrutura"))
                return RedirectToAction("DashboardInfraestrutura", "Dashboard");
            else
                return RedirectToAction("Index", "ProjectManagement");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _identityAccessServices.SignOut();

            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            OtfsContainerSession.RemoveUser();

            return RedirectToAction("Login");
        }
    }
}