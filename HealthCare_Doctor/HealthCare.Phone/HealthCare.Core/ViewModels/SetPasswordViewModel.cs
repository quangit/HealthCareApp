using System;
using HealthCare.Core.Services.Interfaces;

#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using Template10.Mvvm;
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
#endif
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;


namespace HealthCare.Core.ViewModels
{
    public class SetPasswordViewModel : MyMvxViewModel
    {
        private readonly IMessageService _messageService;
        private IFileService _fileService;
        private IReporterService _reporterService;
        public SetPasswordViewModel(IMessageService messageService, IReporterService reporterService, IFileService fileService)
        {
            _messageService = messageService;
            _reporterService = reporterService;
            _fileService = fileService;
        }
#if !MVVMCROSS
        public SetPasswordViewModel() : this(IoC.Resolve<IMessageService>(), IoC.Resolve<IReporterService>(), IoC.Resolve<IFileService>())
        {

        }
#endif
        private string _oldPassword;
        public string OldPassword
        {
            get { return _oldPassword; }
            set { SetProperty(ref _oldPassword, value); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { SetProperty(ref _newPassword, value); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        private MvxCommand _setPasswordCommand;

        public MvxCommand SetPasswordCommand
        {
            get { return _setPasswordCommand ?? (_setPasswordCommand = new MvxCommand(DoSetPassword)); }
        }

        private async void DoSetPassword()
        {
            if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                await _messageService.ShowMessageAsync(AppResources.SignUp_Invalid, AppResources.SetPassword_Title);
            }
            else
             if (!NewPassword.Equals(ConfirmPassword))
                await _messageService.ShowMessageAsync(AppResources.SetPassword_NotMatch, AppResources.SetPassword_Title);
            else
            {
                if (await HealthCareService.Current.ChangePassword(OldPassword, NewPassword))
                {
                    await _messageService.ShowMessageAsync(AppResources.SetPasswrod_Success, AppResources.SetPassword_Title);
                    if (Data.Remember)
                        await _fileService.SaveLocal(Data.Remember, Data.UserName, Data.Password = NewPassword);
                    this.Close(this);
                }
            }
        }

    }
}

