using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading;
using HealthCare.Exceptions;
using System.Diagnostics;
using System.Windows.Input;

namespace HealthCare.ViewModels
{
    public class SearchViewModel : BaseViewModel<SearchViewModel>
    {
        private readonly ISearchWS _searchWS;

        private RelayCommand _chooseDoctorCommand,
            _goDetailDoctorCommand,
            _clearSearchCommand,
            _goLocationDoctorCommand,
            _goSupportCommand,
            _textChangeCommand,
            _cityDistrictSelectionChangedCommand;

        private bool _isFocusSearch, _isShowSuggestion, _isShowClearSearch, _isShowGroupButton, _isShowHint = true;
        private bool _isShowEmpty;
        private bool _keyboardHidden;
        private Suggestion _keyword;
        private ObservableCollection<ProxyDoctorModel> _listDoctorBySearch;
        private ObservableCollection<Suggestion> _listSuggestion;
        private Color _searchBorderColor = HcStyles.BorderLightGrayColor;
        private ProxyDoctorModel _selectedDoctor;
        private RelayCommand<ProxyDoctorModel> _tapDoctorListCommand;
        private RelayCommand<Suggestion> _tapListSuggestionCommand;
        private CancellationTokenSource _cancellationTS;
        private bool _isShowSuggestLoading, _isShowNotFound;
        private int _numberOfCheckup = 0;

        public SearchViewModel(INavigationService ns, ISearchWS searchWS) : base(ns)
        {
            _searchWS = searchWS;
        }

        #region Property

        public Suggestion Keyword
        {
            get { return _keyword ?? (_keyword = new Suggestion()); }
            set
            {
                _keyword = value;
                RaisePropertyChanged("Keyword");
            }
        }

        public bool KeyboardHidden
        {
            get { return _keyboardHidden; }
            set
            {
                _keyboardHidden = value;
                RaisePropertyChanged("KeyboardHidden");
            }
        }

        public ObservableCollection<Suggestion> ListSuggestion
        {
            get { return _listSuggestion; }
            set
            {
                _listSuggestion = value;
                RaisePropertyChanged("ListSuggestion");
            }
        }

        public bool IsFocusSearch
        {
            get { return _isFocusSearch; }
            set
            {
                _isFocusSearch = value;
                if (value && !UserViewModel.Instance.IsUserAddressCompleted())
                {
                    if (_isConfirmedShowFilledUserAddress)
                        ShowFilledUserAddress();
                    _isConfirmedShowFilledUserAddress = false;
                }
                else
                {
                    if (Common.OS != TargetPlatform.Android)
                        SearchBorderColor = value ? HcStyles.GreenColor : HcStyles.BorderBlueGrayColor;
                    IsShowSuggestion = value ? true : ListSuggestion?.Count == 0 ? false : true;
                }
                RaisePropertyChanged("IsFocusSearch");
            }
        }
        private bool _isConfirmedShowFilledUserAddress = true;

        private async void ShowFilledUserAddress()
        {
            if (await Common.ConfirmAsync(AppResources.update_address))
                SettingViewModel.Instance.GoProfileCommand.Execute(null);
            HideKeyboard();
            _isConfirmedShowFilledUserAddress = true;
        }

        public bool IsShowSuggestion
        {
            get { return _isShowSuggestion; }
            set
            {
                _isShowSuggestion = value;
                RaisePropertyChanged("IsShowSuggestion");
            }
        }

        public bool IsShowHint
        {
            get { return _isShowHint; }
            set
            {
                _isShowHint = value;
                RaisePropertyChanged("IsShowHint");
            }
        }

        public bool IsShowEmpty
        {
            get { return _isShowEmpty; }
            set
            {
                _isShowEmpty = value;
                RaisePropertyChanged();
            }
        }

        public bool IsShowClearSearch
        {
            get { return _isShowClearSearch; }
            set
            {
                _isShowClearSearch = value;
                RaisePropertyChanged("IsShowClearSearch");
            }
        }

        public bool IsShowGroupButton
        {
            get { return _isShowGroupButton; }
            set
            {
                _isShowGroupButton = value;
                RaisePropertyChanged("IsShowGroupButton");
            }
        }

