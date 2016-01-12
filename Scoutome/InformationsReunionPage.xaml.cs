using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Scoutome.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Scoutome
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InformationsReunionPage : Page
    {
        public InformationsReunionPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((InformationsReunionViewModel)DataContext).OnNavigatedTo(e);
        }
    }
}
