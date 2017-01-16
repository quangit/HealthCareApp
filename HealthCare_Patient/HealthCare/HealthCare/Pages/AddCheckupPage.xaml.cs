using HealthCare.Controls;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class AddCheckupPage : BaseAddCheckupPage
    {
        public AddCheckupPage()
        {
            InitializeComponent();
            if (Common.OS == TargetPlatform.iOS)
            {
                //checkBox.WidthRequest = 35;
                checkBox.HeightRequest = 35;
            }
        }
    }
}