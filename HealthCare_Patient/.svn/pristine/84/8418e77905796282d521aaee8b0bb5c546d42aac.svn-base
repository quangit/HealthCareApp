using System;
using System.Collections.ObjectModel;
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
    public class WeightViewModel : BaseViewModel<WeightViewModel>
    {

        #region Variables

        private ObservableCollection<WeightModel> _listWeight;
        private readonly IChBaseWS _chBaseWs;
        private readonly WeightValidator _validator;
        private WeightModel _weight;

        #endregion
        public WeightViewModel(INavigationService navigationService, IChBaseWS chBaseWs,WeightValidator validator) : base(navigationService)
        {
            _weight = new WeightModel();
            _validator = validator;
            _chBaseWs = chBaseWs;
            ClearViewModel();
        }

        #region Properties

        public ObservableCollection<WeightModel> ListWeight
        {
            get { return _listWeight; }
            set
            {
                _listWeight = value;
                RaisePropertyChanged();
            }
        }

        public string Value
        {
            get { return _weight.Value; }
            set
            {
                if (value != null)
                {
                    var result = _validator.ValidateNumber(value);
                    if (result.IsValid || string.IsNullOrEmpty(value))
                    {
                        _weight.Value = value;
                    }
                }
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand GetListWeightCommand => new RelayCommand(GetWeightList);

        public RelayCommand AddWeightCommand => new RelayCommand(AddWeight);

        public async void AddWeight()
        {
            var result = _validator.Validate(_weight);
            if (result.IsValid)
            {
                Common.ShowLoading();
                var isSuccess = await _chBaseWs.AddWeight(double.Parse(_weight.Value, ChBaseHelper.EnCultureInfo));
                if (isSuccess)
                {
                    ListWeight = await _chBaseWs.GetWeights();
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

        public RelayCommand ResetCommand => new RelayCommand(() =>
        {
            Value = "";
        });


        public RelayCommand<WeightModel> GoToDetailPageCommand => new RelayCommand<WeightModel>(m =>
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
                        Title = AppResources.weight_title,
                        Value = m.Value,
                        Type = CHBaseDetailTypeEnum.OneLine
                    }
                };

                var deleteAction = new Action(async () =>
                {
                    if (await Common.ConfirmAsync(Resx.AppResources.confirm_del_weight))
                    {
                        Common.ShowLoading();
                        if (await _chBaseWs.RemoveData(m.Id))
                        {
                            // delete success 
                            ListWeight = await _chBaseWs.GetWeights();
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
                CHBaseDetailViewModel.Instance.GoToDetailPage(AppResources.weight, detailList, deleteAction);
            }
        });


        public RelayCommand GoToAddNewCommand
            => new RelayCommand(() =>
            {
                _weight = new WeightModel();
                NavigationService.NavigateTo(typeof(AddWeightPage));
            });

        #endregion

        #region Methods

        private void ClearViewModel()
        {
            ListWeight = new ObservableCollection<WeightModel>();
        }

        public async void GetWeightList()
        {
            Common.ShowLoading();
            ////Mock data
            //ListWeight.Add(new WeightModel {When = "30/10/1999", Value = "50kg"});
            //ListWeight.Add(new WeightModel {When = "02/12/2000", Value = "80kg"});
            //ListWeight.Add(new WeightModel {When = "03/10/2010", Value = "40kg"});
            //ListWeight.Add(new WeightModel {When = "01/11/2015", Value = "100kg"});
            ListWeight = await _chBaseWs.GetWeights();

            Common.HideLoading();
        }

        #endregion
    }
}