using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Telerik.Windows.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Application = System.Windows.Application;
using DataTemplate = System.Windows.DataTemplate;
using Style = System.Windows.Style;
using Thickness = System.Windows.Thickness;

[assembly: ExportRenderer(typeof(Picker), typeof(PickerCustomRenderer))]

namespace HealthCare.WinPhone.Renderer
{
    public class PickerCustomRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            var temp1 = e.NewElement;



            var temp = Control as System.Windows.Controls.Grid;
            var child = temp.Children[0] as Microsoft.Phone.Controls.ListPicker;
            child.Foreground = new SolidColorBrush(Colors.Black);
            child.Background = new SolidColorBrush(Colors.White);

            Control.Margin = new Thickness(0);

            Control.IsHitTestVisible = e.NewElement.IsEnabled;

            e.NewElement.PropertyChanged += (sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(args.PropertyName) && args.PropertyName.Equals("IsEnabled"))
                {
                    Control.IsHitTestVisible = e.NewElement.IsEnabled;
                }
            };

            //child.HeaderTemplate = (DataTemplate)Application.Current.Resources["ListPickerTemplate"]; 
            //System.Windows.FrameworkElement fr = child;
            //var c  = VisualTreeHelper.GetChildrenCount(child);
            //////child.Foreground = new SolidColorBrush(Colors.Black);
            ////System.Windows.Controls.Control..
            //child.Style = (Style)Application.Current.Resources["ListPickerStyle"];
        }
    }
}