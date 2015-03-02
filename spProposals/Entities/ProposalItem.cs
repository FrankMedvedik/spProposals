namespace spProposals.ServiceReference1
{
    public partial class ProposalsItem : global::System.ComponentModel.INotifyPropertyChanged
    {
        private string _siteType;

        public string SiteUrl
        {
            get
            {
                string workUrl;
                if (JobNumber != null)
                {
                    workUrl = SpProperties.WorkUrl + JobNumber.Substring(0, 4) + "/" + JobNumber.Substring(5);
                    SiteType = "Job";
                }
                else if (ClientID != null && ProposalID != null)
                {
                    workUrl = SpProperties.BlueBerryHomeUrl + ClientID.Replace(" ", "%20") + "/" + ProposalID;
                    SiteType = "Prop";
                }
                else
                {
                    workUrl = "";
                    SiteType = "INVALID";
                }
                return workUrl;
            }
        }
        public string SiteType
        {
            get { return _siteType; }
            set { _siteType = value; }
        }

        public string EditUrl
        {
            get
            {
                return SpProperties.BlueBerryProposalsDetailUrl + this.Id;
                
            }
        }

    }

    
}