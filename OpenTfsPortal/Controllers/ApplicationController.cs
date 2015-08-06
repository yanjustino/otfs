using Otfs.IdentityAccess.Core.Domain.Model;
using System.Web.Mvc;
using System.Web.Security;

namespace OpenTfsPortal.Controllers
{
    public class ApplicationController : Controller
    {
        public UsuarioAutorizado UsuarioAutorizadoSession 
        {
            get
            {
                return OtfsContainerSession.GetUser();
            }
        }

        public bool Autorizado
        {
            get
            {
                return UsuarioAutorizadoSession != null;
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!IsLoginPage(filterContext) && !Autorizado)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                OtfsContainerSession.Abort();
            }
        }

        private static bool IsLoginPage(AuthorizationContext filterContext)
        {
            return filterContext.ActionDescriptor.ActionName == "Login" &&
                   filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "IdentityAccess";
        }
    }
}