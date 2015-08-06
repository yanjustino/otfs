using Common.Data;
using Otfs.IdentityAccess.Core.Domain.Model;
using Otfs.IdentityAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenTfsPortal
{
    internal class OtfsContainerSession
    {
        internal static bool HasInstance()
        {
            return GetInstance() != null;
        }

        internal static void SetUser(UsuarioAutorizado usuario)
        {
            HttpContext.Current.Session["OtfsDataContextUser"] = usuario;
        }

        internal static UsuarioAutorizado GetUser()
        {
            return HttpContext.Current.Session["OtfsDataContextUser"] as UsuarioAutorizado;
        }

        internal static void RemoveUser()
        {
            HttpContext.Current.Session.Remove("OtfsDataContextUser");
        }
        
        internal static void Register()
        {
            if (!HasInstance())
                HttpContext.Current.Session["OtfsDataContext"] = new TeamFoundationDataContext();
        }

        internal static TeamFoundationDataContext GetInstance()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["OtfsDataContext"] != null)
                return HttpContext.Current.Session["OtfsDataContext"] as TeamFoundationDataContext;
            return null;
        }

        internal static void Abort()
        {
            HttpContext.Current.Session.Remove("OtfsDataContext");
        }
    }
}