using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;

#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
#endif

namespace HealthCare.Core.ViewModels
{
    public class SignUpViewModel : MyMvxViewModel
    {
        private SignUpInfo _account;
        private List<CheckupType> _checkupTypes;
        private List<City> _cities;
        private List<District> _districts;
        private bool _loading;

        private readonly IMessageService _messageService;
        private IReporterService _reporterService;
        public SignUpViewModel(IMessageService messageService, IReporterService reporterService)
        {
            _messageService = messageService;
            _reporterService = reporterService;
            SignUpCommand = new MvxCommand(SignUp);
            Account = new SignUpInfo();
            BackCommand = new MvxCommand(() => { Close(this); });
        }
#if !MVVMCROSS
        public SignUpViewModel() : this(IoC.Resolve<IMessageService>(), IoC.Resolve<IReporterService>())
        {

        }
#endif


        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        public SignUpInfo Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }

        public List<City> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        public List<CheckupType> CheckupTypes
        {
            get { return _checkupTypes; }
            set { SetProperty(ref _checkupTypes, value); }
        }

        public MvxCommand SignUpCommand { get; set; }
        public MvxCommand BackCommand { get; private set; }
        public List<District> Districts
        {
            get { return _districts; }
            set { SetProperty(ref _districts, value); }
        }

        public List<Gender> Genders
        {
            get { return new List<Gender> { new Gender { IsFemale = false }, new Gender { IsFemale = true } }; }
        }

        private async void SignUp()
        {
            if (Account.Password != Account.RePass)
            {
                await _messageService.ShowMessageAsync(AppResources.SignUp_PassMatch, AppResources.SignUp_Title);
            }
            else if (Account.IsValid())
            {
                var result = await HealthCareService.Current.SignUpAsync(Account);
                if (result != null)
                {
                    await _messageService.ShowMessageAsync(AppResources.Sign_UpSuccess, AppResources.SignUp_Title);
                    Close(this);
                }
            }
            else
            {
                await _messageService.ShowMessageAsync(AppResources.SignUp_Invalid, AppResources.SignUp_Title);
            }
        }

        public void ChangeDistrict(string cityName)
        {
            Districts = Cities.FirstOrDefault(x => x.Name == cityName).Districts;
        }

        public override void Init()
        {
            base.Init();

            dynamic user = GetParam<object>();
            if (user != null)
            {
                Account.Phone = user.UserName;
                Account.Password = user.Password;
            }
            //LoadData();
            //var hospitals = GetParam<Hospitals>();
            //Account.Hospitals = hospitals.keyValue;
        }



        private async void LoadData()
        {
            Loading = true;
            _reporterService.ShowProgress();
            Cities = await HealthCareService.Current.GetCities();
            CheckupTypes = await HealthCareService.Current.GetCheckuptypes();
            Loading = false;
            _reporterService.StopProgress();
        }
    }
}