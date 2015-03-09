using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Reckner.Silverlight.SharePoint.Core.Models;
using spProposals.ViewModels;
using share
namespace spProposals.Views
{
    public partial class ProposalsView : UserControl
    {
        private ProposalsViewModel _vm;
        

        public ProposalsView()
        {
            InitializeComponent();
            _vm = new ProposalsViewModel();
            DataContext = _vm;
            cbxClients.ItemsSource = _vm.Clients;
        }

        private void btnJob_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            cbxClients.ItemsSource = _vm.Clients;
            // MessageBox.Show("This will perform the same action as 'convert to a job' on the proposal web page. This only works with proposals not jobs");
            MessageBox.Show(_vm.Clients.Count.ToString());
        }

        private void btnArchive_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show(@"This will move the proposal to the client/archive/year folder for example: Blueberry\scj\Archive\2015\ProposalName. The proposal will still show up in the proposals list but as 'Archived'. The name will stay the same. This action works only with proposals not jobs");
        }

        private void btnCopy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("This will copy the proposal to create a new proposal under this client or another client. A page will be displayed for the selection of the client (default to the current one) and to rename the proposal. This action works with proposals not jobs. The archived proposal will be tucked into a subsite Archive/Year within the clients proposal list on the top of the client screen.");
        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(_vm.SelectedProposal.IsJob.ToString() + "  " + _vm.SelectedProposal.Title);
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
            }
        }

        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;

            }
        }
       

    }
}
