using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.SharePoint.Client;
using spProposals.Models;
using spProposals.Services;

namespace spProposals.ViewModels
{
    public class ProposalsViewModel : ViewModelBase
    {
        private static Uri _siteUri = new Uri(SpProperties.BlueBerryHomeUrl);
        protected ClientContext spContext;
        private ClientDictionary _clientDictionary = new ClientDictionary();
//        private List<Proposal> _proposals = new List<Proposal>();

        public void SetProposals(List<Proposal> l)
        {
            Proposals.Clear();
            foreach (var p in l)
                Proposals.Add(p);
        }

        public void SetClients(List<Client> l)
        {
            Clients.Clear();
            foreach (var c in l)
            {
                Clients.Add(c);
            }
            SelectedClientId = Clients.FirstOrDefault(x => x.Id == "All").Id;
        }

        public ProposalsViewModel( )
        {
            RefreshAll();
        }

        protected async void RefreshAll()
        {
            LoadAll();
            FilteredProposals = Proposals;
            ProposalStati =
                new ObservableCollection<ProposalStatus>(ProposalStatusSvc.GetAll().OrderBy(x => x.Name).ToList());
            SelectedProposalStatusId = ProposalStati.FirstOrDefault(x => x.Id == "All").Id;
        }

        private string _selectedClientId;
        public string SelectedClientId
        {
            get { return _selectedClientId; }
            set
            {
                _selectedClientId = value;
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

        
        private ObservableCollection<Proposal> _filteredProposals = new ObservableCollection<Proposal>();
        public ObservableCollection<Proposal> FilteredProposals
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
            var fr = new List<Proposal>();

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
            FilteredProposals = new ObservableCollection<Proposal>(fr);
        }

        #endregion

        
        

        #region Proposals

        private ObservableCollection<Proposal> _proposals = new ObservableCollection<Proposal>();

        public ObservableCollection<Proposal> Proposals
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

        private Proposal _selectedProposal;
        public Proposal SelectedProposal
        {
            get { return _selectedProposal; }
            set
            {
                _selectedProposal = value;
                NotifyPropertyChanged();
            }
        }
        #endregion



#region load all

        private void LoadAll()
        {
            try
            {
#if DEBUG
                spContext = new ClientContext(SpProperties.BlueBerryHomeUrl);
#else
                spContext = ClientContext.Current;
#endif

                Web oWebsite = spContext.Web;
                var clientwebs = oWebsite.Webs;
                spContext.Load(oWebsite);
                spContext.Load(clientwebs);
                List plist = oWebsite.Lists.GetByTitle("Proposals");
                var camlQuery = CamlQuery.CreateAllItemsQuery();
                ListItemCollection listItems = plist.GetItems(camlQuery);
                spContext.Load(listItems);
                spContext.ExecuteQueryAsync(
                    (sender, args) =>
                    {
                        var clients = new List<Client>();
                        clients.Add(new Client() { Id = "All", Name = "<All>" });
                        var proposals = new List<Proposal>();
                        foreach (Web orWebsite in clientwebs)
                        {
                            clients.Add(new Client
                            {
                                Id = orWebsite.ServerRelativeUrl.Substring(11),
                                Name = orWebsite.Title,
                                Url = orWebsite.ServerRelativeUrl
                            });
                        }
                        _clientDictionary.Initialize(clients);
                        _clientDictionary.Add("", new Client() { Id = "", Name = "" });
                        foreach (ListItem i in listItems)
                        {

                            var c = _clientDictionary.LookupClient((string)i["ClientID"]);

                            //var v = (string)i["ProposalID"];
                            //v += " " + (string)i["JobNumber"] ;
                            //v += " " + (string)i["ID"];
                            //v += " " + (string)i["Title"];
                           //// MessageBox.Show(v.ToString());

                            proposals.Add(new Proposal()
                            {
                                 ProposalId = (string) i["ProposalID"],
                                 Id = (Int32)i["ID"],
                                 JobNumber = (string)i["JobNumber"],
                                 Title = (string)i["Title"],
                                 ClientID = c.Id,
                                 ClientName = c.Name
                            });
                        }
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {

                            this.SetProposals(proposals.OrderBy(x => x.ClientName).ThenBy(x=>x.Title).ToList());
                            this.SetClients(clients.OrderBy(x => x.Name).ToList());

                        });
                    }, (sender, args) =>
                    {

                    });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

#endregion




    }

}
