using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HealthCare.Models.ChBaseModel;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Helpers;
using HealthCare.WebServices.Interfaces;
using HealthCare.Validators.CHBasesValidator;

namespace HealthCare.ViewModels.CHBases
{
    public class BloodPressureViewModel : BaseViewModel<BloodPressureViewModel>
    {
        private ObservableCollection<BloodPressureModel> _bloodPressureModels;
        private int _itemCount = 0;
        private BloodPressureModel _bloodPressure;
        private ICommand _gotoPageDetail, _gotoPageAddCommand, _addBloodPressureCommand;
        private readonly IChBaseWS _chBaseWs;
        private readonly BloodPressureValidator _validate;

        public BloodPressureViewModel(INavigationService navigationService, IChBaseWS chBaseWs, BloodPressureValidator validate) : base(navigationService)
        {
            _validate = validate;
            _bloodPressure = new BloodPressureModel();
            _chBaseWs = chBaseWs;
            GetBloodPressureList();
            //ObservableCollection<BloodPressureModel> listBloodPressureModels = new ObservableCollection<BloodPressureModel>();
            //listBloodPressureModels.Add(new BloodPressureModel
            //{
            //    Systolic = "123",
            //    Diastolic = "23",
            //    Pulse = "22",
            //    When = DateTime.Now.ToString()
            //});
            //listBloodPressureModels.Add(new BloodPressureModel
            //{
            //    Systolic = "231",
            //    Diastolic = "32",
            //    Pulse = "14",
            //    When = DateTime.Now.ToString()
            //});
            //listBloodPressureModels.Add(new BloodPressureModel
            //{
            //    Systolic = "213",
            //    Diastolic = "42",
            //    Pulse = "22",
            //    When = DateTime.Now.ToString()
            //});
            //ItemCount = 3;
            //BloodPressureModels = listBloodPressureModels;
        }

        public ObservableCollection<BloodPressureModel> BloodPressureModels
        {
            get { return _bloodPressureModels; }
            set
            {
                _bloodPressureModels = value;
                ItemCount = _bloodPressureModels.Count;
                RaisePropertyChanged();
            }
        }

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; RaisePropertyChanged(); }
        }

        public string Systolic
        {
            get { return _bloodPressure.Systolic; }
            set
            {
                if (value != null)
                {
                    var result = _validate.ValidateNumber(value);
                    if (result.IsValid || string.IsNullOrEmpty(value))
                    {
                        _bloodPressure.Systolic = value;
                    }

                }
                RaisePropertyChanged();
            }
        }

        public string Diastolic
        {
            get { return _bloodPressure.Diastolic; }
            set
            {
                if (value != null)
                {
                    var result = _validate.ValidateNumber(value);
                    if (result.IsValid || string.IsNullOrEmpty(value))
                    {
                        _bloodPressure.Diastolic = value;
                    }

                }
                RaisePropertyChanged();
            }
        }

        public string Pulse
        {
            get { return _bloodPressure.Pulse; }
            set
            {
                if (value != null)
                {
                    var result = _validate.ValidateNumber(value);
                    if (result.IsValid || string.IsNullOrEmpty(value))
                    {
                        _bloodPressure.Pulse = value;
                    }

                }
                RaisePropertyChanged();
            }
        }

        public bool IsIrregularHeartbeat
        {
            get { return _bloodPressure.IrregularHeartbeat; }
            set
            {
                _bloodPressure.IrregularHeartbeat = value;
                RaisePropertyChanged();
            }
        }


        public async void GetBloodPressureList()
        {
            Common.ShowLoading();
            BloodPressureModels = await _chBaseWs.GetBloodPressure();
            Common.HideLoading();
        }



        public ICommand GotoPageDetail
        {
            get
            {
                return _gotoPageDetail ?? (_gotoPageDetail = new RelayCommand<BloodPressureModel>(
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
                                          Title = AppResources.systolic,
                                          Value = item.Systolic.ToString(),

                                      });
                                      listBaseDetailUIModels.Add(new CHBaseDetailUIModel
                                      {
                                          Title = AppResources.diastolic,
                                          Value = item.Diastolic.ToString(),
                                      });
                                      listBaseDetailUIModels.Add(new CHBaseDetailUIModel
                                      {
                                          Title = AppResources.pulse,
                                          Value = item.Pulse.ToString(),
                                      });

                                      var deleteAction = new Action(async () =>
                                      {
                                          if (await Common.ConfirmAsync(Resx.AppResources.confirm_del_blood_pressure))
                                          {
                                              Common.ShowLoading();
                                              if (await _chBaseWs.RemoveData(item.Id))
                                              {
                                                  // delete success 
                                                  BloodPressureModels = await _chBaseWs.GetBloodPressure();
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
                                      CHBaseDetailViewModel.Instance.GoToDetailPage(AppResources.blood_pressure, listBaseDetailUIModels, deleteAction);

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
                    _bloodPressure = new BloodPressureModel();
                    NavigationService.NavigateTo(typeof(AddBloodPressurePage));
                }));
            }
        }

        public RelayCommand ResetCommand => new RelayCommand(() =>
        {
            Systolic = "";
            Diastolic = "";
            Pulse = "";
            IsIrregularHeartbeat = false;
        });

        public RelayCommand AddBloodPressureCommand => new RelayCommand(AddBloodPressure);

        public async void AddBloodPressure()
        {
            var result = _validate.Validate(_bloodPressure);
            if (result.IsValid)
            {
                Common.ShowLoading();
                _bloodPressure.When = DateTime.Now;
                var isSuccess = await _chBaseWs.AddBloodPressure(_bloodPressure);
                if (isSuccess)
                {
                    BloodPressureModels = await _chBaseWs.GetBloodPressure();
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
