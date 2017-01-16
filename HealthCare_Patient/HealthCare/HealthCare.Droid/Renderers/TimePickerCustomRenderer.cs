using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using HealthCare.Droid.Renderers;
using HealthCare.Controls;
using HealthCare;

[assembly: ExportRenderer(typeof(TimePickerCustom), typeof(TimePickerCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class TimePickerCustomRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);
            var formsPicker = (TimePickerCustom)e.NewElement;
            this.Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            Control.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)formsPicker.FontSize);
            Xamarin.Forms.Color textColor = formsPicker.TextColor;
            if (textColor.R != -1 && textColor.G != -1 && textColor.B != -1)
                this.Control.SetTextColor(Utils.ConvertFormColorToAndroidColor(textColor));

            if (!((TimePickerCustom)e.NewElement).IsSelected)
                Control.Text = "Chưa đặt giờ";

            e.NewElement.Unfocused += (s, ev) =>
            {
                ((TimePickerCustom) e.NewElement).IsSelected = true;
                Control.Text = ((Xamarin.Forms.TimePicker)s).Time.ToString(((Xamarin.Forms.TimePicker)s).Format);
            };

            Control.SetPadding(
                  Control.CompoundPaddingLeft,
                  Control.CompoundPaddingTop + 5,
                  Control.CompoundPaddingRight,
                  Control.CompoundPaddingBottom + 5);
        }
    }
}
