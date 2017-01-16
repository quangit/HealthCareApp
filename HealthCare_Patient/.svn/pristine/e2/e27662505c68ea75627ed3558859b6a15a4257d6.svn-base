using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.ViewModels
{
    public class DoctorScheduleViewModel : BaseViewModel<DoctorScheduleViewModel>
    {
        private readonly bool _canGoBack;
        private readonly IDoctorWS _doctorWs;
        private ICommand _getScheduleListCommand;
        private ObservableCollection<DateTime> _listScheduleDateTime;
        private ScheduleModel _selectedSchedule;

        public DoctorScheduleViewModel(INavigationService navigationService, IDoctorWS doctorWs)
            : base(navigationService)
        {
            _doctorWs = doctorWs;
            _canGoBack = true;
            ListSchedule = new ObservableCollection<ScheduleModel>();
            ListScheduleDateTime = new ObservableCollection<DateTime>();
            ListScheduleOfADate = new ObservableCollection<ScheduleModel>();
            ResetDateTimeParams();
        }

        #region Properties

        public ObservableCollection<ScheduleModel> ListSchedule { get; set; }

        public ObservableCollection<ScheduleModel> ListScheduleOfADate
        {
            get { return _listScheduleOfADate; }
            set
            {
                _listScheduleOfADate = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<DateTime> ListScheduleDateTime
        {
            get { return _listScheduleDateTime; }
            set
            {
                _listScheduleDateTime = value;
                RaisePropertyChanged();
            }
        }

        public ScheduleModel SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                _selectedSchedule = value;
                RaisePropertyChanged();
            }
        }

        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Methods

        public async Task<bool> LoadData(string doctorId, string hospitalId)
        {
            try
            {
                await GetScheduleOfDoctor(doctorId, hospitalId);
                return true;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            return false;
        }

        /// <summary>
        ///     Each time call, this
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="hospitalId"></param>
        /// <returns></returns>
        public async Task GetScheduleOfDoctor(string doctorId, string hospitalId)
        {
            var url = AppConstant.GetSchedulesOfDoctorFn;

            var listScheduleRaw = (await _doctorWs.GetScheduleOfDoctor(doctorId, Common.ConvertToTimestamp(DateTime.Now.Date),
                 Common.ConvertToTimestamp(DateTime.Now.Date.AddDays(15)), hospitalId))
                 .ToBaseModel<ScheduleModel, ScheduleApiModel>();

            foreach (var model in listScheduleRaw)
            {
                model.StartDateTime = model.StartDateTime.ToLocalTimeZone();
                model.EndDateTime = model.EndDateTime.ToLocalTimeZone();
            }

            ListSchedule = listScheduleRaw;

            _listScheduleDateTime =
                new ObservableCollection<DateTime>(ListSchedule.Select(x => x.StartDateTime.Date).Distinct());

            RaisePropertyChanged("ListScheduleDateTime");
        }

        public void ResetDateTimeParams()
        {
            SelectedDate = null;
            ListScheduleOfADate.Clear();
            ListScheduleDateTime.Clear();
        }

        /// <summary>
        ///     Handle hardware back button on WP and Android when ActionSheet is show
        /// </summary>
        private void HandleHardwareBackButton()
        {
            if (_canGoBack)
                NavigationService.GoBack();
        }

        #endregion

        #region Commands

        public ICommand GetScheduleListCommand
        {
            get
            {
                return _getScheduleListCommand ?? (_getScheduleListCommand = new RelayCommand(async () =>
                {
                    Common.ShowLoading();
                    try
                    {
                        await GetScheduleOfDoctor(DoctorViewModel.Instance.SelectedDoctor.Id,
                            HospitalViewModel.Instance.SelectedHospital.Id);
                    }
                    catch (Exception e)
                    {
                        await Common.AlertAsync(e.Message);
                    }
                    UserDialogs.Instance.HideLoading();
                }));
            }
        }


        private ICommand _selectedDateChangedCommand;
        private ObservableCollection<ScheduleModel> _listScheduleOfADate;
        private DateTime? _selectedDate;

        public ICommand SelectedDateChangedCommand
        {
            get
            {
                return _selectedDateChangedCommand ?? (_selectedDateChangedCommand = new RelayCommand<DateTime>(date =>
                {
                    _listScheduleOfADate = new ObservableCollection<ScheduleModel>(
                        ListSchedule.Where(x => x.StartDateTime.Date == date.Date).OrderBy(x => x.StartDateTime.Ticks));
                    AddCheckupViewModel.Instance.SelectedSchedule = _listScheduleOfADate.Count > 0
                        ? _listScheduleOfADate[0]
                        : null;
                    RaisePropertyChanged("ListScheduleOfADate");
                }));
            }
        }


        public ICommand CheckupFlowGoNextCommand { get; set; }

        #endregion
    }
}