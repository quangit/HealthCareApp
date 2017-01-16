using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class CreditCardViewModel : BaseViewModel<CreditCardViewModel>
    {
        private readonly IPaymentWS _paymentWS;
        private readonly CreditCardValidator _validator;
        private RelayCommand<CreditCardModel> _cardTappedCommand, _pickCardTappedCommand;
        private bool _isBackCardVisible = true;
        private int _itemCount;
        private Uri _urlAddCard;
        private Uri _urlVnPay, _urlVtcPay;
        private WebNavigatedEventArgs _vnPayEvent;
        private ObservableCollection<CreditCardModel> _listCreditCard;
        private string _pinOtp;
        private CreditCardModel _selectedCard, _newCard, _choosenCard;
        private AmountWithPromotionModel _selectedAmount;

        private RelayCommand _setOrChangePwd,
            _goAddCard,
            _addCardCommand,
            _doPaymentCommand,
            _goPickCardCommand,
            _addCardWebViewNavigatedCommand,
            _confirmPinOtpCommand;

        private Command _vnPayNavigatedCommand, _vnPayNavigatingCommand, _vtcPayNavigatedCommand;

        public CreditCardViewModel(INavigationService navService, IPaymentWS paymentWS, CreditCardValidator validator)
            : base(navService)
        {
            _paymentWS = paymentWS;
            _validator = validator;
        }

        #region Property

        private bool _isProcessing;
        private int _itemCountWithVNPay;
        private bool _isChoosePayByOtherMethod;
        private ObservableCollection<CreditCardModel> _listCreditCardToPick;

        public ObservableCollection<CreditCardModel> ListCreditCard
        {
            get { return _listCreditCard; }
            set
            {
                _listCreditCard = value;
                RaisePropertyChanged("ListCreditCard");
                ItemCountWithVNPay = value?.Count ?? -1;
            }
        }

        public ObservableCollection<CreditCardModel> ListCreditCardToPick
        {
            get { return _listCreditCardToPick; }
            set { _listCreditCardToPick = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<CreditCardModel> ListCreditCardNoVnPay
        {
            get
            {
                var temp =
                    _listCreditCard.FirstOrDefault(model => model.CardId == "VNPAY");
                var temp2 =
                    _listCreditCard.FirstOrDefault(model => model.CardId == "VTCPAY");
                var source = new ObservableCollection<CreditCardModel>(_listCreditCard);
                source.Remove(temp);
                source.Remove(temp2);
                ItemCount = source?.Count ?? -1;
                return source;
            }

        }

        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = value;
                RaisePropertyChanged();
            }
        }

        public int ItemCountWithVNPay
        {
            get { return _itemCountWithVNPay; }
            set
            {
                _itemCountWithVNPay = value;
                RaisePropertyChanged();
            }
        }

        public WebNavigatedEventArgs VnPayEvent
        {
            get { return _vnPayEvent; }
            set
            {
                _vnPayEvent = value;
                RaisePropertyChanged();
            }
        }

        public Uri UrlAddCard
        {
            get { return _urlAddCard; }
            set
            {
                _urlAddCard = value;
                RaisePropertyChanged("UrlAddCard");
            }
        }

        public Uri UrlVnPay
        {
            get { return _urlVnPay; }
            set
            {
                _urlVnPay = value;
                RaisePropertyChanged("UrlVnPay");
            }
        }

        public Uri UrlVtcPay
        {
            get { return _urlVtcPay; }
            set
            {
                _urlVtcPay = value;
                RaisePropertyChanged("UrlVtcPay");
            }
        }

        public CreditCardModel SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                _selectedCard = value;
                RaisePropertyChanged("SelectedCard");
            }
        }

        public CreditCardModel ChoosenCard
        {
            get { return _choosenCard; }
            set
            {
                _choosenCard = value;
                RaisePropertyChanged("ChoosenCard");
            }
        }

        //public bool IsChoosePayByOtherMethod
        //{
        //    get { return _isChoosePayByOtherMethod; }
        //    set { _isChoosePayByOtherMethod = value; RaisePropertyChanged(); }
        //}

        public CreditCardModel NewCard
        {
            get { return _newCard; }
            set
            {
                _newCard = value;
                RaisePropertyChanged("NewCard");
            }
        }

        public bool IsBackCardVisible
        {
            get { return _isBackCardVisible; }
            set
            {
                _isBackCardVisible = value;
                RaisePropertyChanged("IsBackCardVisible");
            }
        }

        public string PinOtpNumber
        {
            get { return _pinOtp; }
            set
            {
                _pinOtp = value;
                RaisePropertyChanged("PinOtpNumber");
            }
        }

        public RelayCommand ConfirmPinOtpCommand
            => _confirmPinOtpCommand ?? (_confirmPinOtpCommand = new RelayCommand(ConfirmPinOtp));

        public RelayCommand SetOrChangePassword
            => _setOrChangePwd ?? (_setOrChangePwd = new RelayCommand(SetOrChangePwd));

        public RelayCommand GoAddCard => _goAddCard ??
                                         (_goAddCard = new RelayCommand(GoAddCardPage));

        public RelayCommand AddCardCommand => _addCardCommand ??
                                              (_addCardCommand = new RelayCommand(AddNewCard));

        public RelayCommand AddCardWebViewNavigatedCommand => _addCardWebViewNavigatedCommand ??
                                                              (_addCardWebViewNavigatedCommand =
                                                                  new RelayCommand(AddNewCardWebView));

        public Command VnPayNavigatedCommand => _vnPayNavigatedCommand ??
                                                (_vnPayNavigatedCommand = new Command(VnPayNavigated));




        public RelayCommand<CreditCardModel> CardTappedCommand => _cardTappedCommand ??
                                                                  (_cardTappedCommand =
                                                                      new RelayCommand<CreditCardModel>(TapCard));

        public RelayCommand DoPaymentCommand => _doPaymentCommand ??
                                                (_doPaymentCommand = new RelayCommand(DoPayment));

        public RelayCommand GoPickCardCommand => _goPickCardCommand ??
                                                 (_goPickCardCommand = new RelayCommand(GoPaymentPickCard));

        public RelayCommand<CreditCardModel> PickCardTappedCommand => _pickCardTappedCommand ??
                                                                      (_pickCardTappedCommand =
                                                                          new RelayCommand<CreditCardModel>(ChooseCard))
            ;

        #endregion

        #region Method

        public void GoPaymentPickCard()
        {
            ListCreditCardToPick = ListCreditCard;
            NavigationService.NavigateTo(typeof(PickCreditCardPage));
        }

        public  void ChooseCard(CreditCardModel choosenData)
        {
            //SacombankService.GenerateLinkCardUrl();
            //if (choosenData.IsChoosePayByOtherMethod)
            //{
            //    await Common.AlertAsync(AppResources.payment_other_method_alert);
            //}
            ChoosenCard = choosenData;
            IsBackCardVisible = false;
            // NavigationService.GoBack();
            DoPayment();

        }

        public async void DoPayment()
        {
            try
            {
                if (ChoosenCard == null)
                    UserDialogs.Instance.Alert(AppResources.empty_payment_card);
                else
                {
                    if (ChoosenCard.Id != "Other")
                    {
                        //for creditcard
                        PasswordViewModel.Instance.SetStylePasswordPage(PasswordViewModel.EnterPaymentPwdStyle);
                        NavigationService.NavigateTo(typeof(PasswordPage));
                    }
                    else
                    {
                        //for 3th party payment
                        Common.ShowLoading();
                        AmountWithPromotionModel amount =
                            await _paymentWS.GetAmountWithPromotion(CheckupViewModel.Instance.SelectedItem.Id);
                        _selectedAmount = amount;
                        UserDialogs.Instance.HideLoading();
                        if (amount.DiscountPercent == 0)
                        {
                            if (
                                await
                                    Common.ConfirmAsync(string.Format(AppResources.amount_payment,
                                        amount.TotalAmount)))
                            {
                                if (ChoosenCard.CardId == AppConstant.VnPayCreaditCardId)
                                    await PaymentVnPay(amount.TotalAmount);
                                else if (ChoosenCard.CardId == AppConstant.VtcPayCreaditCardId)
                                {
                                    await PaymentVtcPay(CheckupViewModel.Instance.SelectedItem.Id, amount.TotalAmount);

                                }

                            }

                        }
                        else
                        {
                            if (
                                await
                                    Common.ConfirmAsync(string.Format(AppResources.amount_payment_promotion,
                                        amount.DiscountPercent,
                                        Common.GetDeviceLanguage() == "en"
                                            ? amount.Promotion.NameMap.En
                                            : amount.Promotion.NameMap.Vi, amount.TotalAmount)))
                            {
                                if (ChoosenCard.CardId == AppConstant.VnPayCreaditCardId)
                                    await PaymentVnPay(amount.TotalAmount);
                                else if (ChoosenCard.CardId == AppConstant.VtcPayCreaditCardId)
                                {
                                    await PaymentVtcPay(CheckupViewModel.Instance.SelectedItem.Id, amount.TotalAmount);
                                }
                            }

                        }


                        
                    }
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }


        // VNPAY
        private async Task PaymentVnPay(double amount)
        {
            Common.ShowLoading();
            var vnpay = new VnPayService();
            _isProcessing = false;
            var url = await vnpay.GetUrlPayment(amount);
            if (Common.GetDeviceLanguage() == "vi")
            {
                if (url.Contains("en-US"))
                {
                    url = url.Replace("en-US", "vi-VN");
                }
            }
            else
            {
                if (url.Contains("vi-VN"))
                {
                    url = url.Replace("vi-VN", "en-US");
                }
            }
            UrlVnPay = new Uri(url);
            NavigationService.NavigateTo(typeof(VnPayPaymentPage));
        }

        // VTCPAY

        private async Task PaymentVtcPay(string code, double amount)
        {
            if (amount == 0)
            {
                await _paymentWS.GetPaymentstatus(CheckupViewModel.Instance.SelectedItem.Id, _selectedAmount);
                await Task.Delay(500);
                await Common.AlertAsync(AppResources.payment_success);
                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                await UserViewModel.Instance.RefreshData(true);
            }
            else
            {
                Common.ShowLoading();
                _isProcessing = false;
                string url = VtcPayService.GeneratePaymentUrl(code, amount);
                UrlVtcPay = new Uri(url);
                NavigationService.NavigateTo(typeof(VtcPayPaymentPage));
            }
        }

        public void SetOrChangePwd()
        {
            var userVm = SimpleIocV2.Default.GetInstance<UserViewModel>();
            var passwordVm = SimpleIocV2.Default.GetInstance<PasswordViewModel>();
            passwordVm.SetStylePasswordPage(userVm.CurrentUser.IsSecure
                ? PasswordViewModel.ChangePaymentPwdStyle
                : PasswordViewModel.SetPaymentPwdStyle);
            AppConstant.IsChangePin = true;
            NavigationService.NavigateTo(typeof(PasswordPage));
        }

        public void GoAddCardPage()
        {
            //NewCard = new CreditCardModel();

            //// auto fill with current user profile
            //var curUser = UserViewModel.Instance.CurrentUser;
            //NewCard.Address = curUser.Address;
            //NewCard.BirthDay = Common.ConvertToTimestamp(curUser.BirthDay);
            //NewCard.Email = curUser.Email;
            //NewCard.FirstName = curUser.FirstName;
            //NewCard.LastName = curUser.LastName;
            //NewCard.IdNo = curUser.IdNo;
            //NewCard.PhoneNo = curUser.PhoneNo;
            UrlAddCard = new Uri(SacombankService.GenerateLinkCardUrl());
            AppConstant.IsChangePin = false;
            _isProcessing = false;
            NavigationService.NavigateTo(typeof(AddCardPage));

        }

        private void AddNewCardWebView()
        {
            UserDialogs.Instance.HideLoading();
        }

        public async void AddNewCardNavigating(WebView webView, string url)
        {
            try
            {
                UserDialogs.Instance.ShowLoading();

                if (SacombankService.IsSuccess(url) && !_isProcessing)
                {
                    webView.IsVisible = false;
                    _isProcessing = true;
                    await Common.AlertAsync(AppResources.card_add_success);
                    NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                    await RefreshCardList();
                }
                else if (SacombankService.IsFailed(url) && !_isProcessing)
                {
                    webView.IsVisible = false;
                    _isProcessing = true;
                    NavigationService.GoBack();
                    UserDialogs.Instance.HideLoading();
                }
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
                NavigationService.GoBack();
            }
        }

        private void VnPayNavigated(object o)
        {
            UserDialogs.Instance.HideLoading();

        }


        public async void VnPayNavigating(WebView webView, string url)
        {
            if (!_isProcessing)
                UserDialogs.Instance.ShowLoading();
            if (url.Contains(AppConstant.VnPayHost) && !_isProcessing)
            {
                webView.IsVisible = false;
                _isProcessing = true;
                UserDialogs.Instance.HideLoading();
                if (url.Contains("rspCode=00") || (url.Contains("rspCode=03") && url.Contains("DONGABANK")))
                {
                    await _paymentWS.GetPaymentstatus(CheckupViewModel.Instance.SelectedItem.Id, _selectedAmount);
                    await Common.AlertAsync(AppResources.payment_success);
                }
                else
                {
                    await Common.AlertAsync(AppResources.payment_fail);
                }

                //GC.Collect();
                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                await UserViewModel.Instance.RefreshData(true);
                webView.Source = new HtmlWebViewSource()
                {
                    Html = ""
                };

            }
        }

        private async Task VtcPayShowAlert(WebView wv, string alert)
        {
            _isProcessing = true;
            wv.IsVisible = false;
            UserDialogs.Instance.HideLoading();
            await Task.Delay(500);
            await Common.AlertAsync(alert);
            NavigationService.GoBack();
        }
        private async Task VtcPayShowAlertDontGoBack(WebView wv, string alert)
        {
            _isProcessing = true;
            wv.IsVisible = false;
            UserDialogs.Instance.HideLoading();
            await Common.AlertAsync(alert);
        }

        public void ProcessUrllanguageVtcPay(WebView webView, string url)
        {
            if (url.Contains("checkout.html?") && !url.Contains("&l="))
            {
                if (Common.GetDeviceLanguage() == "vi")
                {
                    url += "&l=vi";
                }
                else
                {
                    url += "&l=en";
                }
                webView.Source = url;

            }
            
        }

        public void VtcPayNavigated(WebView webView, string url)
        {
            if (true)
            {
                ProcessUrllanguageVtcPay(webView, url);
            }
            UserDialogs.Instance.HideLoading();
        }

        public async void VtcPayNavigating(WebView webView, string url)
        {
            UserDialogs.Instance.ShowLoading();

            if (Common.OS != TargetPlatform.WinPhone)
            {
                ProcessUrllanguageVtcPay(webView, url);
            }
            
            if (!_isProcessing)
            {
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
                    _isProcessing = true;
                    webView.IsVisible = false;
                    UserDialogs.Instance.HideLoading();
                    webView.Source = new HtmlWebViewSource()
                    {
                        Html = ""
                    };
                    await _paymentWS.GetPaymentstatus(CheckupViewModel.Instance.SelectedItem.Id, _selectedAmount);
                    await Task.Delay(500);
                    await Common.AlertAsync(AppResources.payment_success);
                    await UserViewModel.Instance.RefreshData(true);
                    GC.Collect();
                  
                    NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));

                    //Check condition and auto register CHBase account
                    await UserViewModel.Instance.TryToRegisterChBase(UserViewModel.Instance.CurrentUser);

                    UserDialogs.Instance.HideLoading();

                
                }
                else if (url.Contains("status=2"))
                {
                    _isProcessing = true;
                    webView.IsVisible = false;
                    await _paymentWS.GetPaymentstatus(CheckupViewModel.Instance.SelectedItem.Id, _selectedAmount);
                    UserDialogs.Instance.HideLoading();
                    webView.Source = new HtmlWebViewSource()
                    {
                        Html = ""
                    };
                    await Task.Delay(500);
                    await Common.AlertAsync(AppResources.payment_success);
                    await UserViewModel.Instance.RefreshData(true);
                    GC.Collect();
                    NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));

                    //Check condition and auto register CHBase account
                    await UserViewModel.Instance.TryToRegisterChBase(UserViewModel.Instance.CurrentUser);

                    UserDialogs.Instance.HideLoading();

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
        }

        public async void AddNewCard()
        {
            try
            {
                Common.ShowLoading();
                var result = _validator.Validate(NewCard);
                if (!result.IsValid)
                {
                    await Common.AlertAsync(result.Errors[0]);
                    return;
                }
                await _paymentWS.AddCard(NewCard);
                NavigationService.NavigateTo(typeof(ConfirmCreditCardPage));
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        public void TapCard(CreditCardModel item)
        {
            SelectedCard = item;
            NavigationService.NavigateTo(typeof(CardDetailPage));
        }

        public async Task RefreshCardList()
        {
            Common.ShowLoading();
            //ListCreditCard = await _paymentWS.GetCardList();
            ListCreditCard = await _paymentWS.GetCardListv2();
            UserDialogs.Instance.HideLoading();
        }

        public async void ConfirmPinOtp()
        {
            try
            {
                var result = _validator.ValidatePinOtp(PinOtpNumber, NewCard);
                if (!result.IsValid)
                {
                    await Common.AlertAsync(result.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                await _paymentWS.VerifyPinOtp(NewCard.CardId, PinOtpNumber);
                await Common.AlertAsync(AppResources.card_add_success);
                //ToolbarViewModel.Instance.SetSelectedPage(typeof(CreditCardPage));
                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                await RefreshCardList();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
        }

        public void ClearViewModel()
        {
            IsBackCardVisible = true;
        }

        #endregion
    }
}