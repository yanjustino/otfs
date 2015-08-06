using Common.Data;
using Common.Domain.Model;
using Otfs.IdentityAccess.Core.Application.Command;
using Otfs.IdentityAccess.Core.Domain.Model;
using Otfs.IdentityAccess.Core.Domain.Model.Interfaces;

namespace Otfs.IdentityAccess.Data
{
    public class IdentityAccessRepository : IIdentityAccessRepository
    {
        private TeamFoundationDataContext _context;
        private IOtfsContainer _container;

        public IdentityAccessRepository(IOtfsContainer container)
        {
            _container = container;
            _container.Register();
            _context = _container.Resolver<TeamFoundationDataContext>();
        }

        public UsuarioAutorizado Register(AutenticacaoCommand command)
        {
            _context.Conectar(command.Uri, command.Credential);

            return Factory.UsuarioAutorizadoFactory.Build(_context.TeamProjectCollection);
        }

        public void UnRegister()
        {
            _container.UnRegister();
        }
    }
}
