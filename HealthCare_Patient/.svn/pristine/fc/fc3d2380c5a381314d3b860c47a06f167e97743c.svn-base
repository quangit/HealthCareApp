using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HealthCare.WinPhone.Renderer;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Calendar;
using Telerik.XamarinForms.InputRenderer.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Application = System.Windows.Application;
using RadCalendar = Telerik.XamarinForms.Input.RadCalendar;

[assembly: ExportRenderer(typeof(RadCalendar), typeof(RadCalendarRenderer))]
namespace HealthCare.WinPhone.Renderer
{
    public class RadCalendarRenderer : CalendarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<RadCalendar> e)
        {
            base.OnElementChanged(e);
            this.Control.Template = (ControlTemplate) Application.Current.Resources["HcRadCalendarControlTemplate"];
            this.Control.MonthInfoDisplayMode = MonthInfoDisplayMode.Large;
        }
    }
}
