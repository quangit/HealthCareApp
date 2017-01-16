using HealthCare.Controls;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class DoctorSchedulePage : BaseAddCheckupPage
    {
        public DoctorSchedulePage()
        {
            InitializeComponent();
            if (Common.OS == TargetPlatform.iOS && Device.Idiom == TargetIdiom.Tablet)
                Calendar.HorizontalOptions = LayoutOptions.CenterAndExpand;
        }
    }
}