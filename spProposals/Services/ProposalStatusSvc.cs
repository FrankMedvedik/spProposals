using System.Collections.ObjectModel;
using spProposals.Entities;

namespace spProposals.Services
{
    public static class ProposalStatusSvc
    {
        public static ObservableCollection<ProposalStatus> GetAll()
        {
            return new ObservableCollection<ProposalStatus>
            {
                new ProposalStatus {Id = "All", Name = "All"},
                new ProposalStatus {Id = "Proposal", Name = "Proposal"},
                new ProposalStatus {Id= "Archive", Name = "Archive"},
                new ProposalStatus {Id= "Job", Name = "Job"}
            };
        }

        public static ProposalStatus GetDefault()
        {
            return new ProposalStatus { Id = "All", Name = "All" };
        }
    }
}