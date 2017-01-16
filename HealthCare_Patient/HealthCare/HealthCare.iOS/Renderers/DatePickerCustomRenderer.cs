using System;
using Xamarin.Forms;
using HealthCare.Controls;
using HealthCare.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerCustom), typeof(DatePickerCustomRenderer))]
namespace HealthCare.iOS
{
    public class DatePickerCustomRenderer : DatePickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            var datepicker = (DatePickerCustom)e.NewElement;
            if (datepicker != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
                Control.Font = UIKit.UIFont.SystemFontOfSize((nfloat)datepicker.FontSize);
                Control.TextColor = Utils.ConvertFormColorToIoSColor(datepicker.TextColor);
                if (!datepicker.IsSelected)
                {
                    Control.Text = string.IsNullOrWhiteSpace(datepicker.PlaceHolderText) ? "Chưa đặt ngày" : datepicker.PlaceHolderText;
                }

                e.NewElement.Unfocused += (s, ev) =>
                {
                    datepicker.IsSelected = true;
                    Control.Text = ((DatePicker)s).Date.ToString("d");
                };
            }
        }
    }
}

