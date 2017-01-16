using System.Threading.Tasks;
using HealthCare.Controls;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class CheckupDetailPage : TopBarContentPage
    {
        public CheckupDetailPage()
        {
            InitializeComponent();
        }

        public static Color ViewColor { get; set; }
    }
}