using System.Collections.Generic;
using spProposals.Models;

namespace spProposals.Services
{
    public class ClientDictionary : Dictionary<string, Client>
    {
        public void Initialize(List<Client> clients)
        {
            foreach (var c in clients)
            {
                Add(c.Id, c);
            }
        }

        public  Client LookupClient(string ClientId)
        {
            Client c;
            if (ClientId == null || !TryGetValue(ClientId, out c) )
                c = new Client()
                {
                    Id = ClientId,
                    Name = "Undefined"
                };
            return c;
        }

    }
}

