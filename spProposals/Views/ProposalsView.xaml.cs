using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using spProposals.ViewModels;
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
        }

        private void btnJob_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // MessageBox.Show("This will perform the same action as 'convert to a job' on the proposal web page. This only works with proposals not jobs");
            MessageBox.Show(_vm.Clients.Count.ToString());
        }

        private void btnArchive_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var workUrl = @SpProperties.BlueBerryHomeUrl + @"SitePages/ArchivePage.aspx?ClientId=" + _vm.SelectedProposal.ClientSiteName +
                "&ProposalID=" + _vm.SelectedProposal.ProposalId + "&ProposalsItemId=" + _vm.SelectedProposal.Id; ;
            //MessageBox.Show(@"This will move " + workUrl + " to the " + _vm.SelectedProposal.ClientID +  "/archive/year folder");

            HtmlPage.Window.Navigate(new Uri(workUrl, UriKind.Absolute), "_blank");

        }

        private void btnCopy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("This will copy the proposal to create a new proposal under this client or another client. A page will be displayed for the selection of the client (default to the current one) and to rename the proposal. This action works with proposals not jobs. The archived proposal will be tucked into a subsite Archive/Year within the clients proposal list on the top of the client screen.");
        }
    }
}
