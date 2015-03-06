using System;
using System.Text.RegularExpressions;
using spProposals.ViewModels;

namespace spProposals.ServiceReference1
{
    public partial class ProposalsItem : ViewModelBase
    {
        private string _siteType;

        public string SiteUrl
        {
            get
            {
                string workUrl;
                if (SiteType == "Job")
                {
                    workUrl = SpProperties.WorkUrl + JobNumber.Substring(0, 4) + "/" + JobNumber.Substring(5);
                }
                else if (SiteType == "Proposal")
                {
                    workUrl = SpProperties.BlueBerryHomeUrl + Regex.Replace(ClientID, @"\W|_", string.Empty) + "/" + Regex.Replace(ProposalID, @"\W|_", string.Empty); 
                }
                else
                {
                    workUrl = "";
                }
                return workUrl;
            }
        }

        public string SiteType
        {
            get
            {
                String siteType;
                if (JobNumber != null)
                {
                    siteType = "Job";
                }
                else if (ClientID != null && ProposalID != null)
                {
                    siteType = "Proposal";
                }
                else
                {
                    siteType = "INVALID";
                }
                return siteType;
            }
        }

        public string EditUrl
        {
            get
            {
               return SpProperties.BlueBerryProposalsDetailUrl + this.Id;
                
            }
        }
             public Boolean IsJob
        {
                 get { return !string.IsNullOrEmpty(_JobNumber); }
        }

    }

    
}