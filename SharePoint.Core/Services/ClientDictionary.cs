using System.Collections.Generic;
using System.Linq;
using Reckner.Silverlight.SharePoint.Core.Models;
using Reckner.Silverlight.SharePoint.Core.ViewModels;

namespace Reckner.Silverlight.SharePoint.Core.Services
{
    public class ClientDictionary : Dictionary<string, Client>
    {

        public Client[] GetAllClients()
        {

         return (from a in this select a.Value).OrderBy(x => x.Name).ToArray();

        }


        private void Load(string  SiteUrl)
        {
            ClientsViewModel c = new ClientsViewModel(SiteUrl);
            c.Clients
        }
        private void Initialize(List<Client> clients)
        {
            foreach (var c in clients)
            {
                Add(c.Id, c);
            }
        }

        private Client LookupClient(string ClientId)
        {
            Client c;
            if (!TryGetValue(ClientId, out c))
                c = new Client()
                {
                    Id = ClientId,
                    Name = "Undefined"
                };
            return c;
        }

    }
}

