using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
using HealthCare.Core;
#endif
using HealthCare.Core.Models;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Resources;

namespace HealthCare.Core.ViewModels
{
    public class ScheduleAddingViewModel : MyMvxViewModel
    {
        private IMessageService _messageService;
        public List<int> Weeks
        {
            get
            {
                return new List<int>()
                {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8,
                };
            }
        }

        private DateTime _currentDate;
        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set { SetProperty(ref _currentDate, value); }
        }
        //private List<Hospital> _hospitals;
        private MvxCommand _refreshCommand;

        public MvxCommand RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new MvxCommand(Refresh)); }
        }

        private async void Refresh()
        {
            try
            {
                if (Data.User.Profile == null)
                {
                    Data.User.Profile = await HealthCareService.Current.GetProfile();
                }
                else
                {
                    var r = await HealthCareService.Current.GetDoctorHospitals();
                    if (r != null && r.Any())
                    {
                        List<string> ids = new List<string>();
                        if (Data.User.Profile.DoctorInfos != null)
                        {
                            ids =
                                Data.User.Profile.DoctorInfos.Select(
                                    x => x.HospitalId + "-" + x.Roles.Select(c => c.ToString()).Aggregate((a, b) => a + "-" + b)).ToList();
                        }
                        Data.User.Profile.DoctorInfos = new List<DoctorInfo>();
                        foreach (var hospital in r)
                        {
                            if (Data.User.Profile == null)
                            {
                                Data.User.Profile = await HealthCareService.Current.GetProfile();
                            }
                            //if (Data.User.Profile.DoctorInfos == null)

                            Data.User.Profile.DoctorInfos.Add(new DoctorInfo()
                            {
                                HospitalId = (hospital.Id ?? hospital.HospitalId),
                                Hospital = hospital,
                                Roles = hospital.Roles
                            });
                        }
                        if (Data.User.Profile.DoctorInfos != null)
                        {
                            var changed = ids.OrderBy(x => x)
                                .Aggregate((x, y) => x + y)
                                .Equals(Data.User.Profile.DoctorInfos.Select(x => x.HospitalId+ "-" + x.Roles.Select(c => c.ToString()).Aggregate((a, b) => a + "-" + b))
                                    .OrderBy(x => x)
                                    .Aggregate((x, y) => x + y));
                            if (!changed)
                            {
                                try
                                {
                                    var data = await HealthCareService.Current.GetShedules();
                                    if (data != null && Data.User != null)
                                    {
                                        HomeViewModel.SetSchedules(data);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                RaisePropertyChanged("Hospitals");
                Hospital = Hospitals.FirstOrDefault();
            }
        }
        public List<Hospital> Hospitals
        {
            get
            {
                if (Data.User.Profile == null)
                    return new List<Hospital>();
                else
                    return Data.User.Profile.Hospitals;
            }
        }

        private Hospital _hospital;

        public Hospital Hospital
        {
            get { return _hospital; }
            set { SetProperty(ref _hospital, value); }
        }


        private DateTime? _startTime;

        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                SetProperty(ref _startTime, value);
                if (_startTime != null)
                    if (_endTime == null || _endTime < _startTime)
                    {
                        EndTime = _startTime.Value.AddHours(1);
                    }
            }
        }

        public List<DoctorDayOfWeekObject> DaySource
        {
            get { return _daySource; }
            set { SetProperty(ref _daySource, value); }
        }

        private DateTime? _endTime;

        public DateTime? EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }


        private MvxCommand _saveCommand;

        public MvxCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new MvxCommand(Save)); }
        }


        private int _quality;
        public int Quality
        {
            get { return _quality; }
            set { SetProperty(ref _quality, value); }
        }

        public DoctorDayOfWeek[] DayOfWeeks { get; set; }

        private async void Save()
        {

            bool r = false;
            if (StartTime == null || EndTime == null)
                return;
            if (StartTime.Value.TimeOfDay.TotalMinutes > EndTime.Value.TimeOfDay.TotalMinutes)
            {
                await _messageService.ShowMessageAsync(AppResources.FailureOverlapTime,
                    AppResources.Warning);
                return;
            }
            if (DayOfWeeks != null && DayOfWeeks.Any())
                r = await HealthCareService.Current.SetSchedule(new SetScheduleObject(Hospital,
                        CurrentDate.Date.Year,
                         CurrentDate.Date.Month,
                         CurrentDate.Date.Day,
                         StartTime.Value.Hour,
                         StartTime.Value.Minute,
                         EndTime.Value.Hour,
                         EndTime.Value.Minute,
                         Quality,
                         WeekCount, DayOfWeeks));
            else
            {
                r = await
                         HealthCareService.Current.SetSchedule(new SetScheduleObject(Hospital,
                         CurrentDate.Date.Year,
                         CurrentDate.Date.Month,
                         CurrentDate.Date.Day,
                         StartTime.Value.Hour,
                         StartTime.Value.Minute,
                         EndTime.Value.Hour,
                         EndTime.Value.Minute,
                             Quality));
            }

            //Close(this);

            if (r)
            {
				await homeVM.GetSchedules ();
				Close(this);
//                var schedulesData = await HealthCareService.Current.GetShedules();
//                if (schedulesData != null)
//                    HomeViewModel.SetSchedules(schedulesData);
            }

        }


        private List<BoolDoctorDayofWeek> _booldoctordayofweeks;
        public List<BoolDoctorDayofWeek> BoolDoctorDayofWeeks
        {
            get { return _booldoctordayofweeks; }
            set { SetProperty(ref _booldoctordayofweeks, value); }

        }

        public ListBoolDoctorDayofWeek ListBoolDoctorDayofWeek { get; set; }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }


        private int _weekCount;

        public ScheduleAddingViewModel(IMessageService messageService)
        {
            _messageService = messageService;
        }
