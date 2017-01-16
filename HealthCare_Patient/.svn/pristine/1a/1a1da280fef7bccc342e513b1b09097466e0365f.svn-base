using System;
using System.Collections.Generic;
using System.Text;
using HealthCare.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TimePickerRenderer = HealthCare.iOS.Renderers.TimePickerRenderer;

[assembly: ExportRenderer(typeof(TimePickerCustom), typeof(TimePickerRenderer))]
namespace HealthCare.iOS.Renderers
{
    public class TimePickerRenderer : Xamarin.Forms.Platform.iOS.TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            var formsTimePicker = (TimePickerCustom)e.NewElement;
            Control.Font = UIKit.UIFont.SystemFontOfSize((nfloat)formsTimePicker.FontSize);
            Control.TextColor = Utils.ConvertFormColorToIoSColor(formsTimePicker.TextColor);
            if (formsTimePicker != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
                if (!formsTimePicker.IsSelected)
                {
                    Control.Text = "Chưa đặt giờ";
                }

                e.NewElement.Unfocused += (s, ev) =>
                {
                    formsTimePicker.IsSelected = true;
                    Control.Text = ((TimePicker)s).Time.ToString(((TimePicker)s).Format);
                };
            }
        }
    }
}
