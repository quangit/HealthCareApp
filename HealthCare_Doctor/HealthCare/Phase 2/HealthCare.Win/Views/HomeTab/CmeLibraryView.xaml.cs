using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HealthCare.Core.ViewModels;
using Windows.UI.Xaml.Navigation;
using Template10.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views.HomeTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CmeLibraryView : Page
    {
        public HomeViewModel ViewModel { get; set; }
        public CmeLibraryView()
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
        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowViewModel<CmeSearchViewModel>();
        }
    }
}
