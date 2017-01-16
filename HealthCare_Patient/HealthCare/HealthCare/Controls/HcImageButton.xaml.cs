using System;
using System.Windows.Input;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class HcImageButton : RelativeLayout
    {
        public HcImageButton()
        {
            InitializeComponent();
        }

        #region property

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create<HcImageButton, ICommand>(p => p.Command, default(ICommand), propertyChanged:
                (bindable, value, newValue) =>
                {
                    var button = ((HcImageButton) bindable).idButton;
                    button.Command = newValue;
                });

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<HcImageButton, string>(p => p.Text, default(string), propertyChanged:
                (bindable, value, newValue) => { ((HcImageButton) bindable).idLabel.Text = newValue; });

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public FontAttributes FontAttributes
        {
            get { return idLabel.FontAttributes; }
            set { idLabel.FontAttributes = value; }
        }

        public double FontSize
        {
            get { return idLabel.FontSize; }
            set { idLabel.FontSize = value; }
        }

        public string Image
        {
            get { return Common.getImageSource(idImage); }
            set { idImage.Source = value; }
        }

        public double ImageWidth
        {
            get { return idImage.Width; }
            set { idImage.WidthRequest = value; }
        }

        public double ImageHeight
        {
            get { return idImage.Height; }
            set { idImage.HeightRequest = value; }
        }

        public Color BgColor
        {
            get { return idBackground.BackgroundColor; }
            set { idBackground.BackgroundColor = value; }
        }

        public Color TextColor
        {
            get { return idLabel.TextColor; }
            set { idLabel.TextColor = value; }
        }

        public LineBreakMode LineBreakMode
        {
            get { return idLabel.LineBreakMode; }
            set { idLabel.LineBreakMode = value; }
        }

        public int BorderRadius
        {
            get { return idButton.BorderRadius; }
            set { idButton.BorderRadius = value; }
        }

        public event EventHandler Clicked
        {
            add { idButton.Clicked += value; }
            remove { idButton.Clicked -= value; }
        }

        public LayoutOptions ButtonHorizontalOptions
        {
            set
            {
                HorizontalOptions =
                    idBackground.HorizontalOptions =
                        idButton.HorizontalOptions =
                            stlContent.HorizontalOptions = value;
            }
        }

        #endregion
    }
}