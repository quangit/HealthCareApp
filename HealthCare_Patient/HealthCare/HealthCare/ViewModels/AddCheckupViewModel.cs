using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class AddCheckupViewModel : BaseViewModel<AddCheckupViewModel>
    {
        private readonly CheckupValidator _checkupValidator;
        private readonly ICheckupWS _checkupWs;
        private readonly CommonValidator _commandValidator;
        private CheckupTypeModel _checkupTypeFilter;

        public AddCheckupViewModel(INavigationService navigationService, ICheckupWS checkupWs,
            CommonValidator commandValidator, CheckupValidator checkupValidator) : base(navigationService)
        {
            _checkupWs = checkupWs;
            _commandValidator = commandValidator;
            _checkupValidator = checkupValidator;
            CheckupAdding = new ProxyAddCheckupModel();
            ListDoctorFilterByCheckupType = new ObservableCollection<ProxyDoctorModel>();
            ListDoctorFilterByCheckupTypeLoadMore = new ObservableCollection<ProxyDoctorModel>();
        }

        #region Properties

        public ObservableCollection<UserModel> ListUser
        {
            get
            {
                var l =
                    new ObservableCollection<UserModel>(UserViewModel.Instance.RelatedAccounts ??
                                                        new ObservableCollection<UserModel>());
                var currentUserClone = UserViewModel.Instance.CurrentUser.Clone();

                SelectedUser = new UserModel();

                l.Insert(0, currentUserClone);
                l.Insert(l.Count, SelectedUser);
                return l;
            }
        }

        public HospitalModel SelectedHospital
        {
            get { return _selectedHospital; }
            set
            {
                _selectedHospital = value;
                RaisePropertyChanged();
            }
        }

        public ProxyDoctorModel SelectedDoctor { get; set; }

        public CheckupTypeModel CheckupTypeFilter
        {
            get
            {
                return _checkupTypeFilter;// ?? (_checkupTypeFilter = DoctorViewModel.Instance.ListCheckupTypeDisplay[0]);
            }
            set
            {
                //if (value != null && value.Id.Equals(_checkupTypeFilter?.Id))
                //    FilterChanged(value);
                //else if (value != null && _checkupTypeFilter == null)
                //    FilterChanged(value);

                if (value != null && !value.Id.Equals(_checkupTypeFilter?.Id))
                    FilterChanged(value);

                _checkupTypeFilter = value;
                RaisePropertyChanged();

            }
        }


        private ICommand _filterCommand;
        public ICommand FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new RelayCommand(() =>
                {
                    //if (Common.OS != TargetPlatform.Android)
                    //    FilterChanged(CheckupTypeFilter);
                }));
            }
        }

        private void FilterChanged(CheckupTypeModel value)
        {
            ListDoctorFilterByCheckupType = new ObservableCollection<ProxyDoctorModel>();
            var doctorListClone = DoctorViewModel.Instance.ListDoctor.CloneJson();
            if (value == null || value.Id.Equals(AppConstant.IdAllItems))
            {
                foreach (var doctorModel in doctorListClone)
                {
                    foreach (var hospital in doctorModel.Hospitals)
                    {
                        if (hospital.Id.Equals(SelectedHospital.Id))
                            ListDoctorFilterByCheckupType.Add(doctorModel);
                    }
                }
            }
            else
            {
                foreach (var doctorModel in doctorListClone)
                {
                    foreach (var hospitalModel in doctorModel.Hospitals)
                    {
                        if (hospitalModel.Id.Equals(SelectedHospital.Id) &&
                            hospitalModel.CheckupType != null && //doctor must have checkuptype
                            hospitalModel.CheckupType.Id.Equals(value.Id))
                            ListDoctorFilterByCheckupType.Add(doctorModel);
                    }
                }
            }

            ListDoctorFilterByCheckupType =
                new ObservableCollection<ProxyDoctorModel>(
                    ListDoctorFilterByCheckupType.GroupBy(x => x.Id).Select(y => y.First()));

            DoctorCount = ListDoctorFilterByCheckupType?.Count ?? -1;

            ResetLoadMoreDoctorList();
            LoadMoreDoctorList(true);
        }

        public ObservableCollection<ProxyDoctorModel> ListDoctorFilterByCheckupType
        {
            get { return _listDoctorFilterByCheckupType; }
            set
            {
                _listDoctorFilterByCheckupType = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProxyDoctorModel> ListDoctorFilterByCheckupTypeLoadMore
        {
            get { return _listDoctorFilterByCheckupTypeLoadMore; }
            set
            {
                _listDoctorFilterByCheckupTypeLoadMore = value;
                RaisePropertyChanged();
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                RaisePropertyChanged();
                IsAddRelatedAccount = string.IsNullOrWhiteSpace(value?.UserCode);
            }
        }

        public CityModel SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                RaisePropertyChanged();
            }
        }

        public DistrictModel SelectedDistrict
        {
            get { return _selectedDistrict; }
            set
            {
                _selectedDistrict = value;
                RaisePropertyChanged();
            }
        }

        public ScheduleModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                _selectedSchedule = value;
                RaisePropertyChanged();
            }
        }

        public ProxyAddCheckupModel CheckupAdding
        {
            get { return _checkupAdding; }
            set
            {
                _checkupAdding = value;
                RaisePropertyChanged();
            }
        }

        public bool IsAddRelatedAccount
        {
            get { return _isAddRelatedAccount; }
            set
            {
                _isAddRelatedAccount = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        private ICommand _goToPickDoctorPageCommand;

        public ICommand GoToPickDoctorPageCommand
        {
            get
            {
                return _goToPickDoctorPageCommand ??
                       (_goToPickDoctorPageCommand = new RelayCommand<string>(async hospitaId =>
                       {
                           if (SelectedHospital != null)
                           {
                               if (await DoctorViewModel.Instance.LoadData(hospitaId, true))
                               {
                                   ListDoctorFilterByCheckupType = DoctorViewModel.Instance.ListDoctor;
                                   CheckupTypeFilter = null;
                                   //await LoadMoreDoctorList(false);
                                   NavigationService.NavigateTo(typeof(PickDoctorPage));
                                   CheckupTypeFilter = DoctorViewModel.Instance.ListCheckupTypeDisplay[0];
                               }
                           }
                           else
                           {
                               await Common.AlertAsync(AppResources.empty_hospital_selected);
                           }
                       }));
            }
        }


        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand(async () =>
                {
                    if (await Common.ConfirmAsync(AppResources.confirm_cancel_checkup))
                    {
                        ResetSelecteds();
                        //ToolbarViewModel.Instance.SetSelectedPage(typeof(CheckupPage));
                        NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                    }
                }));
            }
        }


        private ICommand _goBackCommand;

        public ICommand GoBackCommand
        {
            get { return _goBackCommand ?? (_goBackCommand = new RelayCommand(() => { NavigationService.GoBack(); })); }
        }


        private ICommand _listViewHospitalClickCommand;

        public ICommand ListViewHospitalClickCommand
        {
            get
            {
                return _listViewHospitalClickCommand ??
                       (_listViewHospitalClickCommand =
                           new RelayCommand<HospitalModel>(hospital => { SelectedHospital = hospital; }));
            }
        }


        private ICommand _goToSchedulePageCommand;

        public ICommand GoToSchedulePageCommand =>
            _goToSchedulePageCommand ?? (_goToSchedulePageCommand = new RelayCommand(async () =>
            {
                if (SelectedDoctor != null)
                {
                    Common.ShowLoading();
                    DoctorScheduleViewModel.Instance.ResetDateTimeParams();

                    //Bug: Must load DateTime data before set item source for RadCalander
                    if (await DoctorScheduleViewModel.Instance.LoadData(SelectedDoctor?.Id, SelectedHospital?.Id))
                    {
                        UserDialogs.Instance.HideLoading();
                        await CommonViewModel.Instance.GetListCityAsync(false, false);
                        DoctorScheduleViewModel.Instance.CheckupFlowGoNextCommand = GoToAddCheckupPageCommand;
                        NavigationService.NavigateTo(typeof(DoctorSchedulePage));
                    }
                    else
                        UserDialogs.Instance.HideLoading();
                }
                else
                {
                    await Common.AlertAsync(AppResources.empty_doctor_selected);
                }
            }));


        private ICommand _getScheduleOfDoctorCommand;

        public ICommand GetScheduleListOfDoctorCommand
        {
            get
            {
                return _getScheduleOfDoctorCommand ?? (_getScheduleOfDoctorCommand = new RelayCommand(async () =>
                {
                    Common.ShowLoading();
                    try
                    {
                        await
                            DoctorScheduleViewModel.Instance.GetScheduleOfDoctor(SelectedDoctor.Id, SelectedHospital.Id);
                    }
                    catch (Exception e)
                    {
                        await Common.AlertAsync(e.Message);
                    }
                    UserDialogs.Instance.HideLoading();
                }));
            }
        }


        private ICommand _resetSelectedDoctorCommand;

        public ICommand ResetSelectedDoctorCommand
            =>
                _resetSelectedDoctorCommand ??
                (_resetSelectedDoctorCommand = new RelayCommand(() => { SelectedDoctor = null; }));


        private ICommand _listViewDoctorClickedCommand;

        public ICommand ListViewDoctorClickedCommand
        {
            get
            {
                return _listViewDoctorClickedCommand ??
                       (_listViewDoctorClickedCommand = new RelayCommand<ProxyDoctorModel>(
                           doctor => { SelectedDoctor = doctor; }));
            }
        }


        private ICommand _goToAddCheckupPageCommand;
        private ScheduleModel _selectedSchedule;

        public ICommand GoToAddCheckupPageCommand
        {
            get
            {
                return _goToAddCheckupPageCommand ?? (_goToAddCheckupPageCommand = new RelayCommand(async () =>
                {
                    if (SelectedSchedule != null)
                    {
                        IsAddRelatedAccount = false;
                        CheckupAdding = new ProxyAddCheckupModel();

                        NavigationService.NavigateTo(typeof(AddCheckupPage));
                        //CheckRegistrationWithHospital();
                    }
                    else
                    {
                        await Common.AlertAsync(AppResources.empty_schedule_selected);
                    }
                }));
            }
        }

        private async void CheckRegistrationWithHospital()
        {
            bool isRegisterWithHospital = true;//call API

            if (isRegisterWithHospital)
            {
                //check DOB, email, idNo
                var u = UserViewModel.Instance.CurrentUser;
                if (u.BirthDay == DateTime.MinValue || string.IsNullOrWhiteSpace(u.Email) ||
                    string.IsNullOrWhiteSpace(u.IdNo))
                {
                    if (await Common.ConfirmAsync(AppResources.update_profile))
                        SettingViewModel.Instance.GoProfileCommand.Execute(null);
                }
                else
                {
                    ShowDialogInputSecurityCode();
                }
            }
            else
            {
                NavigationService.NavigateTo(typeof(AddCheckupPage));
            }

            //NavigationService.NavigateTo(typeof(AddCheckupPage));

        }

        private async void ShowDialogInputSecurityCode()
        {
            try
            {
                var result = await UserDialogs.Instance.PromptAsync(
                    string.Format(AppResources.set_pin_hospital_title, UserViewModel.Instance.CurrentUser.Email)
                    , null,
                    AppResources.confirm, AppResources.cancel,
                    AppResources.pin_code, InputType.NumericPassword);

                if (result.Ok)
                {
                    var resultVal = _commandValidator.ValidateSendHospitalReigtrationSecurityCode(result.Text);
                    if (!resultVal.IsValid)
                    {
                        await Common.AlertAsync(resultVal.Errors[0]);
                        ShowDialogInputSecurityCode();
                        return;
                    }

                    //call API

                    //success
                    NavigationService.NavigateTo(typeof(AddCheckupPage));
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
                ShowDialogInputSecurityCode();
            }
        }


        private ICommand _goToAddCheckupConfirmPageCommand;
        private bool _isAddRelatedAccount;
        private UserModel _selectedUser;

        public ICommand GoToAddCheckupConfirmPageCommand
        {
            get
            {
                return _goToAddCheckupConfirmPageCommand ??
                       (_goToAddCheckupConfirmPageCommand = new RelayCommand(async () =>
                       {
                           if (Common.OS == TargetPlatform.iOS)
                               CheckupAdding.Symptom = AppConstant.SymptomIOS;

                           var valResult = _checkupValidator.ValidateAddCheckup(CheckupAdding);
                           if (!valResult.IsValid)
                           {
                               await Common.AlertAsync(valResult.Errors[0]);
                               return;
                           }

                           //validate before go next
                           if (string.IsNullOrWhiteSpace(SelectedUser.UserCode)) //UserCode null is the Other
                           {
                               var validateResult = _commandValidator.ValidateRelatedUser(SelectedUser.FirstName,
                                   SelectedUser.Address, SelectedUser.Email);

                               if (!validateResult.IsValid)
                               {
                                   await Common.AlertAsync(validateResult.Errors[0]);
                                   return;
                               }

                               var user = SelectedUser.CloneJson();
                               user.LastName = "";

                               CheckupAdding.UserId = null;

                               user.ParentId = UserViewModel.Instance.CurrentUser.Id;
                               CheckupAdding.DataUser = user;

                               CheckupAdding.DataUser.City = SelectedCity;
                               CheckupAdding.DataUser.District = SelectedDistrict;
                           }
                           else
                           {
                               var user = SelectedUser.CloneJson();
                               user.LastName = "";

                               if (SelectedUser.UserCode.Equals(UserViewModel.Instance.CurrentUser.UserCode))
                               {
                                   //add for myseft
                                   CheckupAdding.UserId = SelectedUser.Id;
                               }
                               else
                               {
                                   //add for related account
                                   user.ParentId = UserViewModel.Instance.CurrentUser.Id;
                                   CheckupAdding.UserId = user.Id;
                               }

                               CheckupAdding.DataUser = user;
                               SelectedCity = new CityModel();
                               SelectedDistrict = new DistrictModel();
                           }

                           CheckupAdding.Schedule = SelectedSchedule;

                           NavigationService.NavigateTo(typeof(AddCheckupConfirmPage));
                       }));
            }
        }


        private ICommand _addCheckupCommand;

        public ICommand AddCheckupCommand => _addCheckupCommand ?? (_addCheckupCommand = new RelayCommand((async () =>
        {
            if (await AddCheckup())
            {
                //Settings.AddCheckupLastTime = Common.ConvertToTimestamp(DateTime.Now);

                if (SelectedHospital.CheckupFee.Equals(0) && SelectedHospital.SchedulingFee.Equals(0))
                {
                    await Common.AlertAsync(AppResources.add_checkup_success_free);
                }
                else
                {
                   await Common.AlertAsync(string.Format(AppResources.add_checkup_success, _expriedTimeString));
                }
                //ToolbarViewModel.Instance.SetSelectedPage(typeof(CheckupPage));
                //GC.Collect();
                await UserViewModel.Instance.RefreshData(true);
                ResetSelecteds();
                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                UserDialogs.Instance.HideLoading();
            }
        })));


        private ICommand _resetSelectedsCommand;
        private ProxyAddCheckupModel _checkupAdding;
        private ObservableCollection<ProxyDoctorModel> _listDoctorFilterByCheckupType;
        private HospitalModel _selectedHospital;
        private CityModel _selectedCity;
        private DistrictModel _selectedDistrict;
        private ICommand _loadMoreDoctorListCommand;
        private bool _isCanLoadMore;
        private int _startIndex;
        private int _page;
        private ObservableCollection<ProxyDoctorModel> _listDoctorFilterByCheckupTypeLoadMore;
        private ICommand _resetLoadMoreDoctorListCommand;
        private int _doctorCount;

        public ICommand ResetSelectedsCommand
            => _resetSelectedsCommand ?? (_resetSelectedsCommand = new RelayCommand(ResetSelecteds));

        public ICommand LoadMoreDoctorListCommand
        {
            get
            {
                return _loadMoreDoctorListCommand ??
                       (_loadMoreDoctorListCommand = new RelayCommand(async () => { await LoadMoreDoctorList(true); }));
            }
        }

        public ICommand ResetLoadMoreDoctorListCommand => _resetLoadMoreDoctorListCommand ??
                                                          (_resetLoadMoreDoctorListCommand = new RelayCommand(ResetLoadMoreDoctorList));

        #endregion

        #region Methods

        private void ResetLoadMoreDoctorList()
        {
            ListDoctorFilterByCheckupTypeLoadMore = new ObservableCollection<ProxyDoctorModel>();
            _page = 0;
            _startIndex = 0;
            IsCanLoadMore = true;
            DoctorCount = -1;
        }

        public int DoctorCount
        {
            get { return _doctorCount; }
            set { _doctorCount = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ProxyDoctorModel> GetDoctorsLocalyForLoadMore(int start, int length)
        {
            var l = ListDoctorFilterByCheckupType.CloneJson();
            int count = l.Count;
            int endIndex = 0;
            endIndex = length + start - 1;
            if (start >= count) return null;
            if (endIndex >= count) endIndex = count - 1;

            var arr = new ObservableCollection<ProxyDoctorModel>();
            for (int i = start; i <= endIndex; i++)
            {
                arr.Add(l[i]);
            }

            return arr;
        }

        private async Task LoadMoreDoctorList(bool isShowLoading)
        {
            try
            {
                if (!IsCanLoadMore) return;

                if (isShowLoading && Common.OS != TargetPlatform.WinPhone)
                    Common.ShowLoading();

                await Task.Delay(300);

                _startIndex += _page > 0 ? AppConstant.ITEMS_PER_PAGE : 0;
                IsCanLoadMore = false;

                //TODO: Deactive
                var l = GetDoctorsLocalyForLoadMore(_startIndex, AppConstant.ITEMS_PER_PAGE);
                IsCanLoadMore = true;

                await Task.Delay(100);//make sure the IsCanLoadMore = true is notify to the HcListView CanLoadMoreProperty

                if (l?.Count > 0)
                {
                    foreach (var model in l)
                    {
                        ListDoctorFilterByCheckupTypeLoadMore.Add(model);
                    }
                    RaisePropertyChanged("ListDoctorFilterByCheckupTypeLoadMore");
                    _page++;
                    IsCanLoadMore = true;
                }

                if (l == null || l.Count == 0 || l.Count < AppConstant.ITEMS_PER_PAGE)
                {
                    IsCanLoadMore = false;
                }
            }
            catch (Exception e)
            {
                Common.AlertAsync(e.Message);
            }
            finally
            {
                DoctorCount = ListDoctorFilterByCheckupTypeLoadMore.Count;
                UserDialogs.Instance.HideLoading();
            }
        }

        public bool IsCanLoadMore
        {
            get { return _isCanLoadMore; }
            set { _isCanLoadMore = value; RaisePropertyChanged(); }
        }

        private void ResetSelecteds()
        {
            CheckupAdding = new ProxyAddCheckupModel();
            SelectedHospital = null;
            SelectedUser = null;
            SelectedDoctor = null;
            SelectedCity = null;
            SelectedDistrict = null;
            SelectedSchedule = null;
            DoctorScheduleViewModel.Instance.ResetDateTimeParams();
        }

        private string _expriedTimeString;
        private async Task<bool> AddCheckup()
        {
            try
            {
                Common.ShowLoading();
                var checkupAdded = await _checkupWs.AddCheckup(CheckupAdding);
                _expriedTimeString = checkupAdded.PaymentExpiredDate.ToLocalTime().ToString("HH:mm") + " "
                    + checkupAdded.PaymentExpiredDate.ToLocalTime().ToString("d");
                return true;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            return false;
        }

        #endregion
    }
}