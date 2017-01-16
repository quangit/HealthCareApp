using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models.ChBaseModel;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators.CHBasesValidator;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.ViewModels.CHBases
{
    public class HeightViewModel : BaseViewModel<HeightViewModel>
    {
        private readonly IChBaseWS _chBaseWs;
        private readonly HeightValidator _validator ;
        private HeightModel _height;

        public HeightViewModel(INavigationService navigationService, IChBaseWS chBaseWs,HeightValidator validator) : base(navigationService)
        {
            _validator = validator;
            _height = new HeightModel();
            _chBaseWs = chBaseWs;
            ClearViewModel();
            
        }

        

        #region Variables

        private ObservableCollection<HeightModel> _listHeight;

        #endregion
        
        #region Properties

        public ObservableCollection<HeightModel> ListHeight
        {
            get { return _listHeight; }
            set
            {
                _listHeight = value;
                RaisePropertyChanged();
            }
        }

        public string Value
        {
            get { return _height.Value; }
            set
            {
                if (value != null)
                {
                    var result = _validator.ValidateNumber(value);
                    if (result.IsValid || string.IsNullOrEmpty(value))
                    {
                        _height.Value = value;
                    }
                }
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand ResetCommand => new RelayCommand(() =>
        {
            Value = "";
        });

        public RelayCommand GetListHeightCommand => new RelayCommand(GetHeightList);


        public RelayCommand AddHeightCommand => new RelayCommand(AddHeight);

        public async void AddHeight()
        {
            var result = _validator.Validate(_height);
            if (result.IsValid)
            {
                Common.ShowLoading();
              var isSuccess = await _chBaseWs.AddHeight(double.Parse(_height.Value, ChBaseHelper.EnCultureInfo));
                if (isSuccess)
                {
                    ListHeight = await _chBaseWs.GetHeights();
                    NavigationService.GoBack();
                    Common.HideLoading();
                }
                else
                {
                    Common.HideLoading();
                    // Something wrong;
                }
            }
            else
            {
                await Common.AlertAsync(result.Errors[0]);              
            }
        }


        public RelayCommand<HeightModel> GoToDetailPageCommand => new RelayCommand<HeightModel>(m =>
        {
            if (m != null)
            {
                var detailList = new ObservableCollection<CHBaseDetailUIModel>
                {
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.time_title,
                        Value = m.When.ToString("d"),
                        Type = CHBaseDetailTypeEnum.OneLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.height_title,
                        Value = m.Value,
                        Type = CHBaseDetailTypeEnum.OneLine
                    }
                };

                var deleteAction = new Action(async () =>
                {
                    if (await Common.ConfirmAsync(Resx.AppResources.confirm_del_height))
                    {
                        Common.ShowLoading();
                        if (await _chBaseWs.RemoveData(m.Id))
                        {
                            // delete success 
                            ListHeight = await _chBaseWs.GetHeights();
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
                CHBaseDetailViewModel.Instance.GoToDetailPage(AppResources.height, detailList, deleteAction);
                
            }
        });


        public RelayCommand GoToAddNewCommand
            => new RelayCommand(() =>
            {
                _height = new HeightModel();
                NavigationService.NavigateTo(typeof(AddHeightPage));
            });

        

        

        #endregion

        #region Methods

        private void ClearViewModel()
        {
            ListHeight = new ObservableCollection<HeightModel>();
        }
        
        public async void GetHeightList()
        {
            Common.ShowLoading();
            ListHeight = await _chBaseWs.GetHeights();
            Common.HideLoading();
        }

        #endregion
    }
}
