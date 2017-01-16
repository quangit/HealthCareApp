using HealthCare.Controls;
using HealthCare.ViewModels;

namespace HealthCare.Pages
{
    public partial class MedicalRecordPage : TopBarContentPage
    {
        public MedicalRecordPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CheckupViewModel.Instance.GetHistoricalCheckupList();
        }
    }
}