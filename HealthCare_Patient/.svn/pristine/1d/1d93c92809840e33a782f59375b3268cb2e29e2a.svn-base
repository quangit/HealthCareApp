using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HealthCare.Helpers;
using HealthCare.Models.ChBaseModel;
using HealthCare.Pages;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;
using XLabs;

namespace HealthCare.ViewModels.CHBases
{
    public class PaymentViewModel : BaseViewModel<PaymentViewModel>
    {
        public CHBasePatientInfoModel _chBasePatientInfo;

        private ListFees _listFees;
        private DetailPackageFeeModel _detailPackageFee;
        private readonly IPatientCHBaseWS _patientChBaseWs;
        private int _selectedPackageFee = 2;
        private FeeModel _selectedFee;
        private string promotion;

        public PaymentViewModel(INavigationService navigationService, IPatientCHBaseWS patientChBaseWs) : base(navigationService)
        {
            _patientChBaseWs = patientChBaseWs;
         //   GetPackageFeeInfo();
        }

        public string Promotion
        {
            get { return promotion; }
            set
            {
                promotion = value;
                RaisePropertyChanged();
            }
        }


        public string FullName
        {
            get { return UserViewModel.Instance.CurrentUser.FullName; }
        }

        public string Email
        {
            get { return UserViewModel.Instance.CurrentUser.Email; }
        }


        public ObservableCollection<FeeModel> Fees
        {
            get
            {
                if (_listFees != null) return _listFees.Fees;
                return null;
            }
            set
            {
                if (value != null)
                {
                    _listFees.Fees = value;
                    RaisePropertyChanged();
                }
            }
        }



        public DetailPackageFeeModel DetailPackageFee
        {
            get { return _detailPackageFee; }
            set
            {
                if (value != null)
                {
                    _detailPackageFee = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ListFees listFee
        {
            get { return _listFees; }

            set
            {
                if (value != null)
                {
                    _listFees = value;
                    RaisePropertyChanged("Fees");
                }
            }
        }

        public int SelectedPackageFee
        {
            get { return _selectedPackageFee; }
            set
            {
                _selectedPackageFee = value;
            }
        }

        //public string Email
        //{
        //    get { return _chBasePatientInfo.}
        //}


        public async void GetPackageFeeInfo()
        {
            listFee = await _patientChBaseWs.GetPackageInfo();
            Common.ShowLoading();
            if (listFee.Fees.Count > 0)
            {
                _selectedFee = listFee.Fees.First();
                GetDetailAmountPackage(_selectedFee.Id);
            }
            Common.HideLoading();
        }

        public async void GetDetailAmountPackage(string idPackageFee)
        {
            DetailPackageFee = await _patientChBaseWs.GetPackageFeeDetail(idPackageFee);
        }

        public async void PaymentVtcPayCommand()
        {
            await PaymentVtcPay(Guid.NewGuid().ToString(), _detailPackageFee.Amount);
        }

        private async Task PaymentVtcPay(string code, double amount)
        {
            if (amount == 0)
            {

            }
            else
            {
                Common.ShowLoading();
                string url = VtcPayService.GeneratePaymentUrl(code, amount);
                CreditCardViewModel.Instance.UrlVtcPay = new Uri(url);
                NavigationService.NavigateTo(typeof(HealthCare.Pages.CHBases.VtcPayPaymentPage));
            }
        }
        private async Task VtcPayShowAlert(WebView wv, string alert)
        {
            wv.IsVisible = false;
            UserDialogs.Instance.HideLoading();
            await Task.Delay(500);
            await Common.AlertAsync(alert);
            NavigationService.GoBack();
        }

        private async Task VtcPayShowAlertDontGoBack(WebView wv, string alert)
        {
            wv.IsVisible = false;
            UserDialogs.Instance.HideLoading();
            await Common.AlertAsync(alert);
        }

        public async void VtcPayNavigating(WebView webView, string url)
        {
            UserDialogs.Instance.ShowLoading();

            if (url.Contains("status=-1"))
            {
                await VtcPayShowAlert(webView, AppResources.Transaction_cancelled);
            }
            else if (url.Contains("status=-5"))
            {
                await VtcPayShowAlert(webView, AppResources.Transaction_failed_code);
            }
            else if (url.Contains("status=-6"))
            {
                await VtcPayShowAlert(webView, AppResources.Transaction_failed_acc_balance);
            }
            else if (url.Contains("status=-99"))
            {
                await VtcPayShowAlert(webView, AppResources.System_error_occurred);
            }
            else if (url.Contains("status=1"))
            {
                webView.IsVisible = false;
                webView.Source = new HtmlWebViewSource()
                {
                    Html = ""
                };
                var data = await _patientChBaseWs.PurchaseCHBasePackage(_selectedFee.Id);
                if (data != null)
                {
                    ChBaseViewModel.Instance.ChBasePatientInfo.WhenExpired = data.WhenExpired;
                    await Task.Delay(500);
                    await Common.AlertAsync(AppResources.payment_success);
                    GC.Collect();
                    //NavigationService.ReplaceRootPage(typeof(HealthRecordInformationPage));
                    NavigationService.GoBack();
                    NavigationService.GoBack();

                }
                UserDialogs.Instance.HideLoading();
            }
            else if (url.Contains("status=2"))
            {
                webView.IsVisible = false;
                //await _paymentWS.GetPaymentstatus(CheckupViewModel.Instance.SelectedItem.Id, _selectedAmount);
                var data = await _patientChBaseWs.PurchaseCHBasePackage(_selectedFee.Id);
                if (data != null)
                {
                    ChBaseViewModel.Instance.ChBasePatientInfo.WhenExpired = data.WhenExpired;

                    UserDialogs.Instance.HideLoading();
                    webView.Source = new HtmlWebViewSource()
                    {
                        Html = ""
                    };
                    await Task.Delay(500);
                    await Common.AlertAsync(AppResources.payment_success);
                    GC.Collect();
                    //NavigationService.ReplaceRootPage(typeof(HealthRecordInformationPage));
                    NavigationService.GoBack();
                    NavigationService.GoBack();
                    UserDialogs.Instance.HideLoading();
                }
            }
            else if (url.Contains("status=7"))
            {
                await VtcPayShowAlertDontGoBack(webView, AppResources.Transaction_pending);
            }
            else if (url.Contains("status=0"))
            {
                await VtcPayShowAlertDontGoBack(webView, AppResources.Transaction_processing);
            }

        }


        public RelayCommand<FeeModel> ChangeSelectedPackageFee => new RelayCommand<FeeModel>(item =>
       {
           GetDetailAmountPackage(item.Id);
           _selectedFee = item;
       });

        public RelayCommand PaymentByVTCCommand => new RelayCommand(() =>
        {
            PaymentVtcPay(Guid.NewGuid().ToString(), _detailPackageFee.Amount);
        });

        public RelayCommand ApplyPromotionCommand => new RelayCommand(async () =>
        {
            if (!string.IsNullOrEmpty(Promotion))
            {
                Common.ShowLoading();
                GetDetailAmountPackage(_selectedFee.Id);
                Common.HideLoading();
            }
            else
            {
                await Common.AlertAsync(AppResources.empty_promotion_code);
            }
        });


        public class ListFees
        {
            public int total { get; set; }
            public ObservableCollection<FeeModel> Fees { get; set; }
        }
    }
}
