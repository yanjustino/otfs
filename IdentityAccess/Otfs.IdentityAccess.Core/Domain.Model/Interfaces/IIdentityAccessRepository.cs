using Otfs.IdentityAccess.Core.Application.Command;

namespace Otfs.IdentityAccess.Core.Domain.Model.Interfaces
{
    public interface IIdentityAccessRepository
    {
        UsuarioAutorizado Register(AutenticacaoCommand command);
        void UnRegister();
    }
}
