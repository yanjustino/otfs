using Otfs.IdentityAccess.Core.Application.Command;
using Otfs.IdentityAccess.Core.Domain.Model;
using Otfs.IdentityAccess.Core.Domain.Model.Interfaces;

namespace Otfs.IdentityAccess.Core.Application
{
    public class IdentityAccessServices: IIdentityAccessServices
    {
        IIdentityAccessRepository _repository;

        public IdentityAccessServices(IIdentityAccessRepository repository)
        {
            _repository = repository;
        }

        public UsuarioAutorizado SignIn(AutenticacaoCommand command)
        {
            return _repository.Register(command);
        }

        public void SignOut()
        {
            _repository.UnRegister();
        }
    }
}
