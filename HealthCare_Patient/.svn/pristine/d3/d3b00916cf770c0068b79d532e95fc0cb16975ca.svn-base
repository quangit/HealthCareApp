using System.Windows.Media;
using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Application = System.Windows.Application;
using Thickness = System.Windows.Thickness;

[assembly: ExportRenderer(typeof (ImageButtonCustom), typeof (ImageButtonRenderer))]

namespace HealthCare.WinPhone.Renderer
{
    public class ImageButtonRenderer : XLabs.Forms.Controls.ImageButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control.BorderBrush = (Brush) Application.Current.Resources["TransparentBrush"];
            Control.BorderThickness = Control.Padding = Control.Margin = new Thickness(0);
        }
    }
}