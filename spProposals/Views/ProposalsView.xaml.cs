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

        }

        private void btnArchive_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

    }
}
