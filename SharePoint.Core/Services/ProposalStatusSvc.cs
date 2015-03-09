using System.Collections.Generic;
using Reckner.Silverlight.SharePoint.Core.Models;

namespace Reckner.Silverlight.SharePoint.Core.Services
{
    public static class ProposalStatusSvc
    {
        public static List<ProposalStatus> GetAll()
        {
            return new List<ProposalStatus>
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