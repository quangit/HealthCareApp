using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class ButtonRadiusControl : ContentView
    {
        public ButtonRadiusControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<ButtonRadiusControl, string>(p => p.Text, "", BindingMode.TwoWay,
                propertyChanged: (bindable, value, newValue) =>
                {
                    try
                    {
                        var b = (ButtonRadiusControl)bindable;
                        b.txtText.Text = newValue;
                    }
                    catch
                    {
                        
                    }
                  
                });

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Thickness TextPadding
        {
            get { return ctContent.Padding; }
            set { ctContent.Padding = value; }
        }

        public double BgHeight
        {
            get { return grdContain.HeightRequest; }
            set { grdContain.HeightRequest = imgBackground.HeightRequest = value; }
        }

        public double BgWidth
        {
            get { return grdContain.WidthRequest; }
            set { grdContain.WidthRequest = imgBackground.WidthRequest = value; }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create<ButtonRadiusControl, ICommand>(p => p.Command, null, BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (ButtonRadiusControl)bindable;
                    ctrl.grdContain.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = newValue,
                        //CommandParameter = ctrl.CommandParam
                    });
                });

        //public object CommandParam
        //{
        //    get { return (ICommand)GetValue(CommandParamProperty); }
        //    set { SetValue(CommandParamProperty, value); }
        //}

        //public static readonly BindableProperty CommandParamProperty =
        //    BindableProperty.Create<ButtonRadiusControl, object>(p => p.CommandParam, default(object), BindingMode.Default);

        public double FontSize
        {
            get { return txtText.FontSize; }
            set { txtText.FontSize = value; }
        }

        public LayoutOptions TextVerticalOption
        {
            get { return ctContent.VerticalOptions; }
            set
            {
                ctContent.VerticalOptions = value;
                txtText.VerticalOptions = value;
            }
        }

        public LayoutOptions TextHorizontalOption
        {
            get { return ctContent.HorizontalOptions; }
            set
            {
                ctContent.HorizontalOptions = value;
                txtText.HorizontalOptions = value;
            }
        }
    }
}
