using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using spProposals.Entities;
using spProposals.ServiceReference1;
using spProposals.Services;
namespace spProposals.ViewModels
{
    public class ProposalsViewModel : ViewModelBase
    {
        private static Uri _siteUri = new Uri(SpProperties.BlueBerryHomeUrl);
        ServiceReference1.BlueberryDataContext _objDataContext;

        // sets up the 

        public ProposalsViewModel()
        {
            RefreshAll();
        }

        protected async void RefreshAll()
        {
            Proposals = await ProposalSvc.GetProposals();
            FilteredProposals = Proposals;
            Clients = ClientSvc.GetAllClients(Proposals);
            ProposalStati = ProposalStatusSvc.GetAll();
            SelectedProposalStatusId = ProposalStatusSvc.GetDefault().Id;
            SelectedClientId = ClientSvc.GetDefault().Id;

            //SelectedProposal = Proposals[0];

        }

        private string _selectedClientId;
        public string SelectedClientId
        {
            get { return _selectedClientId; }
            set
            {
                _selectedClientId= value;
                NotifyPropertyChanged();
                RefreshFilteredData();
            }
        }

        private string _selectedProposalStatusId;
        public string SelectedProposalStatusId
        {
            get { return _selectedProposalStatusId; }
            set
            {
                _selectedProposalStatusId = value;
                NotifyPropertyChanged();
                RefreshFilteredData();
            }
        }

        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ProposalStatus> _proposalStati= new ObservableCollection<ProposalStatus>();
        public ObservableCollection<ProposalStatus> ProposalStati
        {
            get { return _proposalStati; }
            set
            {
                _proposalStati = value;
                NotifyPropertyChanged();
            }
        }

        #region Filters

        private ObservableCollection<ProposalsItem> _filteredProposals = new ObservableCollection<ProposalsItem>();
        public ObservableCollection<ProposalsItem> FilteredProposals
        {
            get { return _filteredProposals; }
            set
            {
                _filteredProposals = value;
                NotifyPropertyChanged();
            }
        }

        private void RefreshFilteredData()
        {
            FilterProposals();
        }

        private void FilterProposals()
        {
            var fr = new List<ProposalsItem>();

            if ((SelectedClientId != "All") && (SelectedProposalStatusId != "All"))
            {
                fr = (from p in Proposals
                    where p.ClientID == SelectedClientId
                          && p.SiteType == SelectedProposalStatusId
                    select p).ToList();
            }
            else if ((SelectedClientId == "All") && (SelectedProposalStatusId != "All"))
            {
                fr = (from p in Proposals
                    where p.SiteType == SelectedProposalStatusId
                    select p).ToList();
            }
            else if ((SelectedClientId != "All") && (SelectedProposalStatusId == "All"))
            {
                fr = (from p in Proposals
                    where p.ClientID == SelectedClientId
                    select p).ToList();
            }
            if ((SelectedClientId == "All") && (SelectedProposalStatusId == "All"))
            {
                fr = Proposals.ToList();
            }
            FilteredProposals = new ObservableCollection<ProposalsItem>(fr);
        }

        #endregion

        
        

        #region Proposals

        private ObservableCollection<ProposalsItem> _proposals = new ObservableCollection<ProposalsItem>();

        public ObservableCollection<ProposalsItem> Proposals
        {
            get { return _proposals; }
            set
            {
                _proposals = value;
                NotifyPropertyChanged();
            }
        }

     
        #endregion


        #region SelectedProposal

        private ProposalsItem _selectedProposal;
        public ProposalsItem SelectedProposal
        {
            get { return _selectedProposal; }
            set
            {
                _selectedProposal = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
    }

}
