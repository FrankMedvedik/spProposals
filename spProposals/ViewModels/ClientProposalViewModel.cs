//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
//using Reckner.Silverlight.SharePoint.Core.Models;
//using Reckner.Silverlight.SharePoint.Core.Services;
//using Reckner.Silverlight.SharePoint.Core.ViewModels;
//using spProposals.Entities;
//using ProposalStatus = Reckner.Silverlight.SharePoint.Core.Models.ProposalStatus;

//namespace spProposals.ViewModels
//{
//    public class ClientProposalViewModel : ViewModelBase
//    {
//        private ProposalsViewModel _proposals;
//        private ClientsViewModel _clients;
//        public ClientProposalViewModel()
//        {
//            _proposals = new ProposalsViewModel();
//            _clients = new ClientsViewModel(SpProperties.BlueBerryHomeUrl);
//            RefreshAll();
//        }

//        protected async void RefreshAll()
//        {
//            ClientProposals = await GetClientProposals();
//            FilteredClientProposals = ClientProposals;
//            ProposalStati = ProposalStatusSvc.GetAll();
//            SelectedProposalStatusId = ProposalStatusSvc.GetDefault().Id;
//            SelectedClientId = "All";
//        }

//        private Task<ObservableCollection<ClientProposal>> GetClientProposals()
//        {

//            var ps = _proposals.Proposals.ToList();
//            var cs = _clients.Clients.ToList();

//            var query ={ from p in ps Join c in cs on c.clientId equals p.ClientId
//            select new 
//            {  c.Id, c.Name, c.Url
//                p.SiteUrl ,
//                p.SiteType ,
//                p.EditUr};

//    // Display joined groups.
//    foreach (var group in query)
//    {
//        Console.WriteLine("{0} bought {1}", group.Name, group.Product);
//    }

//        }

//        private String _selectedClientId;
//        public String SelectedClientId
//        {
//            get { return _selectedClientId;  }
//            set
//            {
//              _selectedClientId = value;
//               NotifyPropertyChanged();
//                RefreshFilteredData();
//            }
//        }

//        private string _selectedProposalStatusId;
//        public string SelectedProposalStatusId
//        {
//            get { return _selectedProposalStatusId; }
//            set
//            {
//                _selectedProposalStatusId = value;
//                NotifyPropertyChanged();
//                RefreshFilteredData();
//            }
//        }

//        public ObservableCollection<Client> Clients 
//        {

//            get { return _clients.Clients; }
//            set
//            {
//                _clients.Clients = value; 
//                NotifyPropertyChanged();
//            }
//        }

//        private ObservableCollection<ProposalStatus> _proposalStati= new ObservableCollection<ProposalStatus>();
//        public ObservableCollection<ProposalStatus> ProposalStati
//        {
//            get { return _proposalStati; }
//            set
//            {
//                _proposalStati = value;
//                NotifyPropertyChanged();
//            }
//        }

//        #region Filters

//        private ObservableCollection<ClientProposal> _filteredClientProposals = new ObservableCollection<ClientProposal>();
//        public ObservableCollection<ClientProposal> FilteredClientProposals
//        {
//            get { return _filteredClientProposals; }
//            set
//            {
//                _filteredClientProposals = value;
//                NotifyPropertyChanged();
//            }
//        }

//        private void RefreshFilteredData()
//        {
//            FilteredClientProposals = ClientProposals;
//            //FilterProposals();
//        }

//        private void FilterClientProposals()
//        {
//            var fr = new List<ClientProposal>();

//            if ((SelectedClientId != "All") && (SelectedProposalStatusId != "All"))
//            {
//                fr = (from p in ClientProposals
//                    where p.ClientId == SelectedClientId
//                          && p.ProposalSiteType == SelectedProposalStatusId
//                    select p).ToList();
//            }
//            else if ((SelectedClientId == "All") && (SelectedProposalStatusId != "All"))
//            {
//                fr = (from p in ClientProposals
//                    where p.ProposalSiteType == SelectedProposalStatusId
//                    select p).ToList();
//            }
//            else if ((SelectedClientId != "All") && (SelectedProposalStatusId == "All"))
//            {
//                fr = (from p in ClientProposals
//                    where p.ClientId == SelectedClientId
//                    select p).ToList();
//            }
//            if ((SelectedClientId == "All") && (SelectedProposalStatusId == "All"))
//            {
//                fr = ClientProposals.ToList();
//            }
//            FilteredClientProposals = new ObservableCollection<ClientProposal>(fr);
//        }

//        #endregion

        
        

//        #region Proposals

//        private ObservableCollection<ClientProposal> _clientProposals = new ObservableCollection<ClientProposal>();

//        public ObservableCollection<ClientProposal> ClientProposals
//        {
//            get { return _clientProposals; }
//            set
//            {
//                _clientProposals= value;
//                NotifyPropertyChanged();
//            }
//        }

     
//        #endregion


//        #region SelectedProposal

//        private ClientProposal _selectedClientProposal;
//        public ClientProposal  SelectedClientProposal
//        {
//            get { return _selectedClientProposal; }
//            set
//            {
//                _selectedClientProposal = value;
//                NotifyPropertyChanged();
//            }
//        }
//        #endregion
//    }

//}


    