        public Color SearchBorderColor
        {
            get { return _searchBorderColor; }
            set
            {
                _searchBorderColor = value;
                RaisePropertyChanged("SearchBorderColor");
            }
        }

        public ObservableCollection<ProxyDoctorModel> ListDoctorBySearch
        {
            get { return _listDoctorBySearch; }
            set
            {
                _listDoctorBySearch = value;
                RaisePropertyChanged("ListDoctorBySearch");
            }
        }

        public ProxyDoctorModel SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                if (value != null)
                {
                    _selectedDoctor = value;
                    if (value.CheckupCount != null)
                    {
                        NumberOfCheckup = value.CheckupCount;
                    }
                    //
                    RaisePropertyChanged("SelectedDoctor");
                }
            }
        }

        public string MappedDoctorAddress
        {
            get
            {
                if (SelectedDoctor is DoctorApiModel)
                {
                    var doctorInfo = (SelectedDoctor as DoctorApiModel).DoctorInfos;
                    if (doctorInfo != null)
                        foreach (var info in doctorInfo)
                            if (Common.compareLocation(info.Hospital.Location, SelectedDoctor.Location))
                                return info.Hospital.Address;
                }
                return string.Empty;

            }
        }

        public bool IsShowSuggestLoading
        {
            get { return _isShowSuggestLoading; }
            set { _isShowSuggestLoading = value; RaisePropertyChanged("IsShowSuggestLoading"); }
        }

        public bool IsShowNotFound
        {
            get { return _isShowNotFound; }
            set { _isShowNotFound = value; RaisePropertyChanged("IsShowNotFound"); }
        }

        public int NumberOfCheckup
        {
            get { return _numberOfCheckup; }
            set
            {
                if (value != null)
                {
                    _numberOfCheckup = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand ChooseDoctorCommand => _chooseDoctorCommand ??
                                                   (_chooseDoctorCommand = new RelayCommand(ChooseDoctor));

        public RelayCommand GoDetailDoctorCommand => _goDetailDoctorCommand ??
                                                     (_goDetailDoctorCommand = new RelayCommand(GoDetailDoctor));

        public RelayCommand GoLocationDoctorCommand => _goLocationDoctorCommand ??
                                                       (_goLocationDoctorCommand = new RelayCommand(GoLocationDoctor));

        public RelayCommand GoSupportCommand => _goSupportCommand ??
                                                (_goSupportCommand = new RelayCommand(GoSupport));

        public RelayCommand TextChangeCommand => _textChangeCommand ??
                                                 (_textChangeCommand = new RelayCommand(HandleChangedText));

        public RelayCommand ClearSearchCommand => _clearSearchCommand ??
                                                  (_clearSearchCommand = new RelayCommand(ClearKeyword));

        public RelayCommand<Suggestion> TapListSuggestionCommand => _tapListSuggestionCommand ??
                                                                    (_tapListSuggestionCommand =
                                                                        new RelayCommand<Suggestion>(TapListSuggestion))
            ;

        public RelayCommand CityDistrictSelectionChangedCommand => _cityDistrictSelectionChangedCommand ??
                                                                   (_cityDistrictSelectionChangedCommand =
                                                                       new RelayCommand(
                                                                           () => { TapListSuggestion(Keyword); }));

        public RelayCommand<ProxyDoctorModel> TapDoctorListCommand => _tapDoctorListCommand ??
                                                                      (_tapDoctorListCommand =
                                                                          new RelayCommand<ProxyDoctorModel>(
                                                                              TapDoctorList));

        #endregion

        #region Method

        private async void ShowSendRequestDoctorDialog()
        {
            var isShow = await Common.ConfirmAsync(AppResources.ask_to_send_request, AppResources.more_support_send, AppResources.cancel);
            if (isShow)
                GoSupport();
        }

        public void TapDoctorList(ProxyDoctorModel selectedDoctor)
        {
            SelectedDoctor = selectedDoctor;
        }

        public async void TapListSuggestion(Suggestion tappedItem)
        {
            HideKeyboard();
            IsShowSuggestion = false;
            _cancellationTS?.Cancel();
            var tmpItem = tappedItem;
            ClearSuggestions();
            await Task.Delay(AppConstant.DelayBinding);
            if (tmpItem == null || string.IsNullOrWhiteSpace(tmpItem.Name))
                return;

            Keyword = tmpItem;

            ResetLoadMoreDoctorList();
            await LoadMoreDoctorList(true);
            SelectedDoctor = null;
            if (ListDoctorBySearch?.Count > 0)
            {
                //có data
                IsShowHint = false;
                IsShowEmpty = false;
                IsShowGroupButton = true;
            }
            else
            {
                //không data
                IsShowEmpty = !string.IsNullOrWhiteSpace(Keyword?.Name);
                IsShowHint = !IsShowEmpty;
                IsShowGroupButton = false;
            }
            UserDialogs.Instance.HideLoading();
        }


        private ICommand _resetLoadMoreDoctorListCommand;

        public ICommand ResetLoadMoreDoctorListCommand => _resetLoadMoreDoctorListCommand ??
                                                                  (_resetLoadMoreDoctorListCommand =
                                                                      new RelayCommand(ResetLoadMoreDoctorList));

        private ICommand _loadMoreDoctorListCommand;

        public ICommand LoadMoreDoctorListCommand
        {
            get
            {
                return _loadMoreDoctorListCommand ??
                       (_loadMoreDoctorListCommand = new RelayCommand(async () =>
                       {
                           await LoadMoreDoctorList(true);
                           UserDialogs.Instance.HideLoading();
                       }));
            }
        }

        private void ResetLoadMoreDoctorList()
        {
            _page = 0;
            _startIndex = 0;
            IsCanLoadMore = true;
            ItemCount = -1;
            ListDoctorBySearch = new ObservableCollection<ProxyDoctorModel>();
        }

        public bool IsCanLoadMore
        {
            get { return _isCanLoadMore; }
            set { _isCanLoadMore = value; RaisePropertyChanged(); }
        }

        private async Task LoadMoreDoctorList(bool isShowLoading)
        {
            try
            {
                if (!IsCanLoadMore) return;

                if (isShowLoading)
                    Common.ShowLoading();

                _startIndex += _page > 0 ? AppConstant.ITEMS_PER_PAGE : 0;
                IsCanLoadMore = false;
                var currentPos = DoctorLocationViewModel.Instance.CurrentLocation;
                var lat = currentPos.Latitude;
                var lng = currentPos.Longitude;
                var city = CommonViewModel.Instance.SelectedCityForSearch;
                var district = CommonViewModel.Instance.SelectedDistrictForSearch;
                var l = (await _searchWS.GetDoctorListBySearch
                    (Keyword, city.Id, district.Id, lat, lng, _startIndex, AppConstant.ITEMS_PER_PAGE))
                    .ToBaseModel<ProxyDoctorModel, DoctorApiModel>();

                IsCanLoadMore = true;

                await Task.Delay(100);//make sure the IsCanLoadMore = true is notify to the HcListView CanLoadMoreProperty

                if (l.Count > 0)
                {
                    foreach (var model in l)
                    {
                        ListDoctorBySearch.Add(model);
                    }
                    RaisePropertyChanged("ListDoctorBySearch");
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
                ItemCount = ListDoctorBySearch.Count;
                UserDialogs.Instance.HideLoading();
            }

        }

        private int _startIndex, _page;
        private bool _isCanLoadMore;
        private int _itemCount;

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; RaisePropertyChanged(); }
        }

        public void HandleChangedText()
        {
            _cancellationTS?.Cancel();
            ClearSuggestions();
            if (IsFocusSearch && !string.IsNullOrWhiteSpace(Keyword.Name))
                GetSuggestions();

            if (string.IsNullOrEmpty(Keyword.Name))
                IsShowClearSearch = false;
            else
                IsShowClearSearch = true;
        }

        public void ResetSearch()
        {
            ClearKeyword();
            ResetLoadMoreDoctorList();
            IsShowEmpty = !string.IsNullOrWhiteSpace(Keyword?.Name);
            IsShowHint = !IsShowEmpty;
            IsShowGroupButton = false;
            //ListDoctorBySearch = new ObservableCollection<ProxyDoctorModel>();
        }

        public void ClearKeyword()
        {
            Keyword = null;
            if (ListDoctorBySearch == null || ListDoctorBySearch.Count == 0)
            {
                IsShowEmpty = false;
                IsShowHint = true;
            }
            ClearSuggestions();
        }

        public void ClearSuggestions()
        {
            ListSuggestion = new ObservableCollection<Suggestion>();
            IsShowSuggestLoading = false;
            IsShowNotFound = false;
        }

        private async void GetSuggestions()
        {
            _cancellationTS = new CancellationTokenSource();
            try
            {
                ShowSuggestLoading(_cancellationTS.Token);
                ListSuggestion = await _searchWS.GetSuggestions(Keyword, _cancellationTS.Token);
                IsShowSuggestLoading = false;
                IsShowNotFound = ListSuggestion == null || ListSuggestion.Count == 0;
            }
            catch (OperationCanceledException) { }
            catch (NetworkException) { IsShowSuggestLoading = false; IsShowNotFound = true; }
            catch (InvalidOperationException e) { if (e.HResult != -2146233079) await Common.AlertAsync(e.Message); IsShowSuggestLoading = false; }
            catch (Exception e) { await Common.AlertAsync(e.Message); IsShowSuggestLoading = false; }
        }

        private void ShowSuggestLoading(CancellationToken token)
        {
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(AppConstant.DelaySearchSuggestions);
                    token.ThrowIfCancellationRequested();
                    IsShowSuggestLoading = true;
                }
                catch (OperationCanceledException) { }
                catch (Exception) { IsShowSuggestLoading = false; }
            }, token);
        }

        public async void ChooseDoctor()
        {
            if (await IsDoctorSelected())
            {
                if (!await Common.IsLimitAddCheckup())
                {
                    AddCheckupViewModel.Instance.SelectedHospital = null; //clear selected hospital
                    AddCheckupViewModel.Instance.SelectedDoctor = SelectedDoctor;
                    AddCheckupViewModel.Instance.GoToSchedulePageCommand.Execute(null);
                }
            }
        }

        public async void GoDetailDoctor()
        {
            if (await IsDoctorSelected())
                NavigationService.NavigateTo(typeof(DoctorDetailPage));
        }

        public async void GoLocationDoctor()
        {
            if (await IsDoctorSelected())
            {
                if (SelectedDoctor.Location.Latitude != 0 && SelectedDoctor.Location.Longitude != 0)
                {
                    //NavigationService.NavigateTo(typeof (DoctorLocationPage));
                    LaunchNaviveMapApplication(SelectedDoctor);
                }
                else
                    await Common.AlertAsync(AppResources.doctor_location_error);
            }
        }

        public void LaunchNaviveMapApplication(DoctorModel model)
        {
            // Windows Phone doesn't like ampersands in the names and the normal URI escaping doesn't help
            var name = model.FullName.Replace("&", "and"); // var name = Uri.EscapeUriString(place.Name);
            var loc = string.Format("{0},{1}", model.Location.Latitude, model.Location.Longitude);
            var addr = Uri.EscapeUriString(model.Address);

            var request = Device.OnPlatform(
              // iOS doesn't like %s or spaces in their URLs, so manually replace spaces with +s
              //string.Format("http://maps.apple.com/maps?q={0}&sll={1}", name.Replace(' ', '+'), loc),
              string.Format("http://maps.apple.com/maps?sll={0}&q={1}", name, loc),

              // pass the address to Android if we have it
              //string.Format("geo:0,0?q={0}({1})", string.IsNullOrWhiteSpace(addr) ? loc : addr, name),
              string.Format("geo:0,0?q={0}({1})", loc, name),

              // WinPhone
              //string.Format("bingmaps:?cp={0}&q={1}", loc, name)
              string.Format("bingmaps:?cp={0}&q={1}", name, loc)
            );

            Device.OpenUri(new Uri(request));
        }

        public void GoSupport()
        {
            MoreSupportViewModel.Instance.FillUserData();
            NavigationService.NavigateTo(typeof(MoreSupportPage));
        }

        private async Task<bool> IsDoctorSelected()
        {
            if (SelectedDoctor != null) return true;
            await Common.AlertAsync(AppResources.not_selected_doctor_search);
            return false;
        }


        /// <summary>
        ///     Change KeyboardHidden property to raise event hide keyboard
        /// </summary>
        public void HideKeyboard()
        {
            KeyboardHidden = !KeyboardHidden;
        }

        #endregion
    }
}