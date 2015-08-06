using System;
using System.Net;

namespace Otfs.IdentityAccess.Core.Application.Command
{
    public class AutenticacaoCommand
    {
        public Uri Uri { get; private set; }
        public NetworkCredential Credential {get;private set;}

        public AutenticacaoCommand(string login, string password, string url, string collection)
        {
            this.Credential = new NetworkCredential(login, password);
            this.Uri = new Uri(string.Format("{0}/{1}", url, collection));
        }

        public AutenticacaoCommand(string url, string collection)
        {
            this.Credential = null;
            this.Uri = new Uri(string.Format("{0}/{1}", url, collection));
        }
    }
}
