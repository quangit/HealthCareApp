using System;
using System.Collections.Generic;
using System.Windows.Input;

using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
#endif


namespace HealthCare.Core.ViewModels
{
    public class LoginViewModel : MyMvxViewModel
    {
        private readonly IMessageService _messageService;
        private readonly IFileService _fileService;
        private bool _loading;
        private ICommand _loginCommand;
        private string _password;
        private bool _remember;
        private ICommand _signupCommand;
        private string _userName;

#if MVVMCROSS
        public LoginViewModel(IFileService fileService, IMessageService messageService)
        {
            _fileService = fileService;
            _messageService = messageService;
            Remember = true;
        }
#else
        public LoginViewModel()
        {
            _fileService = IoC.Resolve<IFileService>();
            _messageService = IoC.Resolve<IMessageService>();

            
        }
#endif

        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
                Data.UserName = _userName;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                Data.Password = _password;
            }
        }

        public bool Remember
        {
            get { return _remember; }
            set { SetProperty(ref _remember, value); }
        }

        public ICommand LoginCommand
        {
            get
            {
                _loginCommand = _loginCommand ?? new MvxCommand(() =>
                {
                    Login(UserName, Password);
                });
                return _loginCommand;
            }
        }

        public async Task Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                await
                    _messageService.ShowMessageAsync(
                        string.IsNullOrEmpty(userName)
                            ? AppResources.FailureUsernameEmpty
                            : AppResources.FailurePasswordEmpty, AppResources.SignIn_Title);

                return;
            }
            Loading = true;
            var result = Data.User;
            if (result == null || result.Expired < DateTime.Now)
                result = await HealthCareService.Current.LoginAsync(userName, password);

            if (Data.User != null)
                Debug.WriteLine("UserID: " + Data.User.UserId + " ; SessionID: " + Data.User.SessionId);

            if (result != null && Data.User != null)
            {
                if (result.UserType == HealthCare.Core.Models.Enums.UserType.TypeHospitalUser)
                {
                    Data.User.Profile = await HealthCareService.Current.GetProfile();
                    await _fileService.SaveLocal(Remember, userName, password);
                    ShowViewModel<HomeViewModel>();
                }
                else
                {
                    await _messageService.ShowMessageAsync(AppResources.SignIn_NoAccess, AppResources.Warning);
                }
            }
            Loading = false;
        }



        public ICommand SignUpCommand
        {
            get
            {
                _signupCommand = _signupCommand ?? new MvxCommand(() =>
                {
                    Loading = true;
                    //var result = await HealthCareService.Current.GetHospitals();
                    Loading = false;
                    //if (result != null)
                    //{
                    ShowViewModel<SignUpViewModel>(new { UserName, Password });
                    //}
                });
                return _signupCommand;
            }
        }



        private MvxCommand _resetCommand;

        public MvxCommand ResetCommand
        {
            get { return _resetCommand ?? (_resetCommand = new MvxCommand(Reset)); }
        }

        private void Reset()
        {
            ShowViewModel<ResetPassViewModel>();
        }

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        public override async void Init()
        {
            //TODO: Testing purpose 
            //UserName = "01228864526";
            //Password = "654321Ab@";	
            var r = await _fileService.LoadLocal();
#if !MVVMCROSS
            try
            {
                Remember = Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("Remember") && (bool)Windows.Storage.ApplicationData.Current.LocalSettings.Values["Remember"];
            }
            catch (Exception)
            {
                Remember = false;
            }
#endif
            if (r != null && r.ContainsKey("username"))
            {
                UserName = r["username"].ToString();
                Password = r["password"].ToString();
                //                if (r.ContainsKey("data") && r["data"] != null)
                //                {
                //                    Data.User = JsonConvert.DeserializeObject<LoginResult>(r["data"].ToString());
                //                }
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                    LoginCommand.Execute(null);
            }
        }


    }
}