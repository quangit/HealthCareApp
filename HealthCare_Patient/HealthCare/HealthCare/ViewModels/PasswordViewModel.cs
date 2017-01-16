using System;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Exceptions;
using HealthCare.Helpers;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class PasswordViewModel : BaseViewModel<PasswordViewModel>
    {
        public const short SetPaymentPwdStyle = 1;
        public const short ChangePaymentPwdStyle = 2;
        public const short EnterPaymentPwdStyle = 3;
        public const short ChangeUserPwdStyle = 4;
        private readonly CommonValidator _commonValidator;
        private readonly CreditCardValidator _creditCardValidator;
        private readonly IPaymentWS _paymentWS;
        private readonly IUserWS _userWS;
        private string _curPwd, _newPwd, _confirmPwd;
        private string _emailRecoveryPwd;
        private bool _isCurPwdVisible, _isNewPwdVisible, _isConfirmPwdVisible, _isChangePin, _isNotChangePin;
        private RelayCommand _submitForgotPwdCommand, _setPassWebViewNavigatedCommand;
        private string _title, _description, _errorMsg, _urlSetPass;

        public PasswordViewModel(INavigationService ns, IUserWS userWS, IPaymentWS paymentWS,
            CreditCardValidator creditCardValidator, CommonValidator commonValidator)
            : base(ns)
        {
            _userWS = userWS;
            _paymentWS = paymentWS;
            _creditCardValidator = creditCardValidator;
            _commonValidator = commonValidator;

        }

        #region Property

        public bool IsChangePin
        {
            get { return _isChangePin; }
            set
            {
                _isChangePin = value;
                RaisePropertyChanged("IsChangePin");
            }
        }
        public bool IsNotChangePin
        {
            get { return !_isChangePin; }
            set
            {
                _isNotChangePin = value;
                RaisePropertyChanged();
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
        public string UrlSetPass
        {
            get { return _urlSetPass; }
            set
            {
                _urlSetPass = value;
                RaisePropertyChanged("UrlSetPass");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                RaisePropertyChanged("ErrorMsg");
            }
        }

        public string CurPassword
        {
            get { return _curPwd; }
            set
            {
                _curPwd = value;
                RaisePropertyChanged("CurPassword");
            }
        }

        public string NewPassword
        {
            get { return _newPwd; }
            set
            {
                _newPwd = value;
                RaisePropertyChanged("NewPassword");
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPwd; }
            set
            {
                _confirmPwd = value;
                RaisePropertyChanged("ConfirmPassword");
            }
        }

        public bool IsCurPwdVisible
        {
            get { return _isCurPwdVisible; }
            set
            {
                _isCurPwdVisible = value;
                RaisePropertyChanged("IsCurPwdVisible");
            }
        }

        public bool IsNewPwdVisible
        {
            get { return _isNewPwdVisible; }
            set
            {
                _isNewPwdVisible = value;
                RaisePropertyChanged("IsNewPwdVisible");
            }
        }

        public bool IsConfirmPwdVisible
        {
            get { return _isConfirmPwdVisible; }
            set
            {
                _isConfirmPwdVisible = value;
                RaisePropertyChanged("IsConfirmPwdVisible");
            }
        }

        public RelayCommand CancelCommand { protected set; get; }

        public RelayCommand OkCommand { protected set; get; }

        public string EmailRecoveryPassword
        {
            get { return _emailRecoveryPwd; }
            set
            {
                _emailRecoveryPwd = value;
                RaisePropertyChanged("EmailRecoveryPassword");
            }
        }

        public RelayCommand SubmitForgotPwdCommand => _submitForgotPwdCommand ??
                                                      (_submitForgotPwdCommand = new RelayCommand(SubmitForgotPassword))
            ;

        public RelayCommand SetPassWebViewNavigatedCommand => _setPassWebViewNavigatedCommand ??
                                                      (_setPassWebViewNavigatedCommand = new RelayCommand(SetPassWebViewNavigated))
            ;


        #endregion

        #region Method
        public void ChangePinNavigating(WebView webView, string url)
        {
            UserDialogs.Instance.ShowLoading();

            if (SacombankService.IsSuccess(url))
            {

                webView.IsVisible = false;
                NavigationService.GoBack();
                UserDialogs.Instance.HideLoading();

            }
            else if (SacombankService.IsFailed(url))
            {
                webView.IsVisible = false;
                NavigationService.GoBack();
                UserDialogs.Instance.HideLoading();

            }
        }
        private void SetPassWebViewNavigated()
        {
            UserDialogs.Instance.HideLoading();

        }
        public void SetStylePasswordPage(short style)
        {
            CurPassword =
                NewPassword = ConfirmPassword = EmailRecoveryPassword = ErrorMsg = Description = Title = string.Empty;
            switch (style)
            {
                case SetPaymentPwdStyle:
                    UrlSetPass = SacombankService.GenerateChangePinUrl();
                    Title = AppResources.payment_set_password;
                    Description = AppResources.payment_set_password_description;
                    IsCurPwdVisible = false;
                    IsNewPwdVisible = true;
                    IsChangePin = true;
                    IsConfirmPwdVisible = true;
                    OkCommand = new RelayCommand(SetNewPaymentPwdCommand);
                    break;
                case ChangePaymentPwdStyle:
                    UrlSetPass = SacombankService.GenerateChangePinUrl();
                    Title = AppResources.payment_change_password;
                    Description = AppResources.payment_set_password_description;
                    IsCurPwdVisible = true;
                    IsNewPwdVisible = true;
                    IsChangePin = true;
                    IsConfirmPwdVisible = true;
                    OkCommand = new RelayCommand(ChangePaymentPwdCommand);
                    break;
                case EnterPaymentPwdStyle:
                    Title = AppResources.paymet_enter_password;
                    Description = AppResources.empty_payment_enter_password;
                    IsCurPwdVisible = true;
                    IsNewPwdVisible = false;
                    IsConfirmPwdVisible = false;
                    IsChangePin = false;
                    OkCommand = new RelayCommand(EnterPaymentPwdCommand);
                    break;
                case ChangeUserPwdStyle:
                    Title = AppResources.change_password;
                    Description = AppResources.change_password_description;
                    IsCurPwdVisible = true;
                    IsNewPwdVisible = true;
                    IsChangePin = false;
                    IsConfirmPwdVisible = true;
                    OkCommand = new RelayCommand(ChangeUserPwdCommand);
                    break;
                default:
                    break;
            }
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }

        public void ExecuteCancelCommand()
        {
            NavigationService.GoBack();
        }

        public async void SetNewPaymentPwdCommand()
        {
            try
            {
                var valdateResult = _creditCardValidator.ValidateSetPaymentPassword(NewPassword, ConfirmPassword);

                if (!valdateResult.IsValid)
                {
                    await Common.AlertAsync(valdateResult.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                await _paymentWS.SetPaymentPassword("", ConfirmPassword);
                await Common.AlertAsync(AppResources.payment_set_password_success);
                UserViewModel.Instance.CurrentUser.IsSecure = true;
                NavigationService.GoBack();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        public async void ChangePaymentPwdCommand()
        {
            try
            {
                var valdateResult = _commonValidator.ValidateChangePassword(CurPassword, NewPassword,
                    ConfirmPassword, false);

                if (!valdateResult.IsValid)
                {
                    await Common.AlertAsync(valdateResult.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                await _paymentWS.SetPaymentPassword(CurPassword, NewPassword);
                await Common.AlertAsync(AppResources.payment_change_password_success);
                NavigationService.GoBack();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        public async void ChangeUserPwdCommand()
        {
            try
            {
                var resultVal = _commonValidator.ValidateChangePassword(CurPassword, NewPassword, ConfirmPassword, true);
                if (!resultVal.IsValid)
                {
                    await Common.AlertAsync(resultVal.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                await _userWS.ChangeUserPassword(UserViewModel.Instance.CurrentUser.Id, CurPassword, NewPassword);

                var cloneUser = UserViewModel.Instance.CurrentUser;
                cloneUser.Password = NewPassword;
                UserViewModel.Instance.SaveUseInfo(cloneUser);

                await Common.AlertAsync(AppResources.change_password_success);
                NavigationService.GoBack();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        public async void EnterPaymentPwdCommand()
        {
            try
            {
                var checkupId = CheckupViewModel.Instance.SelectedItem.Id;
                var cardId = CreditCardViewModel.Instance.ChoosenCard.CardId;
                var cardToken = CreditCardViewModel.Instance.ChoosenCard.CardToken;

                var valdateResult = _creditCardValidator.ValidateEnterPaymentPassword(CurPassword);

                if (!valdateResult.IsValid)
                {
                    await Common.AlertAsync(valdateResult.Errors[0]);
                    return;
                }

                Common.ShowLoading();
                var promotionInfo = await _paymentWS.DoPayment(checkupId, cardId, CurPassword, cardToken);
                if (promotionInfo == null || promotionInfo.Promotion == null ||
                    promotionInfo.Promotion.DiscountPercent == 0)
                    await Common.AlertAsync(AppResources.payment_success);
                else
                    await Common.AlertAsync(string.Format(AppResources.payment_applied_promotion,
                        promotionInfo.Promotion?.DiscountPercent, promotionInfo.Total));

                await UserViewModel.Instance.RefreshData(true);
                GC.Collect();

                //Check condition and auto register CHBase account
                await UserViewModel.Instance.TryToRegisterChBase(UserViewModel.Instance.CurrentUser);

                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                UserDialogs.Instance.HideLoading();

            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        public async void SubmitForgotPassword()
        {
            try
            {
                var resultVal = _commonValidator.ValidateUserEmail(EmailRecoveryPassword);
                if (!resultVal.IsValid)
                {
                    UserDialogs.Instance.HideLoading();
                    await Common.AlertAsync(resultVal.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                await _userWS.ResetPassword(EmailRecoveryPassword);
                await Common.AlertAsync(AppResources.reset_password_success);
                NavigationService.GoBack();
            }
            catch (ApiException ex)
            {
                if (ex.ErrorCode == ResponseCode.FAILURE_PERMISSION_DENY)
                {
                    await Common.AlertAsync(AppResources.rs_facebook_cannot_forgot_password);
                }
                else
                {
                    await Common.AlertAsync(ex.Message);
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        public void ResetData()
        {
            EmailRecoveryPassword = null;
        }

        #endregion
    }
}