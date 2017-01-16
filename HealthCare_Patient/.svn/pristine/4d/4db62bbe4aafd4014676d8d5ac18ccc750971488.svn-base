using System;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class DatePickerCustom : DatePicker
    {
        public static readonly BindableProperty NullableDateProperty =
            BindableProperty.Create<DatePickerCustom, DateTime?>(p => p.NullableDate, null, BindingMode.TwoWay, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var dp = (DatePickerCustom)bindable;
                    dp.UpdateDate();
                });

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create<DatePickerCustom, bool>(
                p => p.IsSelected, default(bool), BindingMode.TwoWay,
                propertyChanged: (bindable, value, newValue) =>
                {
                    var dp = (DatePickerCustom)bindable;
                    dp.IsSelected = newValue;
                });

        private string _format;

        public DatePickerCustom()
        {
            DateSelected += (s, e) =>
            {
                //IsSelected = true;
                switch (ValidateDateTimeType)
                {
                    case ValidateDateTime.BirthDay:
                        BirthDayValidate(e);
                        break;
                    case ValidateDateTime.FutureDay:
                        FutureDayValidate(e);
                        break;
                    default:
                        break;
                }
            };
        }

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set
            {
                SetValue(NullableDateProperty, value);
                UpdateDate();
            }
        }

        public ValidateDateTime ValidateDateTimeType { get; set; }
        public Color TextColor { get; set; }
        public double FontSize { get; set; }
        public Color PlaceHolderColor { get; set; }

        /// <summary>
        ///     If false, please set placeholder text is "Chưa đặt ngày"
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public string PlaceHolderText { get; set; }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                if (null != _format) Format = _format;
                Date = NullableDate.Value;
            }
            else
            {
                _format = Format;
                Format = PlaceHolderText;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Date")
                NullableDate = Date;
        }

        private void BirthDayValidate(DateChangedEventArgs e)
        {
            //validate DOB
            if (e.NewDate >= DateTime.Now.Date)
            {
                NullableDate = DateTime.Now.Date.AddDays(-1);
            }
            else if (e.NewDate.Year == 1900 && Common.OS == TargetPlatform.Android)
            {
                NullableDate = DateTime.Now.Date.AddDays(-1);
            }
        }

        private void FutureDayValidate(DateChangedEventArgs e)
        {
            if (e.NewDate < DateTime.Now.Date)
            {
                NullableDate = Date = DateTime.Now.Date;
            }
        }
    }

    public enum ValidateDateTime
    {
        None,
        BirthDay,
        FutureDay
    }
}