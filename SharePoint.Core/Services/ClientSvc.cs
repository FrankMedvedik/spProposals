//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Threading;
//using Microsoft.SharePoint.Client;
//using Reckner.Silverlight.SharePoint.Core.Models;

//namespace Reckner.Silverlight.SharePoint.Core.Services
//{
//    public static class SpClientSvc
//    {
//        public static ObservableCollection<Client> GetAllClients(String sitePath)
//        {
//            var c = new ObservableCollection<Client>();
//            var clientContext = new ClientContext(new Uri(sitePath));
//            Web oWebsite = clientContext.Web;
//            var clientwebs = oWebsite.Webs;
//            clientContext.Load(oWebsite );
//            clientContext.Load(clientwebs);
//            clientContext.ExecuteQueryAsync(
//                (sender, args) =>
//                {
//                    var z = new List<Client>();
//                    foreach (Web orWebsite in clientwebs)
//                    {
//                        z.Add(new Client()
//                        {
//                            Id = orWebsite.ServerRelativeUrl,
//                            Name = orWebsite.Title,
//                            Url = orWebsite.ServerRelativeUrl
//                        });
//                    }
//                    Deployment.Current.Dispatcher.BeginInvoke(() => { c = new ObservableCollection<Client>(z); });

//                },(sender, args) =>
//                {
                  
//                });
            
//        }

//        private static void failedCallback(object sender, ClientRequestFailedEventArgs args)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
