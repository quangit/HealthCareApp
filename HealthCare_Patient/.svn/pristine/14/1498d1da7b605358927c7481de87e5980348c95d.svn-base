using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using HealthCare.Helpers;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;

using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCare.Controls
{
    public class CalendarControl : ContentView
    {
        public static readonly BindableProperty SelectedDateChangedCommandProperty =
            BindableProperty.Create<CalendarControl, ICommand>(p => p.SelectedDateChangedCommand, null);

        public static readonly BindableProperty DatesEnableProperty =
            BindableProperty.Create<CalendarControl, ObservableCollection<DateTime>>(p => p.DatesEnable,
                new ObservableCollection<DateTime>(), BindingMode.TwoWay, propertyChanged: DatesEnableChanged);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create<CalendarControl, ICommand>(p => p.Command, null, BindingMode.TwoWay);


        #region SelecteDate

        /// <summary>
        /// Identifies the <see cref="SelecteDate"/> bindable property.
        /// </summary>
        public static readonly BindableProperty SelecteDateProperty =
            BindableProperty.Create<CalendarControl, DateTime?>(p => p.SelecteDate, default(DateTime?), BindingMode.Default, propertyChanged:
                (bindable, value, newValue) =>
                {
                    if (newValue != null)
                    {
                        var control = (CalendarControl)bindable;
                        control._calendar.SelectedDate = newValue.Value.Date;
                        if (newValue.Value.Month != DateTime.Now.Month)
                            control._calendar.DisplayDate = newValue.Value.Date;
                    }
                });

        /// <summary>
        /// Gets or sets the <see cref="SelecteDate" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        ///
        /// </value>
        public DateTime? SelecteDate
        {
            get { return (DateTime?)GetValue(SelecteDateProperty); }
            set { SetValue(SelecteDateProperty, value); }
        }

        #endregion


        //        private RadCalendar _calendar;
        public event EventHandler Loaded;

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

        private static void DatesEnableChanged(BindableObject bindable, ObservableCollection<DateTime> oldValue,
                                               ObservableCollection<DateTime> newValue)
        {
            var control = (CalendarControl)bindable;
            control.DatesEnable = newValue;
            if (control._calendar == null)
                return;

            if (control.SelecteDate != null)
            {
                control._calendar.SelectedDate = control.SelecteDate.Value.Date;
                if (control.SelecteDate.Value.Month != DateTime.Now.Month)
                    control._calendar.DisplayDate = control.SelecteDate.Value.Date;
            }
            else //if (newValue.Contains(DateTime.Now.Date))
                control._calendar.SelectedDate = DateTime.Now.Date;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _calendar = new RadCalendar
            {
                VerticalOptions = LayoutOptions.Start,
            };

            if (Common.OS != TargetPlatform.iOS)
                _calendar.HeightRequest = Common.OnPlatform<double>(400, 280, 400);

            _calendar.WeekNumbersDisplayMode = DisplayMode.Hide;
            //_calendar.DisplayDateChanged += (sender, args) => { Command?.Execute(args.NewValue); };
            _calendar.SetStyleForCell = SetStyleForCell;
            //_calendar.GridLinesDisplayMode = DisplayMode.Hide;
            _calendar.SelectionChanged += (sender, args) =>
            {
                SelectedDateChangedCommand?.Execute(_calendar.SelectedDate);
                SelecteDate = _calendar.SelectedDate;
            };

            Loaded?.Invoke(this, EventArgs.Empty);
            this.BackgroundColor = HcStyles.BorderLightGrayColor;
            this.Padding = new Thickness(1);

            if (SelecteDate != null)
            {
                _calendar.SelectedDate = SelecteDate.Value.Date;
                if (SelecteDate.Value.Month != DateTime.Now.Month)
                    _calendar.DisplayDate = SelecteDate.Value.Date;
            }
            else //if (DatesEnable.Contains(DateTime.Now.Date))
                _calendar.SelectedDate = DateTime.Now.Date;

            Content = _calendar;
        }

        private bool CheckDateEnable(DateTime date)
        {
            return DatesEnable.Any(time => time.Date == date);
        }

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
                        FontSize = Common.OnPlatform<double>(HcStyles.FontSizeSubContent, 18, HcStyles.FontSizeSubContent),
                        FontWeight = FontWeight.Bold,
                        ForegroundColor = Color.Black
                    };
                }

                var defaultStyle = new CalendarCellStyle
                {
                    BackgroundColor = Color.White,
                    BorderColor = Color.FromHex("#D6D6D6"),
                    BorderThickness = new Thickness(0.5),
                    FontSize = Common.OnPlatform<double>(HcStyles.FontSizeSubContent, HcStyles.FontSizeContent, HcStyles.FontSizeSubContent),
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
                    FontSize = Common.OnPlatform<double>(HcStyles.FontSizeSubContent, HcStyles.FontSizeContent, HcStyles.FontSizeSubContent),
                    FontWeight = FontWeight.Normal,
                    ForegroundColor = Color.FromHex("#D6D6D6")
                };
            }
            catch (Exception e)
            {
                UserDialogs.Instance.AlertAsync(e.Message);

                return null;
            }
        }
    }
}