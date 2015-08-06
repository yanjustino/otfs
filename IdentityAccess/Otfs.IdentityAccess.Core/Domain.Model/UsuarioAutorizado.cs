using System;
using System.Linq;

namespace Otfs.IdentityAccess.Core.Domain.Model
{
    public class UsuarioAutorizado
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Grupo[] Grupos { get; private set; }

        public UsuarioAutorizado(Guid id, string name)
        {
            this.Id = id;
            this.Nome = name;
        }

        public void AdicionarGrupos(Grupo[] grupos)
        {
            this.Grupos = grupos;
        }

        public bool PertenceAoGrupo(string perfil)
        {
            return Grupos.Any(x => x.Name.Equals(perfil));
        }
    }
}
