using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class CheckupViewModel : BaseViewModel<CheckupViewModel>
    {
        private readonly ICheckupWS _checkupWS;
        private RelayCommand _addCheckupCommand, _goPaymentCommand;
        private ICommand _goToMedicalRecordPageCommand, _goToHospitalDetailPageCommand;
        private bool _isRefreshing, _isExistPendingCard, _isTapCard;
        private int _itemCount;
        private int _itemHistoryCount;
        private ObservableCollection<ProxyCheckupModel> _listCheckupHistorical;
        private ObservableCollection<ProxyCheckupModel> _listCheckups;
        private ObservableCollection<ProxyCheckupModel> _listFilteredCheckups;
        private RelayCommand _redFilter, _yellowFilter, _greenFilter;
        private ICommand _refreshCommand, _itemTappedCommand;

        public CheckupViewModel(INavigationService navigationService, ICheckupWS checkupWS) : base(navigationService)
        {
            _checkupWS = checkupWS;
        }

        #region Properties
        public bool IsRatedSuccess { get; set; }

        public int CheckupAddedTodaySuccessCount { get; set; }

        public ObservableCollection<ProxyCheckupModel> ListFilteredCheckups
        {
            get { return _listFilteredCheckups; }
            set
            {
                _listFilteredCheckups = value;
                //if (!_isTapCard)
                //    IsExistPendingCard = _listFilteredCheckups != null
                //    && _listFilteredCheckups.Any(model => model.Status == CheckupStatus.Accepted && !model.IsPaid);
                //_isTapCard = false;
                RaisePropertyChanged("ListFilteredCheckups");
                ItemCount = value?.Count ?? -1;
            }
        }

        public ObservableCollection<ProxyCheckupModel> ListCheckups
        {
            get { return _listCheckups; }
            set
            {
                _listCheckups = value;

                IsExistPendingCard = _listCheckups != null
                    && _listCheckups.Any(model => model.Status == CheckupStatus.Accepted && !model.IsPaid);
                RaisePropertyChanged("ListCheckups");
                ListFilteredCheckups = value; // also reset filter list 
                ItemCount = value?.Count ?? -1;
            }
        }
        public bool IsExistPendingCard
        {
            get { return _isExistPendingCard; }
            set
            {
                _isExistPendingCard = value;
                RaisePropertyChanged();
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

        public ObservableCollection<ProxyCheckupModel> ListCheckupHistorical
        {
            get
            {
                ItemHistoryCount = _listCheckupHistorical?.Count ?? -1;
                return _listCheckupHistorical;
            }
            set
            {
                _listCheckupHistorical = value;
                RaisePropertyChanged("ListCheckupHistorical");
                ItemHistoryCount = value?.Count ?? -1;
            }
        }

        public int ItemHistoryCount
        {
            get { return _itemHistoryCount; }
            set
            {
                _itemHistoryCount = value;
                RaisePropertyChanged();
            }
        }

        public ProxyCheckupModel SelectedItem { get; set; }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand MedicalRecordPageApprearingCommand => _medicalRecordPageApprearingCommand ??
                                              (_medicalRecordPageApprearingCommand = new RelayCommand(async () =>
                                              {
                                                  await GetHistoricalCheckupList();
                                              }));

        public RelayCommand GoPaymentCommand => _goPaymentCommand ?? (_goPaymentCommand = new RelayCommand(GoPayment));

        public RelayCommand AddCheckupCommand => _addCheckupCommand ??
                                                 (_addCheckupCommand = new RelayCommand(async () =>
                                                 {
                                                     if (!UserViewModel.Instance.IsUserAddressCompleted())
                                                     {
                                                         if (await Common.ConfirmAsync(AppResources.update_address))
                                                             SettingViewModel.Instance.GoProfileCommand.Execute(null);
                                                     }
                                                     else if (!await Common.IsLimitAddCheckup())
                                                     {
                                                         // NavigationService.NavigateTo(typeof(PickHospitalPage));
                                                         HospitalViewModel.Instance.ResetKeywork();
                                                         NavigationService.NavigateTo(typeof(SearchHospitalPage));
                                                     }
                                                 }));

        public ICommand ItemTappedCommand
        {
            get
            {
                return _itemTappedCommand ??
                       (_itemTappedCommand =
                           new RelayCommand<CheckupModel>(
                              async item =>
                               {
                                   if (item != null)
                                   {
                                       SelectedItem = new ProxyCheckupModel(item);
                                       if (Common.OS == TargetPlatform.WinPhone)
                                       {
                                           UserDialogs.Instance.ShowLoading();
                                           await Task.Delay(100);
                                           UserDialogs.Instance.HideLoading();
                                       }
                                       NavigationService.NavigateTo(typeof(CheckupDetailPage));
                                   }
                               }));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new RelayCommand(async () =>
                    {
                        IsRefreshing = true;
                        await UserViewModel.Instance.RefreshData(false); //await GetCheckupList();
                        IsRefreshing = false;
                    });
                }
                return _refreshCommand;
            }
        }

        public ICommand GotoMedicalRecordPageCommand
        {
            get
            {
                return _goToMedicalRecordPageCommand ??
                       (_goToMedicalRecordPageCommand =
                           new RelayCommand(async () =>
                           {
                               if (await Common.CheckNetwordStatus())
                                   NavigationService.NavigateTo(typeof(MedicalRecordPage));
                           }));
            }
        }

        public ICommand GoToHospitalPageCommand
        {
            get
            {
                return _goToHospitalDetailPageCommand ?? (_goToHospitalDetailPageCommand = new RelayCommand(async () =>
                {
                    Common.ShowLoading();
                    await CommonViewModel.Instance.GetListCityAsync(false, false);
                    var vm = HospitalViewModel.Instance;
                    vm.SelectedHospitalDetail =
                        await vm.GetHosptialById(SelectedItem.Schedule.Hospital.Id);
                    if (vm.SelectedHospitalDetail == null) return;
                    var cityModel = CommonViewModel.Instance.ListCity.MapIdToList(vm.SelectedHospitalDetail.CityId);
                    vm.SelectedHospitalDetail.CityName = cityModel?.Name;
                    vm.SelectedHospitalDetail.DistrictName = cityModel?.Districts?.FirstOrDefault(
                        x => x.Id.Equals(vm.SelectedHospitalDetail.DistrictId))?.Name;
                    UserDialogs.Instance.HideLoading();
                    NavigationService.NavigateTo(typeof(HospitalDetailPage));
                }));
            }
        }

        private ICommand _deleteCheckupCommand;

        public ICommand DeleteCheckupCommand
        {
            get
            {
                return _deleteCheckupCommand ?? (_deleteCheckupCommand = new RelayCommand(async () =>
                {
                    if (SelectedItem != null)
                    {
                        var isCancelling = SelectedItem.Status == CheckupStatus.Accepted;
                        var confirmText = isCancelling ? (SelectedItem.IsPaid ?
                            string.Format(AppResources.checkup_cancel_confirm_with_fee, SelectedItem.CancelCheckupFee)
                            : AppResources.checkup_cancel_confirm)
                            : AppResources.checkup_delete_confirm;

                        if (await Common.ConfirmAsync(confirmText))
                        {
                            Common.ShowLoading();
                            var success = isCancelling ? await CancelCheckup(SelectedItem.Id) :
                                                        await DeleteCheckup(SelectedItem.Id);
                            if (success)
                            {
                                await Common.AlertAsync(isCancelling ?
                                    AppResources.checkup_cancelled_success :
                                    AppResources.checkup_delete_success);
                                await UserViewModel.Instance.RefreshData(true);
                                NavigationService.GoBack();
                                //Refresh list Checkup and go back
                            }
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                }));
            }
        }


        private ICommand _editCheckupScheduleCommand;

        public ICommand EditScheduleCommand
        {
            get
            {
                return _editCheckupScheduleCommand ?? (_editCheckupScheduleCommand = new RelayCommand(async () =>
                {
                    if (SelectedItem != null)
                    {
                        Common.ShowLoading();
                        DoctorScheduleViewModel.Instance.ResetDateTimeParams();
                        //Bug: Must load DateTime data before set item source for RadCalander
                        if (
                            await
                                DoctorScheduleViewModel.Instance.LoadData(SelectedItem.Schedule.Doctor.Id,
                                    SelectedItem.Schedule.Hospital.Id))
                        {
                            UserDialogs.Instance.HideLoading();
                            DoctorScheduleViewModel.Instance.SelectedDate = SelectedItem.Schedule.StartDateTime.Date;
                            AddCheckupViewModel.Instance.SelectedDoctor =
                                new ProxyDoctorModel(SelectedItem.Schedule.Doctor);
                            AddCheckupViewModel.Instance.SelectedHospital =
                                new ProxyHospitalModel(SelectedItem.Schedule.Hospital);
                            //                          AddCheckupViewModel.Instance.SelectedSchedule = SelectedItem.Schedule;
                            AddCheckupViewModel.Instance.SelectedSchedule = null;
                            DoctorScheduleViewModel.Instance.CheckupFlowGoNextCommand = SaveEditCheckupScheduleCommand;
                            NavigationService.NavigateTo(typeof(DoctorSchedulePage));
                        }
                        else
                            UserDialogs.Instance.HideLoading();
                    }
                }));
            }
        }


        private ICommand _resetLoadMoreCheckupHistoricalsCommand;

        public ICommand ResetLoadMoreCheckupHistoricalsCommand => _resetLoadMoreCheckupHistoricalsCommand ??
                                                                  (_resetLoadMoreCheckupHistoricalsCommand =
                                                                      new RelayCommand(ResetLoadMoreHistoricals));

        private ICommand _loadMoreCheckupHistoricalsCommand;

        public ICommand LoadMoreCheckupHistoricalsCommand
        {
            get
            {
                return _loadMoreCheckupHistoricalsCommand ??
                       (_loadMoreCheckupHistoricalsCommand = new RelayCommand(async () =>
                       {
                           Common.ShowLoading();
                           await LoadMoreHistoricals(_selectedUserId);
                           UserDialogs.Instance.HideLoading();
                       }));
            }
        }


        private ICommand _saveEditCheckupScheduleCommand;
        private bool _isSelectRedFilter;
        private bool _isSelectYellowFilter;
        private bool _isSelectGreenFilter;

        public ICommand SaveEditCheckupScheduleCommand
        {
            get
            {
                if (_saveEditCheckupScheduleCommand == null)
                {
                    _saveEditCheckupScheduleCommand = new RelayCommand(async () =>
                    {
                        if (AddCheckupViewModel.Instance.SelectedSchedule != null)
                        {
                            Common.ShowLoading();
                            SelectedItem.Schedule = AddCheckupViewModel.Instance.SelectedSchedule;
                            if (await EditCheckup())
                            {
                                //await CheckupViewModel.Instance.GetCheckupList();
                                //ToolbarViewModel.Instance.SetSelectedPage(typeof(CheckupPage));
                                await Common.AlertAsync(AppResources.checkup_change_schedule_success);
                                await UserViewModel.Instance.RefreshData(true);
                                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            await Common.AlertAsync(AppResources.empty_checkup_schedule);
                        }
                    });
                }
                return _saveEditCheckupScheduleCommand;
            }
        }

        public RelayCommand RedFilter => _redFilter ??
                                         (_redFilter = new RelayCommand(() =>
                                         {
                                             _isTapCard = true;
                                             FilterCheckup("Red");
                                         }));

        public RelayCommand YellowFilter => _yellowFilter ??
                                            (_yellowFilter = new RelayCommand(() =>
                                            {
                                                _isTapCard = true;
                                                FilterCheckup("Yellow");
                                            }));

        public RelayCommand GreenFilter => _greenFilter ??
                                           (_greenFilter = new RelayCommand(() =>
                                           {
                                               _isTapCard = true;
                                               FilterCheckup("Green");
                                           }));

        private ICommand _setRatingCommand;

        public ICommand SetRatingCommand => _setRatingCommand ??
                                                                  (_setRatingCommand =
                                                                      new RelayCommand(async () =>
                                                                      {
                                                                          if (SelectedItem?.Rating > 0)
                                                                              await SetRatingCheckup(SelectedItem);
                                                                          else
                                                                              await
                                                                                  Common.AlertAsync(
                                                                                      AppResources.rate_zero_value);
                                                                      }));
        public bool IsSelectRedFilter
        {
            get { return _isSelectRedFilter; }
            set
            {
                _isSelectRedFilter = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSelectYellowFilter
        {
            get { return _isSelectYellowFilter; }
            set
            {
                _isSelectYellowFilter = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSelectGreenFilter
        {
            get { return _isSelectGreenFilter; }
            set
            {
                _isSelectGreenFilter = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCanLoadMore
        {
            get { return _isCanLoadMore; }
            set
            {
                _isCanLoadMore = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        private string _statusColor;
        private string _filteredHospitalId = AppConstant.IdAllItems;
        private int _startIndex;
        private int _page;
        private bool _isCanLoadMore;
        private string _selectedUserId;
        private RelayCommand _medicalRecordPageApprearingCommand;

        public void ResetFilter()
        {
            _filteredHospitalId = AppConstant.IdAllItems;
            _statusColor = null;
            FilterCheckup();
        }

        public void FilterCheckup(string statusColor = null, string filteredHospitalId = null)
        {
            var filterList = ListCheckups.CloneJson();

            if (statusColor == null)
                statusColor = _statusColor;
            else if (_statusColor == statusColor)
                _statusColor = statusColor = null;
            else
                _statusColor = statusColor;

            if (filteredHospitalId == null)
                filteredHospitalId = _filteredHospitalId;
            else
                _filteredHospitalId = filteredHospitalId;

            if (_filteredHospitalId == null || !_filteredHospitalId.Equals(AppConstant.IdAllItems))
            {
                filterList = new ObservableCollection<ProxyCheckupModel>(filterList.Where(
                    x => x.Schedule.Hospital.Id.Equals(_filteredHospitalId)));
                IsExistPendingCard = filterList.Any(model => model.Status == CheckupStatus.Accepted && !model.IsPaid);
            }
            else if (_filteredHospitalId.Equals(AppConstant.IdAllItems))
            {
                IsExistPendingCard = _listCheckups != null
                       && _listCheckups.Any(model => model.Status == CheckupStatus.Accepted && !model.IsPaid);
            }

            switch (statusColor)
            {
                case "Red":
                    filterList = new ObservableCollection<ProxyCheckupModel>(
                        filterList.Where(x => x.Status == CheckupStatus.Pending || x.Status == CheckupStatus.Rejected));
                    //filterList.Where(x => x.Status == CheckupStatus.Rejected));//chỉ show phiếu khám bị từ chối                    
                    SetFilter(true);
                    break;
                case "Yellow":
                    filterList = new ObservableCollection<ProxyCheckupModel>(
                        filterList.Where(x => x.Status == CheckupStatus.Accepted && !x.IsPaid));
                    SetFilter(isYellowFilter: true);
                    break;
                case "Green":
                    filterList = new ObservableCollection<ProxyCheckupModel>(
                        filterList.Where(x => x.Status == CheckupStatus.Accepted && x.IsPaid));
                    SetFilter(isGreenFilter: true);
                    break;
                default:
                    SetFilter();
                    break;
            }
            //if(!string.IsNullOrEmpty(statusColor))
            //_isTapCard = true;
            ListFilteredCheckups = filterList;
        }

        private void SetFilter(bool isRedFilter = false, bool isYellowFilter = false, bool isGreenFilter = false)
        {
            IsSelectRedFilter = isRedFilter;
            IsSelectYellowFilter = isYellowFilter;
            IsSelectGreenFilter = isGreenFilter;
            RaisePropertyChanged("isRedFilter");
            RaisePropertyChanged("isYellowFilter");
            RaisePropertyChanged("isGreenFilter");
        }

        public async Task GetCheckupList()
        {
            try
            {
                //checkupVm.ResetFilter();

                //checkupVm.ListCheckups = new ObservableCollection<CheckupModel>(
                //    checkupsData.Where(x => x.Status != CheckupStatus.Finished &&
                //        x.Status != CheckupStatus.Deleted));
                var listCheckupRaw = (await _checkupWS.GetCheckupList()).ToBaseModel<CheckupModel, ApiCheckupModel>();

                ResetFilter();

                _listCheckups = new ObservableCollection<ProxyCheckupModel>(
                    listCheckupRaw.Where(x => x.Status != CheckupStatus.Finished &&
                                              x.Status != CheckupStatus.Deleted).Select(x => new ProxyCheckupModel(x)));
                _listFilteredCheckups = _listCheckups;


                HospitalViewModel.Instance.ListHospitalFilter =
                    HospitalViewModel.Instance.GetListHospitalFromCheckupsData(
                        _listCheckups.ToBaseModel<CheckupModel, ProxyCheckupModel>());

                RaisePropertyChanged("ListCheckups");
                RaisePropertyChanged("ListFilteredCheckups");
            }
            catch (Exception e)
            {
                //await Common.AlertAsync(e.Message);
            }
        }


        public void ResetLoadMoreHistoricals()
        {
            ListCheckupHistorical = new ObservableCollection<ProxyCheckupModel>();
            _page = 0;
            _startIndex = 0;
            IsCanLoadMore = true;
            ItemHistoryCount = -1;
        }

        public async Task LoadMoreHistoricals(string userId)
        {
            try
            {
                _selectedUserId = userId;
                if (!IsCanLoadMore || string.IsNullOrWhiteSpace(_selectedUserId)) return;

                Common.ShowLoading();

                _startIndex += _page > 0 ? AppConstant.ITEMS_PER_PAGE : 0;
                IsCanLoadMore = false;

                //TODO: Deactive
                var l =
                    (await _checkupWS.GetHistoricalCheckups(_selectedUserId, _startIndex, AppConstant.ITEMS_PER_PAGE))
                        .ToBaseModel<CheckupModel, ApiCheckupModel>();

                IsCanLoadMore = true;

                await Task.Delay(100);//make sure the IsCanLoadMore = true is notify to the HcListView CanLoadMoreProperty

                if (l.Count > 0)
                {
                    foreach (var hospitalModel in l)
                    {
                        ListCheckupHistorical.Add(new ProxyCheckupModel(hospitalModel));
                    }
                    RaisePropertyChanged("ListCheckupHistorical");
                    _page++;
                    IsCanLoadMore = true;
                }

                if (l.Count == 0 || l.Count < AppConstant.ITEMS_PER_PAGE)
                {
                    IsCanLoadMore = false;
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            finally
            {
                ItemHistoryCount = ListCheckupHistorical.Count;
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void GoPayment()
        {
            //if (CreditCardViewModel.Instance.ListCreditCardNoVnPay?.Count == 0)
            //{
            //    if (await Common.ConfirmAsync(AppResources.update_creadit_card))
            //    {
            //        CreditCardViewModel.Instance.GoAddCard.Execute(null);
            //    }
            //}
            //else
            //{
            //    NavigationService.NavigateTo(typeof(PaymentPage));
            //}

            CreditCardViewModel.Instance.ClearViewModel();
            NavigationService.NavigateTo(typeof(PaymentPage));
        }

        private async Task<bool> DeleteCheckup(string checkupId)
        {
            try
            {
                await _checkupWS.DeleteCheckup(checkupId);
                return true;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            return false;
        }

        private async Task<bool> CancelCheckup(string checkupId)
        {
            try
            {
                await _checkupWS.CancelCheckup(checkupId);
                return true;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            return false;
        }

        private async Task<bool> EditCheckup()
        {
            try
            {
                await _checkupWS.EditCheckup(SelectedItem.Id, SelectedItem.Schedule.Id);
                return true;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            return false;
        }


        public void SetLocalNotificationForCheckups()
        {
            foreach (var checkupAccepted in ListCheckups.Where(x => x.Status == CheckupStatus.Accepted && x.IsPaid))
            {
                Common.SetNotificationForCheckup(checkupAccepted);
            }
        }

        public void ResetMedicalRecordData()
        {
            UserViewModel.Instance.SelectedRelatedAccount = null;
            ListCheckupHistorical = new ObservableCollection<ProxyCheckupModel>();
        }

        public async Task SetRatingCheckup(ProxyCheckupModel checkupModel)
        {
            try
            {
                Common.ShowLoading();
                await _checkupWS.RatingCheckup(checkupModel.Id, checkupModel.Rating);
                checkupModel.RaiseIsRatedPropertyChanged();
                IsRatedSuccess = true;
                await Common.AlertAsync(AppResources.rated_successfully);
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }


        public async Task GetHistoricalCheckupList()
        {
            if (IsRatedSuccess)
            {
                ResetLoadMoreHistoricals();
                Common.ShowLoading();
                await LoadMoreHistoricals(_selectedUserId);
                UserDialogs.Instance.HideLoading();
            }
            IsRatedSuccess = false;
        }

        public int GetCountPaidCheckup()
        {
            return ListCheckups?.Count(x => x.IsPaid) ?? 0;
        }

        public async void ReloadCheckupWhenScheduleCancel(string notifyContent)
        {
            if (!string.IsNullOrWhiteSpace(notifyContent) &&
                (notifyContent.StartsWith("Lịch khám của bạn đã bị đổi. LH: 08 6274 7451") ||
                 notifyContent.StartsWith("Your appoinment has been changed. Call: 08 6274 7451")))
            {
                await UserViewModel.Instance.RefreshData(true);
            }

        }
        #endregion
    }
}