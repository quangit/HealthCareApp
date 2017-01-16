using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using HealthCare.Core.ViewModels;
using Windows.UI.Xaml.Navigation;
using Template10.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views.HomeTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CheckupsView : Page
    {
        public HomeViewModel ViewModel { get; set; }
        public CheckupsView()
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

            var r = await ViewModel.LoadCheckups();

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
            await ViewModel.LoadCheckups(true);
            s.Stop();

        }
        private void UpdateSize(object sender)
        {
            var panel = (sender) as ItemsWrapGrid;
            if (panel != null)
            {
                var i = panel.Children.Count;
                var size = panel.ActualWidth / i;
                Debug.WriteLine(panel.ActualWidth + ":" + size);
                while (size < 400)
                {
                    size = panel.ActualWidth / (--i);
                }
                panel.ItemWidth = panel.ActualWidth / (i);
            }
        }
        private void ItemsWrapGridLoaded(object sender, RoutedEventArgs e)
        {
            var panel = (sender) as ItemsWrapGrid;

            if (panel != null)
            {
                if (panel.Children.Count == 0)
                    return;
                panel.LayoutUpdated += (o, a) => UpdateSize(panel);
                panel.SizeChanged += (o, a) => UpdateSize(panel);
                panel.Loaded -= ItemsWrapGridLoaded;
                UpdateSize(sender);
            }
        }

        private void FrameworkElement_OnLayoutUpdated(object sender, object e)
        {
            Debug.WriteLine("abc");
        }

        private void Grid_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
    }
}
