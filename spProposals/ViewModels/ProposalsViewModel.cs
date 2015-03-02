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
            _selectedProposalId = Proposals[0].Id;
            Clients = ClientSvc.GetAllClients(Proposals);
            ProposalStati = ProposalStatusSvc.GetAll();
            _selectedProposalStatusId = ProposalStatusSvc.GetDefault().Id;
            _selectedClientId = ClientSvc.GetDefault().Id;

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
            FilterByClient();
            FilterByStatus();
        }

        private void FilterByClient()
        {
            var fr = new List<ProposalsItem>();

            if (SelectedClientId != "All")
            {
                fr = (from fobjs in Proposals
                    where fobjs.ClientID == SelectedClientId
                    select fobjs).ToList();

                if (FilteredProposals.Count == fr.Count())
                    return;
                FilteredProposals = new ObservableCollection<ProposalsItem>(fr);
            }
            else
                FilteredProposals = Proposals;
        }


        private void FilterByStatus()
        {
            var fr = new List<ProposalsItem>();

            if (SelectedProposalStatusId != "All")
            {
                fr = (from fobjs in FilteredProposals
                    where fobjs.SiteType == SelectedProposalStatusId
                    select fobjs).ToList();

                if (FilteredProposals.Count == fr.Count())
                    return;
                FilteredProposals = new ObservableCollection<ProposalsItem>(fr);
            }
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


        #region SelectedProposalId

        private int _selectedProposalId = 1;


        public int SelectedProposalId
        {
            get { return _selectedProposalId; }
            set
            {
                _selectedProposalId = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
    }

}
