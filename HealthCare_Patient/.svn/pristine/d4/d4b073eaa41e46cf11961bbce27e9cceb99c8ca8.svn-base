using System;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class EntryImage : StackLayout
    {
        /// <summary>
        ///     The Text state property.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<EntryImage, string>(
                p => p.Text, string.Empty, BindingMode.TwoWay, propertyChanged: OnTextPropertyChanged);

        private string _image;
        public EntryLogin entry;
        private Image image;

        /// <summary>
        ///     Gets or sets a value indicating whether the control is Text.
        /// </summary>
        /// <value>The Text state.</value>
        public string Text
        {
            get { return GetValue(TextProperty) != null ? GetValue(TextProperty).ToString() : ""; }

            set
            {
                SetValue(TextProperty, value);
                TextChanged.Invoke(this, value);
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                if (image != null)
                {
                    image.Source = new FileImageSource {File = value};
                }
            }
        }

        public Color TextColor { get; set; } = Color.Black;
        public Color PlaceHolderColor { get; set; } = Color.Black;
        public string PlaceHolderText { get; set; }
        public bool IsPassword { get; set; }
        public Color LineColor { get; set; } = HcStyles.GreenColor;
        public bool IsLogin { get; set; }
        public double ImageWidthRequest { get; set; }
        public double ImageHeightRequest { get; set; }
        public Keyboard Keyboard { get; set; }
        public bool IsTextEnabled { get; set; }

        /// <summary>
        ///     The Text changed event.
        /// </summary>
        public event EventHandler<EventArgs<string>> TextChanged;

        /// <summary>
        ///     Called when [checked property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">if set to <c>true</c> [oldvalue].</param>
        /// <param name="newvalue">if set to <c>true</c> [newvalue].</param>
        private static void OnTextPropertyChanged(BindableObject bindable, string oldvalue, string newvalue)
        {
            var checkBox = (EntryImage) bindable;
            checkBox.Text = newvalue;
            if (checkBox.entry != null)
                checkBox.entry.Text = newvalue;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            InitView();
        }

        public void UnFocus()
        {
            if (entry != null)
                entry.Unfocus();
        }

        private void InitView()
        {
            var stlParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = Common.OnPlatform(5, 0, !IsTextEnabled ? -25 : -12)
            };

            image = new Image
            {
                Source = new FileImageSource {File = Image},
                WidthRequest = ImageWidthRequest,
                HeightRequest = ImageHeightRequest
            };

            var grd1 = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = GridLength.Auto},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                }
            };

            Grid.SetColumn(image, 0);
            grd1.Children.Add(image);

            entry = new EntryLogin
            {
                TextColor = TextColor,
                IsPassword = IsPassword,
                Placeholder = PlaceHolderText,
                PlaceHolderColor = PlaceHolderColor,
                IsLogin = IsLogin,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Keyboard = Keyboard != null ? Keyboard : Keyboard.Default,
                IsEnabled = IsTextEnabled
            };
            entry.TextColor = TextColor;

            Grid.SetColumn(entry, 1);
            grd1.Children.Add(entry);

            var stl2 = new StackLayout
            {
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = LineColor
            };
            stlParent.Children.Add(grd1);
            stlParent.Children.Add(stl2);

            if (entry != null)
                entry.TextChanged += (s, e) => Text = e.NewTextValue;

            VerticalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
            Children.Clear();
            Children.Add(stlParent);
        }
    }
}