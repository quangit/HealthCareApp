using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using Newtonsoft.Json;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Helpers;
using HealthCare.Models.ChBaseModel;
using HealthCare.Pages.CHBases;
using HealthCare.Validators.CHBasesValidator;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels.CHBases
{
    public class MedicationViewModel : BaseViewModel<MedicationViewModel>
    {
        #region Variables
        private readonly IChBaseWS _chBaseWs;
        private ObservableCollection<MedicationModel> _listMedications;
        private RelayCommand _submitAddCommand;
        private string _symptom;
        private readonly MedicationValidator _medicationValidator;
        private MedicationModel _medicationAdding;
        public string Symptom
        {
            get { return _symptom; }
            set
            {
                _symptom = value;
                RaisePropertyChanged();
            }
        }
        public MedicationModel MedicationAdding
        {
            get { return _medicationAdding; }
            set
            {
                _medicationAdding = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<MedicationModel> ListMedications
        {
            get { return _listMedications; }
            set
            {
                _listMedications = value;
                RaisePropertyChanged("ListMedications");
            }
        }
        #endregion

        #region Commands
        public RelayCommand GoToAddMedicatioPageCommand => new RelayCommand(() =>
        {
            MedicationAdding = new MedicationModel();
            MedicationAdding.DateStarted = DateTime.Now;
            MedicationAdding.DateDiscontinued = DateTime.Now;
            NavigationService.NavigateTo(typeof(AddMedicationPage));
        });

        public RelayCommand GetListMedicalHistoryCommand => new RelayCommand(GetMedicationList);

        public RelayCommand<MedicationModel> GoToDetailPageCommand => new RelayCommand<MedicationModel>(i =>
        {
            if (i != null)
            {
                var detailList = new ObservableCollection<CHBaseDetailUIModel>
                {
                    new CHBaseDetailUIModel
                    {
                        Title = "",
                        Value = i.Name,
                        Type = CHBaseDetailTypeEnum.OneLineOneText
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = "",
                        Value = i.DateStarted.ToString("d") +" - " +i.DateDiscontinued.ToString("d"),
                        Type = CHBaseDetailTypeEnum.OneLineOneText
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_name,
                        Value = i.Name,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_generic_name,
                        Value = i.GenericName,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_strength,
                        Value = i.Strength,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_dose,
                        Value = i.Dose,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_often_taken,
                        Value = i.HowOftenTaken,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_taken,
                        Value = i.HowTaken,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_indication,
                        Value = i.Indication,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_prescribed,
                        Value = i.Prescribed,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.medication_prescribed_by,
                        Value = i.PrescribedBy,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    }
                };
                var deleteAction = new Action(async () =>
                {
                    if (await Common.ConfirmAsync(Resx.AppResources.confirm_del_medication))
                    {
                        Common.ShowLoading();
                        if (await _chBaseWs.RemoveData(i.Id))
                        {
                            // delete success 
                            ListMedications = await _chBaseWs.GetMedication();
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
                CHBaseDetailViewModel.Instance.GoToDetailPage(AppResources.medication, detailList, deleteAction);
            }
        });

        public RelayCommand SubmitAddCommand => _submitAddCommand ??
                                             (_submitAddCommand = new RelayCommand(Submit));
        #endregion

        #region Methods
        public MedicationViewModel(INavigationService navigationService, MedicationValidator medicationValidator, IChBaseWS chBaseWs) : base(navigationService)
        {
            _chBaseWs = chBaseWs;
            _medicationValidator = medicationValidator;

            ClearViewModel();
        }

        private void ClearViewModel()
        {
            ListMedications = new ObservableCollection<MedicationModel>();
        }

        public async void GetMedicationList()
        {
            Common.ShowLoading();

            ListMedications = await _chBaseWs.GetMedication();

            Common.HideLoading();
        }

        public async void Submit()
        {
            try
            {
                if (Common.OS == TargetPlatform.iOS)
                    Symptom = AppConstant.SymptomIOS;

                var result = _medicationValidator.ValidateAddMedication(MedicationAdding);
                if (!result.IsValid)
                {
                    UserDialogs.Instance.Alert(result.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                var isSuccess = await _chBaseWs.AddMedication(MedicationAdding);
                if (isSuccess)
                {
                    ListMedications = await _chBaseWs.GetMedication();
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