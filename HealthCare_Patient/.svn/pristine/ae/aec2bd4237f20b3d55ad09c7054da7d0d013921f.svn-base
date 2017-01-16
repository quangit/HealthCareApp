using System;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class MoreSupportViewModel : BaseViewModel<MoreSupportViewModel>
    {
        private readonly ICommonWS _commonWS;
        private readonly CommonValidator _validator;
        private string _firstName, _lastName, _age, _email, _doctorName, _hospitalName, _symptom;
        private Gender _gender;
        private RelayCommand _submitCommand;
        private PersonModel _personModel;
        private RelayCommand _goBackCommand;

        public MoreSupportViewModel(INavigationService ns, ICommonWS commonWS, CommonValidator validator) : base(ns)
        {
            _commonWS = commonWS;
            _validator = validator;
        }

        #region Property

        public PersonModel PersonModel
        {
            get { return _personModel; }
            set { _personModel = value; RaisePropertyChanged(); }
        }

        //public string FirstName
        //{
        //    get { return _firstName; }
        //    set
        //    {
        //        _firstName = value;
        //        RaisePropertyChanged("FirstName");
        //    }
        //}

        //public string LastName
        //{
        //    get { return _lastName; }
        //    set
        //    {
        //        _lastName = value;
        //        RaisePropertyChanged("LastName");
        //    }
        //}

        //public string Email
        //{
        //    get { return _email; }
        //    set
        //    {
        //        _email = value;
        //        RaisePropertyChanged("Email");
        //    }
        //}

        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }

        public Gender Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                RaisePropertyChanged("Gender");
            }
        }


        public string DoctorName
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
                RaisePropertyChanged("DoctorName");
            }
        }

        public string HospitalName
        {
            get { return _hospitalName; }
            set
            {
                _hospitalName = value;
                RaisePropertyChanged("HospitalName");
            }
        }

        public string Symptom
        {
            get { return _symptom; }
            set
            {
                _symptom = value;
                RaisePropertyChanged("Symptom");
            }
        }

        public ObservableCollection<Gender> ListGender =>
            new ObservableCollection<Gender> { Gender.Female, Gender.Male };

        public RelayCommand SubmitCommand => _submitCommand ??
                                             (_submitCommand = new RelayCommand(Submit));

        public RelayCommand GoBackCommand => _goBackCommand ??
                                             (_goBackCommand = new RelayCommand(GoBack));

        #endregion

        #region Method

        public void GoBack()
        {
            ClearViewModel();
            NavigationService.GoBack();
        }
        public void FillUserData() => PersonModel = UserViewModel.Instance.CurrentUser.Clone();

        public async void Submit()
        {
            try
            {
                if (Common.OS == TargetPlatform.iOS)
                    Symptom = AppConstant.SymptomIOS;
                var result = _validator.ValidateMoreSupport(PersonModel.FirstName, PersonModel.LastName, Age, Gender,
                    PersonModel.Email, DoctorName, HospitalName, !string.IsNullOrWhiteSpace(Symptom) && Symptom.Equals(AppResources.if_more_support_vi) ? "" : Symptom);
                if (!result.IsValid)
                {
                    UserDialogs.Instance.Alert(result.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                await _commonWS.SendAdditionalSupport(PersonModel.FirstName, PersonModel.LastName, Age, Gender,
                    PersonModel.Email, DoctorName, HospitalName, Symptom);

                await Common.AlertAsync(AppResources.more_support_sent);
                NavigationService.GoBack();

                ClearViewModel();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
        }

        private void ClearViewModel()
        {
            PersonModel = new PersonModel();
            Age = default(string);
            DoctorName = default(string);
            HospitalName = default(string);
            Symptom = default(string);
        }

        #endregion
    }
}