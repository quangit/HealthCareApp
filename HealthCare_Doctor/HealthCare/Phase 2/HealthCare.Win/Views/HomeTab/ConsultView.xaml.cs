using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using HealthCare.Core.ViewModels;
using Template10.Common;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views.HomeTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConsultView : Page
    {
        public HomeViewModel ViewModel { get; set; }
        public ConsultView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel = Shell.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            BootStrapper.Current.NavigationService.Frame.BackStack.Clear();
            BootStrapper.Current.NavigationService.ClearHistory();
            BootStrapper.Current.NavigationService.ClearCache(true);
        }
        private async void OnDataRequested(TaskCompletionSource<bool> obj)
        {

            var r = await ViewModel.LoadSupportRequests();
            obj.SetResult(r);
        }

        private Storyboard s;
        private void AppBarButtonLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var grid = sender as Grid;
            s = grid.Resources["Storyboard"] as Storyboard;
            s.RepeatBehavior = RepeatBehavior.Forever;

        }

        private async void RefreshClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            s.Begin();
            await ViewModel.LoadSupportRequests(true);
            s.Stop();

        }
    }
}
