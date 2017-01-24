using System.Threading.Tasks;
using HealthCare.Controls;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class LoginPage : ContentPageNoActionBar
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this,false);
            InitializeComponent();                     
        }
    }
}