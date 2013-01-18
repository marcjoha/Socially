using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Socially.Views.UserControls
{
    /// <summary>
    ///     Interaction logic for ExternalResource.xaml
    /// </summary>
    public partial class ExternalResource : UserControl
    {
        public ExternalResource()
        {
            InitializeComponent();
        }

        private void HandleRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var hl = (Hyperlink) sender;
            string navigateUri = hl.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
        }
    }
}