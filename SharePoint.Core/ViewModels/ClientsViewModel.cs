using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.SharePoint.Client;
using Reckner.Silverlight.SharePoint.Core.Models;

namespace Reckner.Silverlight.SharePoint.Core.ViewModels
{
    public abstract class ThisViewModelBase : INotifyPropertyChanged
    {

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            var e = PropertyChanged;
            if (e != null) e(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ClientsViewModel : ThisViewModelBase
    {
        private static Uri _siteUri;

        public ClientsViewModel(string BaseUrl)
        {
            _siteUri = new Uri(BaseUrl);
            RefreshAll();
        }

        protected  void RefreshAll()
        {
            GetAllClients();
        }

        private void GetAllClients()
        {
            
            var clientContext = new ClientContext(_siteUri);
            Web oWebsite = clientContext.Web;
            var clientwebs = oWebsite.Webs;
            clientContext.Load(oWebsite);
            clientContext.Load(clientwebs);
            clientContext.ExecuteQueryAsync(
                (sender, args) =>
                {
                    var z = new ObservableCollection<Client>();
                    foreach (Web orWebsite in clientwebs)
                    {
                        z.Add(new Client()
                        {
                            Id = orWebsite.ServerRelativeUrl.Substring(11),
                            Name = orWebsite.Title,
                            Url = orWebsite.ServerRelativeUrl
                        });
                    }
                    Clients = z;
                }, (sender, args) =>
                {

                });

        }

        private string _selectedClientId;
        public string SelectedClientId
        {
            get { return _selectedClientId; }
            set
            {
                _selectedClientId = value;
                NotifyPropertyChanged();
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

    }
}
