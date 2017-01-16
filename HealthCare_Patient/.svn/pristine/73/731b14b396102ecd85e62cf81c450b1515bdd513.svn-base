using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HealthCare.Pages;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class ToolbarViewModel : BaseViewModel<ToolbarViewModel>
    {
        private ICheckupWS _checkupWS;
        private ICommand _goPromotionCommand;
        private ICommand _goSettingCommand;
        private ICommand _refreshCommand;
        private Page _selectedPage;

        public ToolbarViewModel(INavigationService ns, ICheckupWS checkupWs) : base(ns)
        {
            _checkupWS = checkupWs;
            //ListPage = new ObservableCollection<Page>
            //{
            //    new SearchPage(),
            //    new CheckupPage(),
            //    new CreditCardPage()
            //};
            //SelectedPage = ListPage[0];
        }

        #region Method

        public async void RefreshData()
        {
            await UserViewModel.Instance.RefreshData(true);
        }

        //public void SetSelectedPage(Type pageType)
        //{
        //    if (pageType == typeof(SearchPage))
        //    {
        //        SelectedPage = ListPage[0];
        //    }
        //    else if (pageType == typeof(CheckupPage))
        //    {
        //        SelectedPage = ListPage[1];
        //    }
        //    else
        //    {
        //        SelectedPage = ListPage[2];
        //    }
        //}
        #endregion

        #region Property
        public ObservableCollection<Page> ListPage { get; set; }

        public Page SelectedPage
        {
            get { return _selectedPage; }
            set { _selectedPage = value; RaisePropertyChanged(); }
        }

        public ICommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(RefreshData));

        public ICommand GoSettingCommand => _goSettingCommand ??
                                            (_goSettingCommand =
                                                new RelayCommand(
                                                    () => { NavigationService.NavigateTo(typeof(SettingPage)); }));

        public ICommand GoPromotionCommand => _goPromotionCommand ??
                                              (_goPromotionCommand =
                                                  new RelayCommand(
                                                      () => { NavigationService.NavigateTo(typeof(PromotionPage)); }));

        #endregion
    }
}