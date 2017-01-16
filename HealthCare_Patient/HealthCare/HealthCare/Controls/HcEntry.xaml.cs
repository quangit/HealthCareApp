using System;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class HcEntry : StackLayout
    {
        private bool _isLoaded;

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<HcEntry, string>(p => p.Text, default(string), BindingMode.TwoWay);

        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create<HcEntry, string>(p => p.Source, default(string), BindingMode.TwoWay,
                propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        ((HcEntry) bindable).idImage.Source = new FileImageSource {File = newValue};
                    });

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<HcEntry, Color>(p => p.TextColor, Color.Black, propertyChanged:
                (bindable, value, newValue) =>
                {
                    ((HcEntry) bindable).idEntry.PlaceHolderColor = ((HcEntry) bindable).idEntry.TextColor = newValue;
                });

        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create<HcEntry, string>(p => p.PlaceHolder, default(string), propertyChanged:
                (bindable, value, newValue) => { ((HcEntry) bindable).idEntry.Placeholder = newValue; });

        public static readonly BindableProperty IsLoginProperty =
            BindableProperty.Create<HcEntry, bool>(p => p.IsLogin, default(bool), propertyChanged:
                (bindable, value, newValue) => { ((HcEntry) bindable).idEntry.IsLogin = newValue; });

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create<HcEntry, bool>(p => p.IsPassword, default(bool), propertyChanged:
                (bindable, value, newValue) => { ((HcEntry) bindable).idEntry.IsPassword = newValue; });

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create<HcEntry, double>(p => p.FontSize, default(double), propertyChanged:
                (bindable, value, newValue) => { ((HcEntry) bindable).idEntry.FontSize = (float) newValue; });

        public static readonly BindableProperty HideKeyboardProperty =
            BindableProperty.Create<HcEntry, bool>(p => p.HideKeyboard, false, BindingMode.TwoWay, propertyChanged:
                (bindable, value, newValue) =>
                {
                    var entry = ((HcEntry) bindable).idEntry;
                    entry.Unfocus();
                });

        public event EventHandler Loaded;

        public HcEntry()
        {
            InitializeComponent();
            idEntry.BindingContext = this;
            idEntry.SetBinding(Entry.TextProperty, "Text", BindingMode.TwoWay);
            idEntry.SetBinding(IsFocusedProperty, "IsFocus", BindingMode.TwoWay);
            BindingContextChanged += (sender, args) =>
            {
                if (!_isLoaded && Loaded != null)
                {
                    _isLoaded = true;
                    Loaded.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Source
        {
            get { return (string) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color) GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public string PlaceHolder
        {
            get { return (string) GetValue(PlaceHolderProperty); }
            set
            {
                SetValue(PlaceHolderProperty, value);
                idEntry.Placeholder = value;
            }
        }

        public bool IsLogin
        {
            get { return (bool) GetValue(IsLoginProperty); }
            set { SetValue(IsLoginProperty, value); }
        }

        public bool IsPassword
        {
            get { return (bool) GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public bool HideKeyboard
        {
            get { return (bool) GetValue(HideKeyboardProperty); }
            set { SetValue(HideKeyboardProperty, value); }
        }

        public Keyboard Keyboard
        {
            get { return idEntry.Keyboard; }
            set { idEntry.Keyboard = value; }
        }

        public double FontSize
        {
            get { return (double) GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public Color RuleColor
        {
            get { return idRule.BackgroundColor; }
            set { idRule.BackgroundColor = value; }
        }

        public bool IsVisibleImage
        {
            get { return idImage.IsVisible; }
            set { idImage.IsVisible = value; }
        }

        public double ImageWidth
        {
            get { return idImage.WidthRequest; }
            set { idImage.WidthRequest = value; }
        }

        public double ImageHeight
        {
            get { return idImage.HeightRequest; }
            set { idImage.HeightRequest = value; }
        }

        public double TextSize
        {
            get { return idEntry.FontSize; }
            set { idEntry.FontSize = (float) value; }
        }

        public void UnFocus()
        {
            idEntry?.Unfocus();
        }

        private void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        #region CanEdit

        /// <summary>
        ///     Identifies the <see cref="CanEdit" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CanEditProperty =
            BindableProperty.Create<HcEntry, bool>(p => p.CanEdit, true, BindingMode.Default, propertyChanged:
                (bindable, value, newValue) =>
                {
                    ((HcEntry) bindable).idEntry.IsEnabled = newValue;
                    if (!newValue)
                        ((HcEntry) bindable).idEntry.TextColor = HcStyles.GrayDisabledText;
                    else
                        ((HcEntry) bindable).idEntry.TextColor = HcStyles.BlackTextColor;
                });

        /// <summary>
        ///     Gets or sets the <see cref="CanEdit" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public bool CanEdit
        {
            get { return (bool) GetValue(CanEditProperty); }
            set { SetValue(CanEditProperty, value); }
        }

        #endregion
    }
}