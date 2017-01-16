using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Pages;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.ViewModels
{
    public class PromotionViewModel : BaseViewModel<PromotionViewModel>
    {
        public PromotionViewModel(INavigationService navigationService, IPromotionWS service) : base(navigationService)
        {
            _service = service;
            IsCanLoadMore = true;
            ListItemLoadmore = new ObservableCollection<PromotionModel>();
        }

        #region Properties

        private readonly IPromotionWS _service;
        private ObservableCollection<PromotionModel> _listItemLoadmore;
        private PromotionModel _selectedItem;
        private ICommand _itemTappedCommand;
        private ICommand _pullRefreshCommand;
        private ICommand _loadMoreTappedCommand;
        private int _page;
        private int _startIndex;
        private bool _isRefreshing;
        private bool _isCanLoadMore;


        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged();
            }
        }

        public PromotionModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<PromotionModel> ListItemLoadmore
        {
            get { return _listItemLoadmore; }
            set
            {
                _listItemLoadmore = value;
                RaisePropertyChanged("ListItemLoadmore");
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

        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand PullRefreshCommand
        {
            get
            {
                return _pullRefreshCommand ?? (_pullRefreshCommand = new RelayCommand(async () =>
                {
                    IsRefreshing = true;
                    ResetLoadMore();
                    await LoadMore(false);
                    IsRefreshing = false;
                }));
            }
        }

        public ICommand ItemTappedCommand
        {
            get
            {
                return _itemTappedCommand ?? (_itemTappedCommand = new RelayCommand<PromotionModel>(model =>
                {
                    if (model != null)
                    {
                        SelectedItem = model;
                        NavigationService.NavigateTo(typeof (PromotionDetailPage));
                    }
                }));
            }
        }


        public ICommand LoadMoreTappedCommand
        {
            get
            {
                return _loadMoreTappedCommand ??
                       (_loadMoreTappedCommand = new RelayCommand(async () => { await LoadMore(true); }));
            }
        }


        private ICommand _getListPromotionCommand;
        private int _itemCount;

        public ICommand GetListPromotionCommand
        {
            get
            {
                return _getListPromotionCommand ?? (_getListPromotionCommand = new RelayCommand(async () =>
                {
                    ResetLoadMore();
                    await LoadMore(true);
                }));
            }
        }

        #endregion

        #region Methods

        private async Task LoadMore(bool isShowLoading)
        {
            try
            {
                if (!IsCanLoadMore) return;

                if (isShowLoading)
                    Common.ShowLoading();

                _startIndex += _page > 0 ? AppConstant.ITEMS_PER_PAGE : 0;
                IsCanLoadMore = false;
                var l = await _service.GetPromotionList(_startIndex, AppConstant.ITEMS_PER_PAGE);
                IsCanLoadMore = true;

                await Task.Delay(100);//make sure the IsCanLoadMore = true is notify to the HcListView CanLoadMoreProperty

                if (l.Count > 0)
                {
                    foreach (var promotionModel in l)
                    {
                        _listItemLoadmore.Add(promotionModel);
                    }
                    RaisePropertyChanged("ListItemLoadmore");
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
                ItemCount = ListItemLoadmore.Count;
                UserDialogs.Instance.HideLoading();
            }
        }

        private void ResetLoadMore()
        {
            _page = 0;
            _startIndex = 0;
            IsCanLoadMore = true;
            ItemCount = -1;
            _listItemLoadmore = new ObservableCollection<PromotionModel>();
        }

        #endregion
    }
}