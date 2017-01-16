using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class ProfilePage : TopBarContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            if(Common.OS == TargetPlatform.iOS)
            StackProfile.WidthRequest = 150;
        }

        protected override bool OnBackButtonPressed()
        {
            UserViewModel.Instance.HandleHardwareBackButton();
            return true;
        }
    }
}