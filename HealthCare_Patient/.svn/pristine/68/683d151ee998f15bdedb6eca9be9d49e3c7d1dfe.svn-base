using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class ContentControl : ContentView
    {
        /// <summary>
        ///     The Text state property.
        /// </summary>
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create<ContentControl, string>(
                p => p.Icon, "", BindingMode.TwoWay,
                propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        ((ContentControl) bindable).imgIcon.Source = new FileImageSource {File = newValue};
                    });

        /// <summary>
        ///     The Text state property.
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create<ContentControl, string>(
                p => p.Title, "", BindingMode.TwoWay,
                propertyChanged: (bindable, value, newValue) => { ((ContentControl) bindable).lbTitle.Text = newValue; });

        /// <summary>
        ///     The Text state property.
        /// </summary>
        public static readonly BindableProperty ViewContentProperty =
            BindableProperty.Create<ContentControl, Layout>(
                p => p.ViewContent, default(Layout), BindingMode.TwoWay,
                propertyChanged:
                    (bindable, value, newValue) => { ((ContentControl) bindable).cvContent.Content = newValue; });

        public ContentControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control is Text.
        /// </summary>
        /// <value>The Text state.</value>
        public string Icon
        {
            get { return (string) GetValue(IconProperty) ?? ""; }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control is Text.
        /// </summary>
        /// <value>The Text state.</value>
        public string Title
        {
            get { return (string) GetValue(TitleProperty) ?? ""; }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control is Text.
        /// </summary>
        /// <value>The Text state.</value>
        public Layout ViewContent
        {
            get { return (Layout) GetValue(ViewContentProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}