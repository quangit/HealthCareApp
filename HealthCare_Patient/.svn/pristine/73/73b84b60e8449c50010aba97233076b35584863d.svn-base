using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.ViewModels.CHBases
{
    public class CHBaseDetailViewModel : BaseViewModel<CHBaseDetailViewModel>
    {
        #region Variables

        private ObservableCollection<CHBaseDetailUIModel> _listItemDetail;
        private string _title;
        private bool _isProcedure;
        private ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();

        private readonly IChBaseWS _chBaseWs;
        #endregion

        public CHBaseDetailViewModel(INavigationService navigationService, IChBaseWS chBaseWs) : base(navigationService)
        {
            ClearViewModel();
            _chBaseWs = chBaseWs;
        }

        #region Properties
        public ObservableCollection<GalleryImage> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<CHBaseDetailUIModel> ListItemDetail
        {
            get { return _listItemDetail; }
            set
            {
                _listItemDetail = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged(); }
        }

        public bool IsProcedure
        {
            get { return _isProcedure; }
            set { _isProcedure = value; RaisePropertyChanged(); }
        }
        #endregion

        #region Methods

        private void ClearViewModel()
        {
            ListItemDetail = new ObservableCollection<CHBaseDetailUIModel>();
        }
        public RelayCommand DeleteCommand => new RelayCommand(_deleteAction);

        private Action _deleteAction;

        public void GoToDetailPage(string title, IList<CHBaseDetailUIModel> listItemDetail, Action delete = null, bool isProcedure = false)
        {
            Title = Common.GetDeviceLanguage() == "vi" ? AppResources.detail + " " + title : title + " " + AppResources.detail;
            IsProcedure = isProcedure;
            if (IsProcedure)
            {
                RaisePropertyChanged("IsProcedure");
            }
            else
            {
                Images?.Clear();
            }
            //ListItemDetail.Clear();
            //foreach (var model in listItemDetail)
            //{
            //    ListItemDetail.Add(model);
            //}
            _deleteAction = delete;
            ListItemDetail = new ObservableCollection<CHBaseDetailUIModel>(listItemDetail);
            if (Images?.Count == 0)
            {
                IsProcedure = false;
            }
            NavigationService.NavigateTo(typeof(CHBaseDetailPage));
          
        }

        #endregion
    }
}