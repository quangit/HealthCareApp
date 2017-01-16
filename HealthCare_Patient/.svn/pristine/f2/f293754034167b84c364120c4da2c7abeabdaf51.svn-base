using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthCare.Helpers;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
//#if WINDOWS_PHONE 

//#endif

namespace HealthCare.Controls
{
    public partial class HcCalendar : ContentView
    {
        public static readonly BindableProperty SelectedDateChangedCommandProperty =
            BindableProperty.Create<HcCalendar, ICommand>(p => p.SelectedDateChangedCommand, null);

        public static readonly BindableProperty DatesEnableProperty =
            BindableProperty.Create<HcCalendar, ObservableCollection<DateTime>>(p => p.DatesEnable,
                new ObservableCollection<DateTime>(), BindingMode.TwoWay, propertyChanged: DatesEnableChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create<HcCalendar, ICommand>(p => p.Command, null, BindingMode.TwoWay);

        private dynamic _calendar;

        public ICommand SelectedDateChangedCommand
        {
            get { return (ICommand)GetValue(SelectedDateChangedCommandProperty); }
            set { SetValue(SelectedDateChangedCommandProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public ObservableCollection<DateTime> DatesEnable
        {
            get { return (ObservableCollection<DateTime>)GetValue(DatesEnableProperty); }
            set { SetValue(DatesEnableProperty, value); }
        }

        public event EventHandler Loaded;

        private static void DatesEnableChanged(BindableObject bindable, ObservableCollection<DateTime> oldValue,
            ObservableCollection<DateTime> newValue)
        {
            var control = (HcCalendar)bindable;
            control.DatesEnable = newValue;
            if (control._calendar == null)
                return;

            if (control.SelecteDate != null)
            {
                control._calendar.SelectedDate = control.SelecteDate.Value.Date;
                if (control.SelecteDate.Value.Month != DateTime.Now.Month)
                    control._calendar.SelectedDate = control.SelecteDate.Value.Date;
            }
            else //if (newValue.Contains(DateTime.Now.Date))
                control._calendar.SelectedDate = DateTime.Now.Date;
        }

        private void fakeEnabledDate()
        {
            DatesEnable = new ObservableCollection<DateTime>();
            DatesEnable.Add(DateTime.Today.AddDays(1));
            DatesEnable.Add(DateTime.Today.AddDays(2));
            DatesEnable.Add(DateTime.Today.AddDays(3));
            DatesEnable.Add(DateTime.Today.AddDays(-2));
            DatesEnable.Add(DateTime.Today.AddDays(-3));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Common.OS == TargetPlatform.iOS || Common.OS == TargetPlatform.Android)
            {
                _calendar = new CalendarView
                {
                    BackgroundColor = Color.White,
                    MinDate = CalendarView.FirstDayOfMonth(DateTime.Now.AddMonths(-12)),
                    MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddMonths(12)),
                    HighlightedDateBackgroundColor = Color.FromRgb(227, 227, 227),
                    ShouldHighlightDaysOfWeekLabels = false,
                    SelectionBackgroundStyle = CalendarView.BackgroundStyle.Fill,
                    TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleFill,
                    ShowNavigationArrows = true,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    MonthTitleForegroundColor = Color.Gray,
                    //DateForegroundColor = Color.White,
                    DayOfWeekLabelForegroundColor = Color.Gray,
                    EnabledDateList = DatesEnable
                };

                ((CalendarView)_calendar).DateSelected += (sender, args) =>
               {
                   if (SelectedDateChangedCommand != null)
                       SelectedDateChangedCommand?.Execute(_calendar.SelectedDate);
                   SelecteDate = _calendar.SelectedDate;
               };
            }
            else
            {
                //#if WINDOWS_PHONE || SILVERLIGHT
                _calendar = new RadCalendar
                {
                    VerticalOptions = LayoutOptions.Start
                };

                if (Common.OS != TargetPlatform.iOS)
                    ((RadCalendar)_calendar).HeightRequest = Common.OnPlatform<double>(400, 280, 400);

                ((RadCalendar)_calendar).WeekNumbersDisplayMode = DisplayMode.Hide;
                //_calendar.DisplayDateChanged += (sender, args) => { Command?.Execute(args.NewValue); };
                ((RadCalendar)_calendar).SetStyleForCell = SetStyleForCell;
                //_calendar.GridLinesDisplayMode = DisplayMode.Hide;
                ((RadCalendar)_calendar).SelectionChanged += (sender, args) =>
               {
                   SelectedDateChangedCommand?.Execute(_calendar.SelectedDate);
                   SelecteDate = _calendar.SelectedDate;
               };
                //#endif
            }


            Loaded?.Invoke(this, EventArgs.Empty);

            if (SelecteDate != null)
            {
                _calendar.SelectedDate = SelecteDate.Value.Date;
                if (SelecteDate.Value.Month != DateTime.Now.Month)
                    _calendar.SelectedDate = SelecteDate.Value.Date;
            }
            else //if (DatesEnable.Contains(DateTime.Now.Date))
                _calendar.SelectedDate = DateTime.Now.Date;

            Content = _calendar;
        }

        private bool CheckDateEnable(DateTime date)
        {
            return DatesEnable.Any(time => time.Date == date);
        }

        //#if WINDOWS_PHONE || SILVERLIGHT
        private CalendarCellStyle SetStyleForCell(CalendarCell cell)
        {
            try
            {
                if (cell.Type == CalendarCellType.DayName)
                {
                    return new CalendarCellStyle
                    {
                        BackgroundColor = Color.White,
                        BorderColor = Color.White,
                        BorderThickness = new Thickness(0.5),
                        FontSize = Common.OnPlatform(HcStyles.FontSizeSubContent, 18, HcStyles.FontSizeSubContent),
                        FontWeight = FontWeight.Bold,
                        ForegroundColor = Color.Black
                    };
                }

                var defaultStyle = new CalendarCellStyle
                {
                    BackgroundColor = Color.White,
                    BorderColor = Color.FromHex("#D6D6D6"),
                    BorderThickness = new Thickness(0.5),
                    FontSize =
                        Common.OnPlatform(HcStyles.FontSizeSubContent, HcStyles.FontSizeContent,
                            HcStyles.FontSizeSubContent),
                    FontWeight = FontWeight.Normal,
                    ForegroundColor = Color.FromHex("#D6D6D6")
                };

                var dayCell = cell as CalendarDayCell;
                if (dayCell != null)
                {
                    if (dayCell.IsFromCurrentMonth)
                    {
                        if (dayCell.Date == DateTime.Today)
                        {
                            defaultStyle.BackgroundColor = Color.FromHex("#778089");
                            defaultStyle.ForegroundColor = Color.White;
                        }
                        else
                        {
                            defaultStyle.BackgroundColor = Color.FromHex("#F5F7F9");
                        }
                    }
                    else
                    {
                        defaultStyle.BackgroundColor = Color.FromHex(dayCell.IsToday ? "#90DFE9" : "#bababa");
                    }

                    if (CheckDateEnable(dayCell.Date))
                    {
                        defaultStyle.ForegroundColor = Color.Black;
                        defaultStyle.BackgroundColor = Color.White;
                    }

                    if (dayCell.IsSelected)
                    {
                        defaultStyle.BackgroundColor = Color.FromHex("#007AFF");
                    }
                    return defaultStyle;
                }

                return new CalendarCellStyle
                {
                    BackgroundColor = Color.White,
                    BorderColor = Color.FromHex("#D6D6D6"),
                    BorderThickness = new Thickness(0.5),
                    FontSize =
                        Common.OnPlatform(HcStyles.FontSizeSubContent, HcStyles.FontSizeContent,
                            HcStyles.FontSizeSubContent),
                    FontWeight = FontWeight.Normal,
                    ForegroundColor = Color.FromHex("#D6D6D6")
                };
            }
            catch (Exception e)
            {
                Common.AlertAsync(e.Message);

                return null;
            }
        }
        //#endif

        #region SelecteDate

        /// <summary>
        ///     Identifies the <see cref="SelecteDate" /> bindable property.
        /// </summary>
        public static readonly BindableProperty SelecteDateProperty =
            BindableProperty.Create<HcCalendar, DateTime?>(p => p.SelecteDate, default(DateTime?), BindingMode.Default,
                propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        if (newValue != null)
                        {
                            var control = (HcCalendar)bindable;
                            control._calendar.SelectedDate = newValue.Value.Date;
                            if (newValue.Value.Month != DateTime.Now.Month)
                                control._calendar.SelectedDate = newValue.Value.Date;
                        }
                    });

        /// <summary>
        ///     Gets or sets the <see cref="SelecteDate" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public DateTime? SelecteDate
        {
            get { return (DateTime?)GetValue(SelecteDateProperty); }
            set { SetValue(SelecteDateProperty, value); }
        }

        #endregion
    }
}