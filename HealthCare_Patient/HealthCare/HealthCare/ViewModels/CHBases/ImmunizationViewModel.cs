using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;
using Newtonsoft.Json;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Helpers;
using HealthCare.Pages.CHBases;
using HealthCare.Models.ChBaseModel;
using HealthCare.Resx;
using HealthCare.Validators.CHBasesValidator;

namespace HealthCare.ViewModels.CHBases
{
    public class ImmunizationViewModel : BaseViewModel<ImmunizationViewModel>
    {
        #region Variables
        private readonly IChBaseWS _chBaseWs;
        private RelayCommand _submitAddCommand;
        private string _symptom;
        private readonly ImmunizationValidator _immunizationValidator;
        private ImmunizationModel _immunizationAdding;
        private ObservableCollection<ImmunizationModel> _listImmunizations;

        public ObservableCollection<ImmunizationModel> ListImmunizations
        {
            get { return _listImmunizations; }
            set
            {
                _listImmunizations = value;
                RaisePropertyChanged("ListImmunizations");
            }
        }

        public string Symptom
        {
            get { return _symptom; }
            set
            {
                _symptom = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region Commands
        public RelayCommand GoToAddImmunizationPageCommand
                   => new RelayCommand(() =>
                   {
                       ImmunizationAdding = new ImmunizationModel();
                       ImmunizationAdding.DateAdministrated=DateTime.Now;
                       ImmunizationAdding.ExpirationDate=DateTime.Now;
                       NavigationService.NavigateTo(typeof(AddImmunizationPage));
                   });
        public ImmunizationModel ImmunizationAdding
        {
            get { return _immunizationAdding; }
            set
            {
                _immunizationAdding = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SubmitAddCommand => _submitAddCommand ??
                                             (_submitAddCommand = new RelayCommand(Submit));

        public RelayCommand GetListImmunizationCommand => new RelayCommand(GetImmunizationList);


        public RelayCommand<ImmunizationModel> GoToDetailPageCommand => new RelayCommand<ImmunizationModel>(i =>
        {
            if (i != null)
            {
                var detailInfo = new ObservableCollection<CHBaseDetailUIModel>()
                {
                    new CHBaseDetailUIModel()
                    {
                        Value = i.Name,
                        Type = CHBaseDetailTypeEnum.OneLineOneText
                    },
                    new CHBaseDetailUIModel()
                    {
                        Value = i.DateAdministrated.ToString("d"),
                        Type = CHBaseDetailTypeEnum.OneLineOneText
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.who_gave_it_title,
                        Value = i.Administrator,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.manufacturer_title,
                        Value = i.Manufacturer,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.lot_title,
                        Value = i.Lot,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.where_administrered_title,
                        Value = i.Route,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.how_administered_title,
                        Value = i.AnatomicSurface,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.expiration_date_title,
                        Value = i.ExpirationDate.ToString("d"),
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.sequence_title,
                        Value = i.Sequence,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.adverse_event_afterwards_title,
                        Value = i.AdverseEvent,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel()
                    {
                        Title = AppResources.consent_description_title,
                        Value = i.Consent,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                };
                var deleteAction = new Action(async () =>
                {
                    if (await Common.ConfirmAsync(Resx.AppResources.confirm_del_Immunization))
                    {
                        Common.ShowLoading();
                        if (await _chBaseWs.RemoveData(i.Id))
                        {
                            // delete success 
                            ListImmunizations = await _chBaseWs.GetImmunization();
                            NavigationService.GoBack();
                            Common.HideLoading();
                        }
                        else
                        {
                            Common.HideLoading();
                            //Error
                        }
                    }
                });
                CHBaseDetailViewModel.Instance.GoToDetailPage(AppResources.immunization, detailInfo, deleteAction);
            }
        });
        #endregion

        #region Methods
        public ImmunizationViewModel(INavigationService navigationService, ImmunizationValidator immunizationValidator, IChBaseWS chBaseWs) : base(navigationService)
        {
            _chBaseWs = chBaseWs;
            _immunizationValidator = immunizationValidator;
            ClearViewModel();
        }




        private void ClearViewModel()
        {
            ListImmunizations = new ObservableCollection<ImmunizationModel>();

        }

        public async void GetImmunizationList()
        {
            Common.ShowLoading();

            ListImmunizations = await _chBaseWs.GetImmunization();

            Common.HideLoading();
        }

        public async void Submit()
        {
            try
            {
                if (Common.OS == TargetPlatform.iOS)
                    Symptom = AppConstant.SymptomIOS;

                var result = _immunizationValidator.ValidateAddImmunization(ImmunizationAdding);
                if (!result.IsValid)
                {
                    UserDialogs.Instance.Alert(result.Errors[0]);
                    return;
                }

                var isSuccess = await _chBaseWs.AddImmunization(ImmunizationAdding);
                if (isSuccess)
                {
                    ListImmunizations = await _chBaseWs.GetImmunization();
                    NavigationService.GoBack();
                    Common.HideLoading();
                }
                else
                {
                    Common.HideLoading();
                    // Something wrong;
                }
                //Common.ShowLoading();
                //await _commonWS.SendAdditionalSupport(PersonModel.FirstName, PersonModel.LastName, Age, Gender,
                //    PersonModel.Email, DoctorName, HospitalName, Symptom);
                //await _commonWS.SendAdditionalSupport(Symptom, AttachByteArray, Shared);
                //await Common.AlertAsync(AppResources.question_sent);

                //ResetAvatarResource();

                //ClearViewModel();

                //NavigationService.GoBack();

                // await MoreSupportViewModel2.Instance.GetQuestionList(true, true);
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
        }
        #endregion
    }
}