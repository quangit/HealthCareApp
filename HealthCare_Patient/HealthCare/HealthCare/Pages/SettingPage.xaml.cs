using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class SettingPage : TopBarContentPage
    {
        private LoginCHBaseControl loginChBaseControl;
        public SettingPage()
        {
            InitializeComponent();
            SettingViewModel.Instance.WebViewJavascript = null;
            var grid =(Grid) this.Content;
            loginChBaseControl = new LoginCHBaseControl();
            grid.Children.Add(loginChBaseControl);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(Common.OS == TargetPlatform.WinPhone)
            if (loginChBaseControl != null)
            {
                var grid = (Grid)this.Content;
                grid.Children.Remove(loginChBaseControl);
                loginChBaseControl = new LoginCHBaseControl();
                grid.Children.Add(loginChBaseControl);
            }
        }

        protected override void OnDisappearing()
        {
            SettingViewModel.Instance.HidePopup();
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            if (SettingViewModel.Instance.ShowPopup)
            {
                SettingViewModel.Instance.HidePopup();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}