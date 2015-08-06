using Otfs.IdentityAccess.Core.Application.Command;
using Otfs.IdentityAccess.Core.Domain.Model;

namespace Otfs.IdentityAccess.Core.Application
{
    public interface IIdentityAccessServices
    {
        UsuarioAutorizado SignIn(AutenticacaoCommand command);
        void SignOut();
    }
}
