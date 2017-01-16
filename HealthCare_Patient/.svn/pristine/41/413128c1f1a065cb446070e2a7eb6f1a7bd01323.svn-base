using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using HealthCare.Controls;
using HealthCare.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(CalendarView), typeof(XlabCalendarRenderer))]
namespace HealthCare.iOS.Renderers
{
    public class XlabCalendarRenderer : CalendarViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CalendarView> e)
        {
            base.OnElementChanged(e);
            Control.BoxWidth = (int)(Control.Frame.Width / 7) - 2;
            Control.BackgroundColor = UIColor.DarkGray;
        }
    }
}
