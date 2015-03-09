using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Reckner.Silverlight.SharePoint.Core.Models;
using Reckner.Silverlight.SharePoint.Core.Services;
using Reckner.Silverlight.SharePoint.Core.ViewModels;
using spProposals.ServiceReference1;
using spProposals.Services;

namespace spProposals.ViewModels
{
    public class ProposalsViewModel : ViewModelBase
    {
        private static Uri _siteUri = new Uri(SpProperties.BlueBerryHomeUrl);
        ServiceReference1.BlueberryDataContext _objDataContext;
        private ClientsViewModel _vmClients = new ClientsViewModel(SpProperties.BlueBerryHomeUrl);

        // sets up the 

        public ProposalsViewModel()
        {
            RefreshAll();
        }

        protected async void RefreshAll()
        {
            Proposals = await ProposalSvc.GetProposals();
            FilteredProposals = Proposals;
            ProposalStati = new ObservableCollection<ProposalStatus>(ProposalStatusSvc.GetAll());
            SelectedProposalStatusId = ProposalStatusSvc.GetDefault().Id;
            SelectedClientId = "All";
        }

        public string SelectedClientId
        {
            get { return _vmClients.SelectedClientId; }
            set
            {
               _vmClients.SelectedClientId = value;
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

        public ObservableCollection<Client> Clients 
        {

            get { return _vmClients.Clients; }
            set
            {
                _vmClients.Clients = value; 
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
            FilteredProposals = Proposals;
            //FilterProposals();
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
