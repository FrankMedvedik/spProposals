using System;
using System.Text.RegularExpressions;
using spProposals.ViewModels;

namespace spProposals.ServiceReference1
{
    public partial class ProposalsItem : ViewModelBase
    {
        public String SiteUrl
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

        public String SiteType
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

        public String EditUrl
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
        public String ClientTitle { get; set; }

    }

    
}