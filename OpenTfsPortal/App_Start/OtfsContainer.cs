using Common.Domain.Model;
using Otfs.IdentityAccess.Data;
using System.Web;

namespace OpenTfsPortal
{
    public class OtfsContainer : IOtfsContainer
    {
        public void Register()
        {
            OtfsContainerSession.Register();
        }

        public void UnRegister()
        {
            Abort();
        }

        internal static bool HasInstance()
        {
            return GetInstance() != null;
        }

        internal static object GetInstance()
        {
            return OtfsContainerSession.GetInstance();
        }

        internal static void Abort()
        {
            OtfsContainerSession.Abort();
        }

        public T Resolver<T>()
        {
            return (T)GetInstance();
        }
    }
}