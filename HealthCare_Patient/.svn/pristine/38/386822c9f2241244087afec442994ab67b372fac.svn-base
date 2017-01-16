using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class HospitalViewModel : BaseViewModel<HospitalViewModel>
    {
        private readonly IHospitalWS _hospitalWS;
        private int _hospitalCount;
        private HospitalModel _hospitalFilter;
        private bool _isCanLoadMore;
        private bool _isRefreshing;
        private ObservableCollection<HospitalModel> _listAllOfHospitalLoadMore;
        private ObservableCollection<HospitalModel> _listHospital, _listHospitalFilter;
        private RelayCommand<HospitalModel> _listViewClickCommand;
        private RelayCommand _loadAllHospitalCommand, _nextCommand, _cancelCommand;
        private int _page;
        private HospitalModel _selectedHospital, _selectedHospitalDetail, _selectedHospitalFilter;
        private int _startIndex;

        public HospitalViewModel(INavigationService ns, IHospitalWS hospitalWS)
            : base(ns)
        {
            _hospitalWS = hospitalWS;
            SearchBorderColor = HcStyles.BorderLightGrayColor;
        }

        #region Property


        #region Search
        public Color SearchBorderColor
        {
            get { return _searchBorderColor; }
            set { _searchBorderColor = value; RaisePropertyChanged(); }
        }

        public string Keyword
        {
            get { return _keyword; }
            set { _keyword = value; RaisePropertyChanged(); }
        }

        public bool IsFocusSearch
        {
            get { return _isFocusSearch; }
            set
            {
                _isFocusSearch = value;
                if (Common.OS != TargetPlatform.Android)
                    SearchBorderColor = value ? HcStyles.GreenColor : HcStyles.BorderBlueGrayColor;
                RaisePropertyChanged("IsFocusSearch");
            }
        }

        public RelayCommand TextChangeCommand => _textChangeCommand ??
                                              (_textChangeCommand = new RelayCommand(() =>
                                              {
                                                  IsShowClearSearch = !string.IsNullOrEmpty(Keyword);
                                              }));

        public RelayCommand ClearSearchCommand => _clearSearchCommand ??
                                            (_clearSearchCommand = new RelayCommand(() =>
                                            {
                                                Keyword = "";
                                            }));

        public ICommand GetListHospitalCommand
        {
            get
            {
                return _getListHospitalCommand ?? (_getListHospitalCommand = new RelayCommand(async () =>
                {
                    await GetListHospitalBySearch(false, true);
                }));
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
        #endregion

        public bool IsCanLoadMore
        {
            get { return _isCanLoadMore; }
            set
            {
                _isCanLoadMore = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<HospitalModel> ListHospital
        {
            get { return _listHospital; }
            set
            {
                _listHospital = value;
                RaisePropertyChanged("ListHospital");
            }
        }

        public ObservableCollection<HospitalModel> ListHospitalFilter
        {
            get { return _listHospitalFilter; }
            set
            {
                SelectedHospitalFilter = null;
                var AllHospitalItem = new HospitalModel { Id = AppConstant.IdAllItems, Name = AppResources.all };
                if (!(value.Where(x => x.Id == AppConstant.IdAllItems)).Any())
                    value.Insert(0, AllHospitalItem);
                _listHospitalFilter = value;
                RaisePropertyChanged("ListHospitalFilter");
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

        /// <summary>
        ///     This property use for HospialDetailPage, PickHospitalPage
        /// </summary>
        public HospitalModel SelectedHospitalDetail
        {
            get { return _selectedHospitalDetail; }
            set
            {
                _selectedHospitalDetail = value;
                RaisePropertyChanged("SelectedHospitalDetail");
            }
        }

        public HospitalModel SelectedHospitalFilter
        {
            get { return _selectedHospitalFilter; }
            set
            {
                _selectedHospitalFilter = value;
                RaisePropertyChanged("SelectedHospitalFilter");
                if (value != null) CheckupViewModel.Instance.FilterCheckup(filteredHospitalId: value.Id);
            }
        }

        public HospitalModel HospitalFilter
        {
            get { return _hospitalFilter; }
            set
            {
                _hospitalFilter = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<HospitalModel> ListAllOfHospitalLoadMore
        {
            get { return _listAllOfHospitalLoadMore; }
            set
            {
                _listAllOfHospitalLoadMore = value;
                RaisePropertyChanged("ListAllOfHospitalLoadMore");
                HospitalCount = value?.Count ?? -1;
            }
        }

        public int HospitalCount
        {
            get { return _hospitalCount; }
            set
            {
                _hospitalCount = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand LoadAllHospitalCommand =>
            _loadAllHospitalCommand ?? (_loadAllHospitalCommand = new RelayCommand(async () =>
            {
                ResetLoadMore();
                await GetListHospitalBySearch(false, true);
            }));


        private ICommand _loadMoreHospitalCommand;
        private Color _searchBorderColor;
        private string _keyword;
        private bool _isFocusSearch;
        private RelayCommand _textChangeCommand;
        private bool _isShowClearSearch;
        private RelayCommand _clearSearchCommand;
        private ICommand _getListHospitalCommand;

        public ICommand LoadMoreHospitalCommand
        {
            get
            {
                return _loadMoreHospitalCommand ??
                       (_loadMoreHospitalCommand = new RelayCommand(async () => { await GetHospitalListLoadMore(true, Keyword); }));
            }
        }

        public RelayCommand<HospitalModel> ListViewClickCommand => _listViewClickCommand ??
                                                                   (_listViewClickCommand =
                                                                       new RelayCommand<HospitalModel>(
                                                                           ListViewClickHandler));

        #endregion

        #region Methods
		
		public void ResetKeywork()
        {
            Keyword = "";
        }

        public async Task GetListHospitalBySearch(bool isResetKeyword, bool isShowLoadingDialog)
        {
            ResetLoadMore();
            Keyword = isResetKeyword ? "" : Keyword?.Trim();
            await GetHospitalListLoadMore(isShowLoadingDialog, Keyword);
        }

        private void ListViewClickHandler(HospitalModel data)
        {
            SelectedHospital = data;
        }

        private async Task GetHospitalListLoadMore(bool isShowLoading, string keySearch)
        {
            try
            {
                if (!IsCanLoadMore) return;

                if (isShowLoading)
                    Common.ShowLoading();

                _startIndex += _page > 0 ? AppConstant.ITEMS_PER_PAGE : 0;
                IsCanLoadMore = false;

                //TODO: Deactive
                var l = (await _hospitalWS.GetHospitalList(keySearch, _startIndex, AppConstant.ITEMS_PER_PAGE)).Where(x => x.Status).ToList();
                IsCanLoadMore = true;

                await Task.Delay(100);//make sure the IsCanLoadMore = true is notify to the HcListView CanLoadMoreProperty

                if (l.Count > 0)
                {
                    foreach (var hospitalModel in l)
                    {
                        ListAllOfHospitalLoadMore.Add(hospitalModel);
                    }
                    RaisePropertyChanged("ListAllOfHospitalLoadMore");
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
                HospitalCount = ListAllOfHospitalLoadMore.Count;
                UserDialogs.Instance.HideLoading();
            }
        }

        private void ResetLoadMore()
        {
            ListAllOfHospitalLoadMore = new ObservableCollection<HospitalModel>();
            _page = 0;
            _startIndex = 0;
            IsCanLoadMore = true;
            HospitalCount = -1;
        }

        private void Cancel()
        {
            NavigationService.GoBack();
        }

        public ObservableCollection<HospitalModel> GetListHospitalFromCheckupsData(IList<CheckupModel> checkups)
        {
            var listHospital = new ObservableCollection<HospitalModel>();
            foreach (var checkup in checkups)
            {
                var hospital = checkup.Schedule.Hospital;
                if (!listHospital.Contains(hospital))
                    listHospital.Add(hospital);
            }

            listHospital =
                new ObservableCollection<HospitalModel>(
                    listHospital.Where(p => p.Id != null).GroupBy(p => p.Id).Select(grp => grp.First()));

            return listHospital;
        }

        public async Task<HospitalModel> GetHosptialById(string id)
        {
            try
            {
                var result = await _hospitalWS.GetHospitalById(id);
                return result;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
            return null;
        }

        #endregion
    }
}