#if !MVVMCROSS
        public ScheduleAddingViewModel():this(IoC.Resolve<IMessageService>())
        {
            
        }
#endif
        public int WeekCount
        {
            get { return _weekCount; }
            set { SetProperty(ref _weekCount, value); }
        }

		private HomeViewModel homeVM;
        private List<DoctorDayOfWeekObject> _daySource;

        public override void Init()
        {
            // Initalize for test purpose
            Quality = 1;
            WeekCount = 1;
			homeVM = GetParam<HomeViewModel>();
			CurrentDate = homeVM.SelectedDate;
            if (CurrentDate.Year <= 1)
                CurrentDate = DateTime.Now;


            StartTime = new DateTime(1997, 1, 1, 7, 0, 0);
            DaySource =
                new List<DoctorDayOfWeekObject>(new[]
                {
                    DoctorDayOfWeek.Sunday,
                    DoctorDayOfWeek.Monday,
                    DoctorDayOfWeek.Tuesday, DoctorDayOfWeek.Wednesday, DoctorDayOfWeek.Thursday,
                    DoctorDayOfWeek.Friday, DoctorDayOfWeek.Saturday
                }.Select(x => new DoctorDayOfWeekObject() { Value = x }));

            ////Android Binding Spinner
            BoolDoctorDayofWeeks = new List<BoolDoctorDayofWeek>();
            foreach (var doctorDayOfWeek in DaySource)
            {
                var bDoctor = new BoolDoctorDayofWeek();
                bDoctor.DoctorDayOfWeek = doctorDayOfWeek.Value;
                bDoctor.isSelected = false;
                BoolDoctorDayofWeeks.Add(bDoctor);
            }

            Refresh();

        }

    }


    public class BoolDoctorDayofWeek : MyNotifyPropertyChanged
    {
        private bool _isSelected;

        public bool isSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                //RaisePropertyChanged(()=> isSelected);
                RaisePropertyChanged("SelectDayString");
            }
        }
        public DoctorDayOfWeek DoctorDayOfWeek { get; set; }


        public string SelectDayString
        {
            get
            {
                string name = "";
                if (isSelected)
                {
                    name += DoctorDayOfWeek + " ";
                }
                return name;
            }
        }

    }

    public class ListBoolDoctorDayofWeek : MyNotifyPropertyChanged
    {
        public string Name { get; set; }

        private List<BoolDoctorDayofWeek> _booldoctordayofweeks;
        public List<BoolDoctorDayofWeek> BoolDoctorDayofWeeks
        {
            get { return _booldoctordayofweeks; }
            set { SetProperty(ref _booldoctordayofweeks, value); }
        }

        public ListBoolDoctorDayofWeek()
        {
            var DaySource =
                  new List<DoctorDayOfWeek>(new[]
                {
                    DoctorDayOfWeek.Monday,
                    DoctorDayOfWeek.Tuesday, DoctorDayOfWeek.Wednesday, DoctorDayOfWeek.Thursday,
                    DoctorDayOfWeek.Friday, DoctorDayOfWeek.Saturday, DoctorDayOfWeek.Sunday
                });

            BoolDoctorDayofWeeks = new List<BoolDoctorDayofWeek>();
            foreach (var doctorDayOfWeek in DaySource)
            {
                var bDoctor = new BoolDoctorDayofWeek();
                bDoctor.DoctorDayOfWeek = doctorDayOfWeek;
                bDoctor.isSelected = false;
                BoolDoctorDayofWeeks.Add(bDoctor);
            }
        }
    }

}
