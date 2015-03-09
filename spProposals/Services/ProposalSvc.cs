using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using spProposals.ServiceReference1;

namespace spProposals.Services
{
    public static class ProposalSvc
    {
        private static ServiceReference1.BlueberryDataContext _objDataContext =
            new BlueberryDataContext(new Uri(SpProperties.BlueBerryProposalsUrl));

        public static async Task<ObservableCollection<ProposalsItem>> GetProposals()
        {
            var wqs = new ObservableCollection<ProposalsItem>();
            var proposalDataQuery = from p in _objDataContext.Proposals
                select p;
            var proposalQuery = (DataServiceQuery<ProposalsItem>) proposalDataQuery;

            var t = await Task<IEnumerable<ProposalsItem>>.Factory.FromAsync(proposalQuery.BeginExecute,
                proposalQuery.EndExecute, proposalDataQuery);

            wqs = new ObservableCollection<ProposalsItem>(t.ToList());
            return wqs;
        }

        public static async Task<ObservableCollection<ProposalsItem>> GetClientProposals(string clientId)
        {
            var wqs = new ObservableCollection<ProposalsItem>();
            var proposalDataQuery = _objDataContext.Proposals.Where(p => p.ClientID == clientId);
            var proposalQuery = (DataServiceQuery<ProposalsItem>) proposalDataQuery;

            var t = await Task<IEnumerable<ProposalsItem>>.Factory.FromAsync(proposalQuery.BeginExecute,
                proposalQuery.EndExecute, proposalDataQuery);
            wqs = new ObservableCollection<ProposalsItem>(t.ToList().OrderBy(x => x.ClientID).ThenBy(x => x.Title) );
            return wqs;
        }

    }

}