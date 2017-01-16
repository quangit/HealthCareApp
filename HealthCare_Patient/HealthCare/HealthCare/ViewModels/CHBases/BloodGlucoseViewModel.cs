using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Models.ChBaseModel;
using HealthCare.Services.Interfaces;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HealthCare.Pages.CHBases;
using HealthCare.Helpers;
using HealthCare.Resx;
using HealthCare.WebServices.Interfaces;
using HealthCare.Validators.CHBasesValidator;

namespace HealthCare.ViewModels.CHBases
{
    public class BloodGlucoseViewModel : BaseViewModel<BloodGlucoseViewModel>
    {
        private ObservableCollection<BloodGlucoseModel> _bloodGlucoseModels;
        private int _itemCount = 0;
        private ICommand _gotoPageDetailCommand,_gotoPageAddCommand;
        private readonly IChBaseWS _chBaseWs;
        private BloodGlucoseModel _bloodGlucose;
        private readonly BloodGlucoseValidator _bloodGlocoseValidate;
        

        public BloodGlucoseViewModel(INavigationService navigationService, IChBaseWS chBaseWs,BloodGlucoseValidator validate) : base(navigationService)
        {
            _bloodGlocoseValidate = validate;
            _bloodGlucose = new BloodGlucoseModel();
            _bloodGlucose.Normalcy = "1";
            _chBaseWs = chBaseWs;
            GetBloodGlucoseList();
            //ObservableCollection<BloodGlucoseModel> listBloodGlucoseModel = new ObservableCollection<BloodGlucoseModel>();
            //listBloodGlucoseModel.Add(new BloodGlucoseModel
            //{
            //    Value = "23",
            //    MeasurementContext = "ádasdkashd",
            //    When = DateTime.Now.ToString()
            //});
            //listBloodGlucoseModel.Add(new BloodGlucoseModel
            //{
            //    Value = "32",
            //    MeasurementContext = "kxizcyiaw",
            //    When = DateTime.Now.ToString()
            //});
            //listBloodGlucoseModel.Add(new BloodGlucoseModel
            //{
            //    Value = "323",
            //    MeasurementContext = "xhfiwua",
            //    When = DateTime.Now.ToString()
            //});
            //BloodGlucoseModels = listBloodGlucoseModel;
            //ItemCount = 3;
        }

        public BloodGlucoseModel BloodGlucoseModel
        {
            get { return _bloodGlucose; }
            set { _bloodGlucose = value;
                RaisePropertyChanged(); }
        }

        public string Value
        {
            get
            {
                return _bloodGlucose.Value;
            }
            set
            {
                if(value!=null)
                {
                    var result = _bloodGlocoseValidate.ValidateNumber(value);
                    if (result.IsValid || string.IsNullOrEmpty(value))
                    {
                        _bloodGlucose.Value = value;
                        
                    }
                }
                RaisePropertyChanged();
            }
        }
        public string Type
        {
            get { return _bloodGlucose.Type; }
            set
            {
                _bloodGlucose.Type = value;
                RaisePropertyChanged();
            }
        }
        public string MeasurementContext
        {
            get { return _bloodGlucose.MeasurementContext; }
            set
            {
                _bloodGlucose.MeasurementContext = value;
                RaisePropertyChanged();
            }
        }

        public string Normalcy
        {
            get { return _bloodGlucose.Normalcy; }
            set
            {
                _bloodGlucose.Normalcy = value;
                RaisePropertyChanged();
            }
        }

        public bool IsControlTest
        {
            get { return _bloodGlucose.IsControlTest; }
            set
            {
                _bloodGlucose.IsControlTest = value;
                RaisePropertyChanged();
            }
        }
        public bool IsOutsideOperatingTemp
        {
            get { return _bloodGlucose.OutsideOperatingTemp; }
            set
            {
                _bloodGlucose.OutsideOperatingTemp = value;
                RaisePropertyChanged();
            }
        }



        public ObservableCollection<BloodGlucoseModel> BloodGlucoseModels
        {
            get { return _bloodGlucoseModels; }
            set { _bloodGlucoseModels = value;
                ItemCount = _bloodGlucoseModels.Count; RaisePropertyChanged(); }
        }

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value;
                RaisePropertyChanged(); }
        }

        public int SelectItemComparison
        {
            get { return (Convert.ToInt32(_bloodGlucose.Normalcy)-1); }
            set
            {
                _bloodGlucose.Normalcy = (value+1).ToString();
                RaisePropertyChanged();
            }
        }

        public async void GetBloodGlucoseList()
        {
            Common.ShowLoading();
            BloodGlucoseModels = await _chBaseWs.GetBloodGlucose();
            Common.HideLoading();
        }

        public ICommand GotoPageDetail
        {
            get
            {
                return _gotoPageDetailCommand ?? (_gotoPageDetailCommand = new RelayCommand<BloodGlucoseModel>(
                              item =>
                              {
                                  if (item != null)
                                  {
                                      ObservableCollection<CHBaseDetailUIModel> listBaseDetailUIModels = new ObservableCollection<CHBaseDetailUIModel>();
                                      listBaseDetailUIModels.Add(new CHBaseDetailUIModel
                                      {
                                          Title = AppResources.time_title,
                                          Value = item.When.ToString("d"),                                        
                                      });
                                      listBaseDetailUIModels.Add(new CHBaseDetailUIModel
                                      {
                                          Title = AppResources.glucose_value,
                                          Value = item.Value,

                                      });
                                      listBaseDetailUIModels.Add(new CHBaseDetailUIModel
                                      {
                                          Title = AppResources.glucose_context,
                                          Value = item.MeasurementContext,
                                      });

                                      var deleteAction = new Action(async () =>
                                      {
                                          if (await Common.ConfirmAsync(Resx.AppResources.confirm_del_blood_glucose))
                                          {
                                              Common.ShowLoading();
                                              if (await _chBaseWs.RemoveData(item.Id))
                                              {
                                                  // delete success 
                                                  BloodGlucoseModels = await _chBaseWs.GetBloodGlucose();
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
                                      CHBaseDetailViewModel.Instance.GoToDetailPage(AppResources.blood_glucose, listBaseDetailUIModels, deleteAction);
                                  }
                              }));
            }
        }

        public ICommand GotoPageAdd
        {
            get
            {
                return _gotoPageAddCommand ?? (_gotoPageAddCommand = new RelayCommand(() =>
                {
                    _bloodGlucose = new BloodGlucoseModel();
                    NavigationService.NavigateTo(typeof (AddBloodGlucosePage));
                }));
            }
        }

        public RelayCommand ResetCommand => new RelayCommand(() =>
        {
            Value = "";
            Type = "";
            MeasurementContext = "";
            IsControlTest = false;
            IsOutsideOperatingTemp = false;
        });

        public RelayCommand AddBloodGlucoseCommand => new RelayCommand(AddBloodGlucose);

        public async void AddBloodGlucose()
        {
            var result = _bloodGlocoseValidate.Validate(_bloodGlucose);
            if (result.IsValid)
            {
                Common.ShowLoading();
                _bloodGlucose.When = DateTime.Now;
                var isSuccess = await _chBaseWs.AddBloodGlucose(_bloodGlucose);
                if (isSuccess)
                {
                    BloodGlucoseModels = await _chBaseWs.GetBloodGlucose();
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

    }
}
