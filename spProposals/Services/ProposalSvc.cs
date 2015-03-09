//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Data.Services.Client;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.SharePoint.Client;
//using spProposals.Models;

//namespace spProposals.Services
//{
//    public static class ProposalSvc
//    {

//        public static async Task<ObservableCollection<Proposal>> GetProposals()
//        {
//            var wqs = new ObservableCollection<Proposal>();

//            wqs = new ObservableCollection<Proposal>(t.ToList());
//            return wqs;
//        }


//    }

//}   protected ClientContext spContext;

//        // The actual Grid will be generated dynamically, but this reference will need to be shared.
//        private Grid listGrid;
//        private ClientDictionary _clientDictionary = new ClientDictionary();
//        private List<Proposal> _proposals = new List<Proposal>();


//        /// <summary>
//        /// The applicaton's constructor.  Initializes the SharePoint context, and populates the drop-down list picker.
//        /// </summary>
//        public MainPage()
//        {
//            InitializeComponent();
//            LoadAll();
//        }

//        private void LoadAll()
//        {
//            try
//            {
//                // Initialize the context.  It is easier to debug if we point to a particular test server.
//                // However, for the release build, the application uses the current SharePoint context, no matter
//                // what site it is on.  Also note that for the direct client OM connection to work, the target site
//                // needs a properly configured clientaccesspolicy.xml file.
//#if DEBUG
//                spContext = new ClientContext("http://home.reckner.com/BlueBerry/");
//#else
//                        spContext = ClientContext.Current;
//            #endif

//                Web oWebsite = spContext.Web;
//                var clientwebs = oWebsite.Webs;
//                spContext.Load(oWebsite);
//                spContext.Load(clientwebs);
//                List plist = oWebsite.Lists.GetByTitle("Proposals");
//                var camlQuery = CamlQuery.CreateAllItemsQuery();
//                //FieldCollection fieldColl = plist.Fields;
//                //spContext.Load(fieldColl);
//                ListItemCollection listItems = plist.GetItems(camlQuery);
//                spContext.Load(listItems);

//                spContext.ExecuteQueryAsync(
//                    (sender, args) =>
//                    {
//                        var z = new List<Client>();
//                        var Names = new List<String>();
//                        var proposals = new List<Proposal>();
//                        foreach (Web orWebsite in clientwebs)
//                        {
//                            z.Add(new Client
//                            {
//                                Id = orWebsite.ServerRelativeUrl.Substring(11),
//                                Name = orWebsite.Title,
//                                Url = orWebsite.ServerRelativeUrl
//                            });
//                            Names.Add(orWebsite.Title);
//                        }
//                        _clientDictionary.Initialize(z);
//                        Client c = new Client() { Id = "", Name = "" };
//                        _clientDictionary.Add(c.Id, c);

//                        foreach (ListItem i in listItems)
//                        {

//                            c = _clientDictionary.LookupClient((string)i["ClientID"]);

//                           // var v = (string)i["ProposalID"];
//                           // v += " " + i["JobNumber"] != null ? i["JobNumber"] : "";
//                           // v += " " + (string)i["Title"];
//                           //// MessageBox.Show(v.ToString());

//                            proposals.Add(new Proposal()
//                            {
//                                 Id = (string) i["ProposalID"],
//                                 JobNumber = (string)i["JobNumber"],
//                                 Title = (string)i["Title"],
//                                 ClientID = c.Id,
//                                 ClientName = c.Name
//                            });
//                        }

//                        Dispatcher.BeginInvoke(() =>
//                        {
//                            ComboBox_ListPicker.ItemsSource = Names;
//                            _proposals = proposals;
//                        });


//                    }, (sender, args) =>
//                    {

//                    });
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.ToString());
//            }
//        }


//    }
//}
