using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.SharePoint.Client;
using spProposals.Models;
using spProposals.Services;
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
            MessageBox.Show(@"This will move the proposal to the client/archive/year folder for example: Blueberry\scj\Archive\2015\ProposalName. The proposal will still show up in the proposals list but as 'Archived'. The name will stay the same. This action works only with proposals not jobs");
        }

        private void btnCopy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("This will copy the proposal to create a new proposal under this client or another client. A page will be displayed for the selection of the client (default to the current one) and to rename the proposal. This action works with proposals not jobs. The archived proposal will be tucked into a subsite Archive/Year within the clients proposal list on the top of the client screen.");
        }
    }
}
