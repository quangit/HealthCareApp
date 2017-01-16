using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.WinPhone.Renderer;
using Microsoft.Phone.Controls.Primitives;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Style = System.Windows.Style;
using Application = System.Windows.Application;

[assembly: ExportRenderer(typeof(Switch), typeof(SwitchCustomRenderer))]
namespace HealthCare.WinPhone.Renderer
{
    public class SwitchCustomRenderer : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);
            var child = Control?.Child as ToggleSwitchButton;
            if (child != null)
                child.Style = (Style)Application.Current.Resources["HcToggleSwitchButtonStyle"];
        }
    }
}
