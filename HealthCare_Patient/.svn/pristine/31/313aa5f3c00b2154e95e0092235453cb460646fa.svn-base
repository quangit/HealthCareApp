using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.ViewModels.CHBases;
using Xamarin.Forms;

namespace HealthCare.Pages.CHBases
{
    public partial class PaymentCheckupRecordPage : TopBarContentPage
    {
        public PaymentCheckupRecordPage()
        {
            InitializeComponent();
        }
        private async void BtnTransfer_OnClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(AppResources.choosing_payment_method,AppResources.cancel, null,AppResources.transfer,AppResources.payment_via_vtcpay);
            if (action.Equals(AppResources.transfer))
            {
                var detailTransfer = new DetailTransfer();
                //detailPage.BindingContext = e.SelectedItem as Contact;
                //SimpleIocV2.Default.GetInstance<INavigationService>().NavigateTo();
                await Navigation.PushModalAsync(detailTransfer);
            }
            if (action.Equals(AppResources.payment_via_vtcpay))
            {
                PaymentViewModel.Instance.PaymentVtcPayCommand();
            }
            
        }
    }
}
