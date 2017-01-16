using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HealthCare.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConsultView : Page
    {
        public ConsultView()
        {
            this.InitializeComponent();
            this.Loaded += ConsultView_Loaded;
        }

        private void ConsultView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void SkypeClicked(object sender, RoutedEventArgs e)
        {
            var vm = ((ConsultViewModel)DataContext);
            await Launcher.LaunchUriAsync(new Uri(vm.Request.PatientSkypeUrl));
        }

        private void SkypeLoad(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
