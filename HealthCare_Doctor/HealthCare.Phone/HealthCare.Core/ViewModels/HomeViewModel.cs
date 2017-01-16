using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using HealthCare.Core.Models;
using HealthCare.Core.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Utils;
using HealthCare.Core.Resources;

#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
#endif

namespace HealthCare.Core.ViewModels
{
    public class HomeViewModel
        : MyMvxViewModel
    {
        private IFileService _fileService;
        private IMessageService _messageService;
        //private IBandService _bandService;
        private readonly ICmeService _cmeService;
        private bool _loading = true;

        private Dictionary<string, bool> loadingLocks = new Dictionary<string, bool>();

        
        public HomeViewModel(IFileService fileService, IMessageService messageService, ICmeService cmeService)
        {
            _fileService = fileService;
            _messageService = messageService;
            _cmeService = cmeService;
            //_bandService = bandService;
#if MVVMCROSS
            DeleteScheduleCommand = new Cirrious.MvvmCross.ViewModels.MvxCommand<Schedule>(DeleteSchedule);
#else
            DeleteScheduleCommand = new Template10.Mvvm.MvxCommand<Schedule>(DeleteSchedule);
#endif
            CurrentCheckupPage = 0;
        }

#if !MVVMCROSS
        public HomeViewModel() : this(IoC.Resolve<IFileService>(), IoC.Resolve<IMessageService>(), IoC.Resolve<ICmeService>(), IoC.Resolve<IBandService>())
        {

        }
#endif
        //public Doctor Doctor { get { return Data.User.Profile; } }



        private Doctor _doctor;

        public Doctor Doctor
        {
            get { return _doctor; }
            set { SetProperty(ref _doctor, value); }
        }


        private Setting _settings;
        public Setting Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                RaisePropertyChanged("Settings");
            }
        }

        //private BandSetting _bandSetting;
        //public BandSetting BandSetting
        //{
        //    get { return _bandSetting; }
        //    set
        //    {
        //        _bandSetting = value;
        //        RaisePropertyChanged(() => BandSetting);
        //    }
        //}



        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        public void ShowCmeSearch()
        {
            ShowViewModel<CmeSearchViewModel>();
        }
        private ICommand _cmeCommand;
        public ICommand CmeCommand
        {
            get
            {
#if MVVMCROSS
                _cmeCommand = _cmeCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand<string>(ShowCmeCategory);
#else
                _cmeCommand = _cmeCommand ?? new Template10.Mvvm.MvxCommand<string>(ShowCmeCategory);
#endif
                return _cmeCommand;
            }
        }

      
        private async void ShowCmeCategory(string cat)
        {
            
            try
            {
                var resp = await _cmeService.GetClasses(cat);
                if (resp.CmeClasses != null)
                {
                    ShowViewModel<CmeCategoryViewModel>(resp);
                }
                else
                {
                    await _messageService.ShowMessageAsync(AppResources.Cme_ClassEmpty, AppResources.Warning);
                }
            }
            finally
            {
              
            }
        }



        private List<string> _cmeCategories;
        public List<string> CmeCategories
        {
            get { return _cmeCategories; }
            set { SetProperty(ref _cmeCategories, value); }
        }

        private List<string> _allCmeCategories;
        public List<string> AllCmeCategories
        {
            get { return _allCmeCategories; }
            set { SetProperty(ref _allCmeCategories, value); }
        }

        private string _cmeCategoriesSearch = "";
        public string CmeCategoriesSearch
        {
            get { return _cmeCategoriesSearch; }
            set
            {
                value = value.Trim();
                if (AllCmeCategories != null)
                {
                    if (string.IsNullOrWhiteSpace(value))
                        CmeCategories = AllCmeCategories;
                    else
                        CmeCategories = AllCmeCategories.Where((string str) =>
                           (str.ToLowerInvariant()).Contains(value.ToLowerInvariant())).ToList();
                }
                SetProperty(ref _cmeCategoriesSearch, value);
            }
        }

        public async Task<bool> LoadCmeCategories(bool refresh = false)
        {
            try
            {
                AllCmeCategories = await _cmeService.GetCategories();
                CmeCategoriesSearch = _cmeCategoriesSearch;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public string CHBASE_URL;

        private bool _inited;
        public override async void Init()
        {
            if (_inited)
                return;
            _inited = true;
           // Data.BandSetting = new BandSetting();
            Settings = Data.Setting;
           // BandSetting = Data.BandSetting;

            SettingConsent();

            var vn = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("vi");
            CHBASE_URL = "http://chbase.bacsi24x7.vn/" + (vn ? "?lang=vi" : "");


            if (Data.User == null)
            {
                var r = await HealthCareService.Current.LoginAsync(Data.UserName, Data.Password);
                if (r == null)
                {
                    this.Close(this);
                }
            }


            if (Data.User != null)
            {
                Doctor = Data.User?.Profile;
                Data.User.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName.Equals("Profile"))
                        Doctor = Data.User.Profile;
                    if (e.PropertyName.Equals("SchedulesAdded"))
                        Schedules = Data.User.Schedules;
                };
            }
            Loading = true;
            await Task.WhenAny(GetSchedules(), LoadTopics(), LoadSupportRequests(), LoadCheckups(), LoadCmeCategories());
            Loading = false;


           

            base.Init();

#if !MVVMCROSS
            try
            {
                this.BandSetting.DoConnectDefault();
            }
            catch (Exception)
            {

            }
#endif
        }

        private async void SettingConsent()
        {
            Data.Setting = await _fileService.Load<Setting>("settings.dat");
            if (Data.Setting == null)
            {
                var r = await
                    _messageService.ShowConfirmMessageAsync(AppResources.Setting_NotificationInfo,
                        AppResources.Setting_Noti,
                        AppResources.Messsage_Yes, AppResources.Messsage_No);

                Data.Setting = new Setting() { PushConsent = r };
            }

            await _fileService.SaveSetting();
            Data.Setting.PropertyChanged += async (s, e) =>
            {
                await _fileService.SaveSetting();
                if (e.PropertyName == "PushConsent")
                {
                    await HealthCareService.Current.NotificationRegister();
                }
            };
            Settings = Data.Setting;

            await HealthCareService.Current.NotificationRegister();
        }


        private LoadMoreData<Checkup> _checkupsData;
        public async Task<bool> LoadCheckups(bool refresh = false)
        {
            try
            {
                Func<int, Task<LoadMoreData<Checkup>>> getDataTask = async (start) =>
                {
                    return await HealthCareService.Current.GetCheckups("", start);
                };

                var resp = await LoadMore<Checkup>("CheckupLoading", _checkupsData, getDataTask, refresh);
                if (resp != null)
                {
                    _checkupsData = resp;
                    if (Checkups == null)
                        Checkups = new ObservableCollection<Checkup>();
                    var changed = false;
                    if (refresh)
                    {
                        Checkups = new ObservableCollection<Checkup>(_checkupsData.Data);
                        changed = true;
                    }
                    else
                    {
                        foreach (var request in _checkupsData.Data)
                        {
                            if (!Checkups.Any(x => x.Id.Equals(request.Id)))
                            {
                                Checkups.Add(request);
                                changed = true;
                            }

                        }

                        RaisePropertyChanged(() => Checkups);
                    }
                    return changed;
                }

            }
            catch (Exception)
            {
                //throw;
            }
            return false;
        }

        //        public async Task LoadSupportRequests()
        //        {
        //            var requestData = await HealthCareService.Current.GetRequests();
        //            if (requestData != null)
        //            {
        //                ConsultRequests = Data.User.ConsultRequests = new ObservableCollection<ConsultRequest>(requestData);
        //            }
        //        }
        private LoadMoreData<ConsultRequest> _requestsData;
        public async Task<bool> LoadSupportRequests(bool refresh = false)
        {
            Func<int, Task<LoadMoreData<ConsultRequest>>> getDataTask = async (start) =>
            {
                return await HealthCareService.Current.GetRequests(start);
            };

            var resp = await LoadMore<ConsultRequest>("RequestLoading", _requestsData, getDataTask, refresh);
            if (resp != null)
            {
                _requestsData = resp;
                if (ConsultRequests == null)
                    ConsultRequests = new ObservableCollection<ConsultRequest>();
                var changed = false;
                if (refresh)
                {
                    ConsultRequests = new ObservableCollection<ConsultRequest>(_requestsData.Data);
                    changed = true;
                }
                else
                {
                    foreach (var request in _requestsData.Data)
                    {
                        if (!ConsultRequests.Any(x => x.Id.Equals(request.Id)))
                        {
                            ConsultRequests.Add(request);
                            changed = true;
                        }

                    }
#if MVVMCROSS
                    RaisePropertyChanged(() => ConsultRequests);
#endif
                }
                Data.User.ConsultRequests = ConsultRequests;
                return changed;
            }

            return false;
        }

        public void Replace<T>(IList<T> collection, T item, Func<T, bool> query)
        {
            var oldItem = collection.FirstOrDefault(query);
            var index = collection.IndexOf(oldItem);
            collection[index] = item;
        }

        private async Task<LoadMoreData<T>> LoadMore<T>(string lockname, LoadMoreData<T> data, Func<int, Task<LoadMoreData<T>>> getDataTask, bool refresh = false)
        {
            if (!loadingLocks.ContainsKey(lockname))
                loadingLocks.Add(lockname, false);
            if (!loadingLocks[lockname])
            {
                loadingLocks[lockname] = true;

                int start = 0;
                if (!refresh)
                {
                    if ((data != null))
                    {
                        if ((data.Count() >= data.Total) || data.Total <= Data.LoadLength)
                        { // no more to load
                            loadingLocks[lockname] = false;
                            return null;
                        }
                        else
                            start = data.Count();
                    }
                }

                var respData = await getDataTask(start);

                if (respData != null)
                {

                    if (respData.Data == null || respData.Data.Count == 0)
                    { // if Data empty => no more item
                        if (data != null)
                            data.Total = data.Count();
                    }
                    else
                    {
                        if ((start == 0))
                        { // first load
                            data = respData;
                        }
                        else
                        { // concat to end of list
                            data.Total = respData.Total;
                            data.Data = data.Data.Concat(respData.Data).ToList();
                        }
                    }

                    loadingLocks[lockname] = false;
                    return data;
                }


                loadingLocks[lockname] = false;
            }
            return null;
        }

        private LoadMoreData<Topic> _topicData;
        public async Task<bool> LoadTopics(bool refresh = false)
        {

            Func<int, Task<LoadMoreData<Topic>>> getDataTask = async (start) => await HealthCareService.Current.GetTopic(start);

            try
            {
                var resp = await LoadMore<Topic>("TopicLoading", _topicData, getDataTask, refresh);
                if (resp != null)
                {
                    _topicData = resp;

                    if (AllWeekTopics == null)
                        AllWeekTopics = new ObservableCollection<Topic>();
                    var changed = false;
                    if (refresh)
                    {
                        AllWeekTopics = new ObservableCollection<Topic>(_topicData.Data);
                        changed = true;
                    }
                    else
                    {
                        foreach (var request in _topicData.Data)
                        {
                            if (!AllWeekTopics.Any(x => x.Id.Equals(request.Id)))
                            {
                                AllWeekTopics.Add(request);
                                changed = true;
                            }

                        }
                    }
                    FilterWeekTopic();
                    return changed;


                }
            }
            catch (Exception)
            {
                //throw;
            }
            return false;
        }



        #region Calendar View
        public ICommand DeleteScheduleCommand { get; set; }
        private async void DeleteSchedule(Schedule obj)
        {
            if (obj != null)
            {
                if (obj.DayOfWeeks != null && obj.DayOfWeeks.Count > 0)
                {

                    var r = Data.User.SchedulesRaw.First(x => x.Id == obj.Id);
                    if (r.ExcludedDates == null)
                        r.ExcludedDates = new List<object>();
                    r.ExcludedDates.Add(obj.Date);
                    var a = await HealthCareService.Current.SetSchedule(r);
                    if (a)
                        await GetSchedules();
                    return;
                }
                var t = await HealthCareService.Current.DeleteSchedule(obj.Id);
                if (t)
                    await GetSchedules();
            }
        }

        public async Task GetSchedules()
        {
            try
            {
                var data = await HealthCareService.Current.GetShedules();
                if (data != null && Data.User != null)
                {
                    Schedules = SetSchedules(data);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static ObservableCollection<Schedule> SetSchedules(List<Schedule> schedulesData)
        {
            if (Data.User.Schedules == null)
                Data.User.Schedules = new ObservableCollection<Schedule>();
            Data.User.SchedulesRaw = schedulesData;
            Data.User.Schedules.Clear();
            foreach (var schedule in schedulesData)
            {
                if (schedule.DayOfWeeks == null)
                {
                    if (!schedule.HospitalId.Equals(schedule.Hospital.Name))
                        Data.User.Schedules.Add(schedule);
                }
                else
                {
                    foreach (var dayOfWeek in schedule.DayOfWeeks)
                    {

                        var dateUTC = Util.GetNextWeekday(schedule.Date == 0 ? DateTime.Now : schedule.StartDateTime.Date, (int)dayOfWeek - 1);
                        var startTimeUTC = new DateTime(dateUTC.Year, dateUTC.Month, dateUTC.Day, schedule.StartHour, schedule.StartMinute, 0, DateTimeKind.Utc);
                        var endTimeUTC = new DateTime(dateUTC.Year, dateUTC.Month, dateUTC.Day, schedule.EndHour, schedule.EndMinute, 0, DateTimeKind.Utc);
                        if (endTimeUTC.TimeOfDay.TotalSeconds < startTimeUTC.TimeOfDay.TotalSeconds)
                        {
                            endTimeUTC = endTimeUTC.AddDays(1);
                        }

                        int i = 0;
                        do
                        {
                            var index = (i++) * 7;
                            var startDateTimeUTC = dateUTC.AddDays(index);
                            var endDateTimeUTC = endTimeUTC.AddDays(index);
                            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            var item = schedule.Clone();
                            item.Date = (long)(startDateTimeUTC.Subtract(unixEpoch)).TotalMilliseconds;
                            item.EndDate = (long)(endDateTimeUTC.Subtract(unixEpoch)).TotalMilliseconds;

                            if ((schedule.EndDate != 0 && item.Date > (schedule.EndDate) || (schedule.EndDate == 0 && i > 4)))
                                break;
                            if (schedule.ExcludedDates != null && schedule.ExcludedDates.Any())
                            {

                                //var exDateUTC = ;
                                //date = exDateUTC.ToLocalTime();
                                if (item.StartDateTime.Date.Equals(item.EndDateTime.Date) &&
                                    schedule.ExcludedDates.All(x => x != null) &&
                                    schedule.ExcludedDates.Select(x => ToDate(x.ToString(), item.StartHour, item.StartMinute)).Any(x => x.Date.Equals(item.StartDateTime.Date)))
                                {
                                    continue;
                                }
                                else
                                {
                                    //Debugger.Break();
                                }

                            }
                            if (!schedule.HospitalId.Equals(schedule.Hospital.Name))
                                Data.User.Schedules.Add(item);
                        } while (true);
                    }
                }
            }

            // var tmpList = Data.User.Schedules.OrderBy(p => p.StartDateTime);
            var sortList = from p in Data.User.Schedules
                           orderby p.StartDateTime
                           select p;
            // Data.User.Schedules.Clear();
            Data.User.Schedules = new ObservableCollection<Schedule>(sortList.ToList());
            // Debug.WriteLine(sortList);

            return Data.User.Schedules;
        }

        private static DateTime ToDate(string value, int startHour, int startMinute)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = new DateTime();
            long dateInLong;
            if (long.TryParse(value, out dateInLong))
            {
                date = unixEpoch.AddMilliseconds(dateInLong).Date;
            }
            else if (value != null)
            {
                try
                {
                    date = DateTime.ParseExact(value, "yyyyMMdd",
                        CultureInfo.CurrentCulture);
                }
                catch (Exception)
                {
                    Debugger.Break();
                }
            }

            return new DateTime(date.Year, date.Month, date.Day, startHour, startMinute, 0, DateTimeKind.Utc).ToLocalTime();
        }

        private ObservableCollection<Schedule> _selectedSchedule;

        public ObservableCollection<Schedule> SelectedSchedule
        {
            get { return _selectedSchedule; }
            set { SetProperty(ref _selectedSchedule, value); }
        }

        //private ObservableCollection<Schedule> _schedules;
        public ObservableCollection<Schedule> Schedules
        {
            get { return Data.User.Schedules; }
            // ReSharper disable once ValueParameterNotUsed
            set
            {
                RaisePropertyChanged();
                SetSelectedExamination(SelectedDate);
            }
        }



        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        private ICommand _showScheduleAddingCommand;
        public ICommand ShowScheduleAddingCommand
        {
            get
            {
#if MVVMCROSS
                _showScheduleAddingCommand = _showScheduleAddingCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(() => ShowViewModel<ScheduleAddingViewModel>(this));
#else
                _showScheduleAddingCommand = _showScheduleAddingCommand ?? new Template10.Mvvm.MvxCommand(() => ShowViewModel<ScheduleAddingViewModel>(this));
#endif

                return _showScheduleAddingCommand;
            }
        }

        public void SetSelectedExamination(DateTime date)
        {
            //SelectedSchedule = new ObservableCollection<Schedule>();
            SelectedDate = date;
            if (Schedules != null)
            {
                //Debug.WriteLine(JsonConvert.SerializeObject(Schedules.Select(x => new { x.StartDateTime, x.EndDateTime })));
                SelectedSchedule =
                    new ObservableCollection<Schedule>(
                        Schedules.Where(x => DateCompare(x.StartDateTime, date) || DateCompare(x.EndDateTime, date)));
                //                foreach (var examination in Schedules)
                //                {
                //                    if (examination.StartDateTime.Date.Date == date.Date.Date.Date)
                //                    {
                //                        SelectedSchedule.Add(examination);
                //                    }
                //                    //SelectedPatientExamination = new ObservableCollection<PatientExamination>();
                //                }
            }
        }

        public bool DateCompare(DateTime date1, DateTime date2)
        {

            var ret = (date1.Day == date2.Day) && (date1.Month == date2.Month) && (date1.Year == date2.Year);
            return ret;
        }


        public void DoLogoutTap(bool t = true)
        {

            try
            {
                Data.User = null;
                _fileService.Delete("_login.dat");
                if (t)
                    Close(this);
            }
            catch (Exception)
            {
                //throw;

            }

        }

        public void FilterWeekTopic(int mode = -1)
        {
            if (mode == -1)
                mode = _weekTopicsMode;
            if (AllWeekTopics == null)
                return;
            _weekTopicsMode = mode;
            //if (WeekTopics == null)
                WeekTopics = new ObservableCollection<Topic>();



//            foreach (var topic in AllWeekTopics.Where(x => !(_weekTopicsMode == 0 || (_weekTopicsMode == 1 && x.IsOnline) || (_weekTopicsMode == 2 && !x.IsOnline))))
//            {
//                WeekTopics.Remove(WeekTopics.FirstOrDefault(x => x.Id.Equals(topic.Id)));
//            }
            int i = 0;
            foreach (var topic in AllWeekTopics.Where(x => _weekTopicsMode == 0 || (_weekTopicsMode == 1 && x.IsOnline) || (_weekTopicsMode == 2 && !x.IsOnline)))
            {
                if (i < WeekTopics.Count)
                {
                    WeekTopics[i] = topic;
                }
                else
                {
                    WeekTopics.Add(topic);
                }
                i++;
            }
            RaisePropertyChanged(() => WeekTopics);
        }
        #endregion

        #region TopicView

        private ObservableCollection<Topic> _weekTopics;

        public ObservableCollection<Topic> WeekTopics
        {
            get { return _weekTopics; }
            set { SetProperty(ref _weekTopics, value); }
        }

        private int _weekTopicsMode = 0; // 0 = all, 1 = online, 2 = offline

        private ObservableCollection<Topic> _allWeekTopics;

        public ObservableCollection<Topic> AllWeekTopics
        {
            get { return _allWeekTopics; }
            set
            {
                SetProperty(ref _allWeekTopics, value);
                //FilterWeekTopic();
            }
        }


        private Topic _selectedTopic;

        public Topic SelectedTopic
        {
            get { return _selectedTopic; }
            set { SetProperty(ref _selectedTopic, value); }

        }

        public Action TopicSelected;


        private MvxCommand _showWeekTopicCommand;


        public MvxCommand ShowWeekTopicCommand
        {
            get
            {
                _showWeekTopicCommand = _showWeekTopicCommand ?? new MvxCommand(ShowTopic);
                return _showWeekTopicCommand;
            }
        }

        private void ShowTopic()
        {
            TopicSelected();

        }


        private MvxCommand<Topic> _showWeekTopicFileCommand;


        public MvxCommand<Topic> ShowWeekTopicFileCommand
        {
            get
            {
                _showWeekTopicFileCommand = _showWeekTopicFileCommand ?? new MvxCommand<Topic>(ShowWeekfile);
                return _showWeekTopicFileCommand;
            }
        }


        public void ShowWeekfile(Topic t)
        {
            ShowViewModel<WeekTopicFileViewModel>(t);
        }


        #endregion

        #region Consultancy

        private ObservableCollection<ConsultRequest> _consultRequests;
        public ObservableCollection<ConsultRequest> ConsultRequests
        {
            get { return _consultRequests; }
            set { SetProperty(ref _consultRequests, value); }
        }




        private ICommand _showConsultCommand;

        public ICommand ShowConsultCommand
        {
            get
            {
#if MVVMCROSS
                return _showConsultCommand ??
                       (_showConsultCommand = new Cirrious.MvvmCross.ViewModels.MvxCommand<ConsultRequest>(ShowConsult));

#else
                return _showConsultCommand ??
                      (_showConsultCommand = new Template10.Mvvm.MvxCommand<ConsultRequest>(ShowConsult));
#endif

            }
        }

        private void ShowConsult(ConsultRequest c)
        {
            ShowViewModel<ConsultViewModel>(c);
        }
        #endregion

        #region Checkups

        public int CurrentCheckupPage { get; set; }

        private ObservableCollection<Checkup> _checkups;
        public ObservableCollection<Checkup> Checkups
        {
            get { return _checkups; }
            set { SetProperty(ref _checkups, value); }
        }

        private ICommand _checkupViewCommand;

        public ICommand CheckupViewCommand
        {
            get { return _checkupViewCommand ?? (_checkupViewCommand = new MvxCommand<Checkup>(ShowCheckupView)); }
        }

        private void ShowCheckupView(Checkup c)
        {
            ShowViewModel<CheckupViewModel>(c);
        }
        #endregion



        private ICommand _updateProfileCommand;

        public ICommand UpdateProfileCommand
        {
            get
            {
#if MVVMCROSS
                return _updateProfileCommand ??
                       (_updateProfileCommand = new Cirrious.MvvmCross.ViewModels.MvxCommand(UpdateProfile));

#else
                return _updateProfileCommand ??
                      (_updateProfileCommand = new Template10.Mvvm.MvxCommand(UpdateProfile));
#endif
            }
        }

        private void UpdateProfile()
        {
            ShowViewModel<UpdateProfileViewModel>();
        }


        private ICommand _changePassCommand;

        public ICommand ChangePassCommand
        {
            get { return _changePassCommand ?? (_changePassCommand = new MvxCommand(ChangePass)); }
        }
        public int WeekTopicsMode
        {
            get { return _weekTopicsMode; }
            set { _weekTopicsMode = value; }
        }

        private void ChangePass()
        {
            ShowViewModel<SetPasswordViewModel>();

        }
        public async Task SyncPushChannel()
        {
            if (Data.PushSync == false)
                await HealthCareService.Current.NotificationRegister();

        }

        public async Task<bool> GoToMap(Topic t)
        {
            if (t == null)
                return false;
            return await
                _messageService.ShowConfirmMessageAsync(
                    string.Format(AppResources.HomeViewModel_GoToMap, Util.LongtoDateTime(t.StartDateTime).ToString("hh:mm, dd-MM-yyyy"), t.Address),
                    AppResources.HomeViewModel_Offline_meeting, AppResources.Messsage_Yes, AppResources.Messsage_No);
        }
    }
}