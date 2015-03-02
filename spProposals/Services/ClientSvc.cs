using System;
using System.Collections.ObjectModel;
using System.Linq;
using spProposals.Entities;
using spProposals.ServiceReference1;

namespace spProposals.Services
{
        public static class ClientSvc
        {
            public static ObservableCollection<Client> GetAllClients(ObservableCollection<ProposalsItem> p)
            {
                var clientList = new ObservableCollection<Client>();
                var qry = p.Select(i => i.ClientID).Distinct().OrderBy(m => m);
                clientList.Add(GetDefault());
                foreach (var n in qry)
                    clientList.Add(new Client()
                    {
                        Id = n,
                        Name = n
                    });
                return clientList;

            }
            public static Client GetDefault()
            {
                return new Client()
                {
                    Id = "All",
                    Name = "All"
                };

            }
        }

}
