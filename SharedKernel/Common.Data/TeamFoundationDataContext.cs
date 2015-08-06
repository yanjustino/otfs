using Microsoft.TeamFoundation.Client;
using System;
using System.Net;

namespace Common.Data
{
    public class TeamFoundationDataContext
    {
        public TfsTeamProjectCollection TeamProjectCollection { get; private set; }

        public void Conectar(Uri uri, NetworkCredential credential)
        {
            TeamProjectCollection = credential == null ?
                new TfsTeamProjectCollection(uri) :
                new TfsTeamProjectCollection(uri, credential);

            TeamProjectCollection.EnsureAuthenticated();
        }
    }
}
