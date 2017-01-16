using System.Collections.Generic;
#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
#endif
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using System;

namespace HealthCare.Core.ViewModels
{
    public class UpdateProfileViewModel : MyMvxViewModel
    {
        private List<CheckupType> _checkupTypes;
        private List<City> _cities;

        private MvxCommand _genderCommand;

        public MvxCommand GenderCommand
        {
            get { return _genderCommand ?? (_genderCommand = new MvxCommand(Gender)); }
        }

        private void Gender()
        {
            Account.Gender = new Gender() {IsFemale = Account.Gender?.IsFemale != true};
        }
        private MvxCommand _saveCommand;
        public MvxCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new MvxCommand(Save)); }
        }

        private bool _loading;
        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        private Doctor _account;
        private readonly IReporterService _reporterService;
        private IFileService _fileService;
        public UpdateProfileViewModel(IReporterService reporterService, IFileService fileService)
        {
            _reporterService = reporterService;
            _fileService = fileService;
        }
#if !MVVMCROSS
        public UpdateProfileViewModel() : this(IoC.Resolve<IReporterService>(), IoC.Resolve<IFileService>())
        {

        }
#endif
        public List<Gender> Genders
        {
            get { return new List<Gender> { new Gender { IsFemale = false }, new Gender { IsFemale = true } }; }
        }

        public Doctor Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }

        public List<City> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        private List<District> _districts;
        public List<District> Districts
        {
            get { return _districts; }
            set { SetProperty(ref _districts, value); }
        }

        public List<CheckupType> CheckupTypes
        {
            get { return _checkupTypes; }
            set { SetProperty(ref _checkupTypes, value); }
        }
        public override void Init()
        {
            base.Init();
            LoadData();
            Account = (Data.User.Profile).Clone();

        }




        private void LoadData()
        {
            Loading = true;
            _reporterService.ShowProgress();

            LoadBackground(async () =>
            {
                Cities = await HealthCareService.Current.GetCities();
                //Account.City
                if (Account.City != null)
                {
                    Account.City = Cities.Find(x => Account.City.Name == x.Name);
#if !MVVMCROSS
                    Account.District = Account.City?.Districts?.Find(x => x.Id.EndsWith(Account.DistrictId));
#endif
                }

            });

            LoadBackground(async () =>
            {
                CheckupTypes = await HealthCareService.Current.GetCheckuptypes();
#if !MVVMCROSS
                if (Account.CheckupType != null || !string.IsNullOrEmpty(Account.CheckupTypeId?.ToString()))
                {
                    Account.CheckupType =
                        CheckupTypes.Find(x => x.Id.Equals(Account.CheckupType?.Id ?? Account.CheckupTypeId?.ToString()));
                }
#endif
            });
            Loading = false;


            _reporterService.StopProgress();
        }
        private async void Save()
        {
            if (await HealthCareService.Current.UpdateProfile(Account))
            {
                _reporterService.ReportMessage(AppResources.UpdateProfile_Title, AppResources.UpdateProfile_Success);
                var r = await HealthCareService.Current.GetProfile();
                if (r != null)
                    Data.User.Profile = r;
                //var r = _fileService.LoadLocal();
                //_fileService.SaveLocal(true, (string) r["username"], (string) r["password"]);
            }
        }

        private void LoadBackground(Action a)
        {
            a();
        }
    }
}