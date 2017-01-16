using System;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class DatePickerWithImageBox : StackLayout
    {
        /// <summary>
        ///     The Text state property.
        /// </summary>
        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create<DatePickerWithImageBox, DateTime>(
                p => p.SelectedValue, default(DateTime), BindingMode.TwoWay,
                propertyChanged: OnSelectedValuePropertyChanged);

        //public static readonly BindableProperty NullableDateProperty =
        //    BindableProperty.Create<DatePickerWithImageBox, DateTime?>(p => p.NullableDate, null,
        //        BindingMode.TwoWay);

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create<DatePickerWithImageBox, bool>(
                p => p.IsSelected, default(bool), BindingMode.TwoWay,
                propertyChanged: (bindable, value, newValue) =>
                {
                    var dp = (DatePickerWithImageBox)bindable;
                    dp.IsSelected = newValue;
                });

        public readonly Thickness HcDatePickerPadding = new Thickness(0, Common.OnPlatform<double>(0, 0, -20), 0, 0);
        private Image _image;
        private bool _isLoaded;
        private string _source;
        public DatePickerCustom dp;

        public DatePickerWithImageBox()
        {
            BindingContextChanged += (sender, args) =>
            {
                if (!_isLoaded && Loaded != null)
                {
                    _isLoaded = true;
                    Loaded.Invoke(this, EventArgs.Empty);
                }
            };

            Padding = HcDatePickerPadding;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control is Text.
        /// </summary>
        /// <value>The Text state.</value>
        public DateTime SelectedValue
        {
            get { return (DateTime)GetValue(SelectedValueProperty); }
            set
            {
                if (SelectedValue.Date != value.Date)
                {
                    SetValue(SelectedValueProperty, value);
                    SelectedValueChanged.Invoke(this, value);
                }
            }
        }

        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                if (_image != null)
                    _image.Source = new FileImageSource { File = value };
            }
        }

        public ValidateDateTime ValidateDateTimeType { get; set; }

        //public DateTime? NullableDate
        //{
        //    get { return (DateTime?)GetValue(NullableDateProperty); }
        //    set { SetValue(NullableDateProperty, value); }
        //}

        public Color TextColor { get; set; }
        public double FontSize { get; set; }
        public Color PlaceHolderColor { get; set; }
        public Color LineColor { get; set; } = HcStyles.GreenColor;
        public DateTime? MaximumDate { get; set; }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public string PlaceHolderText { get; set; }
        public event EventHandler Loaded;

        /// <summary>
        ///     The Text changed event.
        /// </summary>
        public event EventHandler<EventArgs<DateTime>> SelectedValueChanged;

        /// <summary>
        ///     Called when [checked property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">if set to <c>true</c> [oldvalue].</param>
        /// <param name="newvalue">if set to <c>true</c> [newvalue].</param>
        private static void OnSelectedValuePropertyChanged(BindableObject bindable, DateTime oldvalue, DateTime newvalue)
        {
            var checkBox = (DatePickerWithImageBox)bindable;
            checkBox.SelectedValue = newvalue;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            InitView();
        }

        private void InitView()
        {
            var stlParent = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = Common.OnPlatform(5, 0, -22)
            };
            _image = new Image { Source = Source };
            _image.WidthRequest = HcStyles.ImageInEntryWidth;
            _image.HeightRequest = HcStyles.ImageInEntryHeight;

            var stl1 = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(Common.OnPlatform(8, 8, 8, 8), Common.OnPlatform(0, 0, 8, 8)),
                Spacing = Common.OS == TargetPlatform.WinPhone ? 0 : 8
            };
            stl1.Children.Add(_image);
            TextColor = HcStyles.BlackTextColor;
            FontSize = HcStyles.FontSizeContent;
            dp = new DatePickerCustom
            {
                TextColor = TextColor,
                PlaceHolderColor = PlaceHolderColor,
                //IsSelected = IsSelected,
                PlaceHolderText = PlaceHolderText,
                FontSize = FontSize,
                ValidateDateTimeType = ValidateDateTimeType,
                HorizontalOptions = Common.OnPlatform<LayoutOptions>(LayoutOptions.FillAndExpand, LayoutOptions.StartAndExpand, LayoutOptions.StartAndExpand),
                BindingContext = this
            };
            //dp.SetBinding(DatePicker.DateProperty, new Binding("SelectedValue", BindingMode.TwoWay));

            if (Common.OS == TargetPlatform.Android)
                switch (ValidateDateTimeType)
                {
                    case ValidateDateTime.BirthDay:
                        SelectedValue = DateTime.Now.Date.AddYears(-30);
                        //NullableDate = null;
                        break;
                    case ValidateDateTime.FutureDay:
                    case ValidateDateTime.None:
                        SelectedValue = DateTime.Now.Date;
                        break;
                }

            dp.SetBinding(DatePickerCustom.NullableDateProperty, new Binding("SelectedValue", BindingMode.TwoWay));
            dp.SetBinding(DatePickerCustom.IsSelectedProperty, new Binding("IsSelected", BindingMode.TwoWay));
            dp.TextColor = TextColor;
            dp.ValidateDateTimeType = ValidateDateTimeType;


            if (MaximumDate.HasValue)
                dp.MaximumDate = MaximumDate.Value;

            stl1.Children.Add(dp);

            var stl2 = new StackLayout
            {
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = LineColor
            };
            stlParent.Children.Add(stl1);
            stlParent.Children.Add(stl2);

            HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            Children.Clear();
            Children.Add(stlParent);
        }
    }
}