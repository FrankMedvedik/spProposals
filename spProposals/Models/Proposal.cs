using System;
using System.Text.RegularExpressions;

namespace spProposals.Models
{
    public class Proposal 
    {
        private string _clientId;

        public string ClientID
        {
            get
            {
                return _clientId;
            }
            set { _clientId = value; }
        }

        public string ClientSiteName
        {
            get
            {
                return  (ClientID == "SC JOH") ? "SC%20JOH" : ClientID;
            }
        }

        public string ClientName { get; set; }
        public string JobNumber { get; set; }
        public Int32 Id { get; set; }
        public string ProposalId { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public String SiteUrl
        {
            get
            {
                string workUrl;
                switch (SiteType)
                {
                    case "Job":
                        workUrl = SpProperties.WorkUrl + JobNumber.Substring(0, 4) + "/" + JobNumber.Substring(5);
                        break;
                    case "Archive":
                        //workUrl = SpProperties.BlueBerryHomeUrl + Regex.Replace(ClientSiteName, @"\W|_", string.Empty) + "/Archive/" + ArchiveDate.Value.Year + "/" + Regex.Replace(ProposalId, @"\W|_", string.Empty);
                        workUrl = SpProperties.BlueBerryHomeUrl + ClientSiteName + "/Archive/" + ArchiveDate.Value.Year + "/" + Regex.Replace(ProposalId, @"\W|_", string.Empty);

                        break;
                    case "Proposal":
                        //workUrl = SpProperties.BlueBerryHomeUrl + Regex.Replace(ClientSiteName, @"\W|_", string.Empty) + "/" +Regex.Replace(ProposalId, @"\W|_", string.Empty);
                        workUrl = SpProperties.BlueBerryHomeUrl + ClientSiteName + "/" + Regex.Replace(ProposalId, @"\W|_", string.Empty);
                        break;
                    default:
                        workUrl = "";
                        break;
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
                    siteType = "Job";
                else if (ClientID != null && ProposalId != null && ArchiveDate != null)
                    siteType = "Archive";
                else if (ClientID != null && ProposalId != null)
                    siteType = "Proposal";
                else
                    siteType = "INVALID";
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
                 get { return !string.IsNullOrEmpty(JobNumber); }
        }
        public String Title { get; set; }

    }

    
}