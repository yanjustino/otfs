using System;

namespace Otfs.IdentityAccess.Core.Domain.Model
{
    public class Grupo
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Grupo(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
