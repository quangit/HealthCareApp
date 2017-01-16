using HealthCare;
using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Application = System.Windows.Application;
using DataTemplate = System.Windows.DataTemplate;

[assembly: ExportRenderer(typeof (TabbedPageCustom), typeof (TabbedPageCustomRenderer))]

namespace HealthCare.WinPhone.Renderer
{
    public class TabbedPageCustomRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            HeaderTemplate = (DataTemplate) Application.Current.Resources["SmallPanoramaTitle"];
        }
    }
}