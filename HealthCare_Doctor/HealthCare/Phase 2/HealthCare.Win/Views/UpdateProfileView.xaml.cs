using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using HealthCare.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateProfileView : Page
    {
        public UpdateProfileViewModel ViewModel => this.DataContext as UpdateProfileViewModel;
        public UpdateProfileView()
        {
            this.InitializeComponent();

            //GenderTextBox.AddHandler(TappedEvent, new TappedEventHandler(UIElement_OnTapped), true);
        }


        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.GenderCommand.Execute();
        }
    }
}
