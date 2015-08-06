using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;
using Otfs.IdentityAccess.Core.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Otfs.IdentityAccess.Data.Factory
{
    internal static class UsuarioAutorizadoFactory
    {
        public static UsuarioAutorizado Build(TfsTeamProjectCollection projects)
        {
            var usuario = new UsuarioAutorizado
            (
                projects.AuthorizedIdentity.TeamFoundationId,
                projects.AuthorizedIdentity.DisplayName
            );

            RecuperarGruposUsuario(projects, usuario);

            return usuario;
        }

        private static void RecuperarGruposUsuario(TfsTeamProjectCollection projectCollection, UsuarioAutorizado usuario)
        {
            List<Identity> memberOf = RecuperarIdentidadesGrupos(projectCollection);

            usuario.AdicionarGrupos(memberOf.Select(x => new Grupo(x.TeamFoundationId, x.DisplayName)).ToArray());
        }

        private static List<Identity> RecuperarIdentidadesGrupos(TfsTeamProjectCollection projectCollection)
        {
            var service = projectCollection.GetService<IGroupSecurityService>();

            Identity iden = service.Convert(projectCollection.AuthorizedIdentity);
            Identity sids = service.ReadIdentity(SearchFactor.Sid, iden.Sid, QueryMembership.Expanded);

            return service.ReadIdentities(SearchFactor.Sid, sids.MemberOf, QueryMembership.Expanded).ToList();
        }
    }
}
