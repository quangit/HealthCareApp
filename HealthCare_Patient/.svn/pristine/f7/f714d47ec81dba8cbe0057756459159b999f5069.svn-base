using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using HealthCare.Droid.Renderers;
using HealthCare.Controls;
using HealthCare;

[assembly: ExportRenderer(typeof(DatePickerCustom), typeof(DatePickerCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class DatePickerCustomRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            var formsPicker = (DatePickerCustom)e.NewElement;
            this.Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            Control.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)formsPicker.FontSize);
            Xamarin.Forms.Color textColor = formsPicker.TextColor;
            if (textColor.R != -1 && textColor.G != -1 && textColor.B != -1)
                this.Control.SetTextColor(Utils.ConvertFormColorToAndroidColor(textColor));
            DatePickerCustom picker = (DatePickerCustom)e.NewElement;
            if (!picker.IsSelected)
            {
                Control.Text = string.IsNullOrWhiteSpace(picker.PlaceHolderText) ? "Chưa đặt ngày" : picker.PlaceHolderText;
            }

            e.NewElement.Unfocused += (s, ev) =>
            {
                ((DatePickerCustom) e.NewElement).IsSelected = true;
                Control.Text = ((Xamarin.Forms.DatePicker)s).Date.ToString("d");
            };


            Control.SetPadding(
                  Control.CompoundPaddingLeft,
                  Control.CompoundPaddingTop + 5,
                  Control.CompoundPaddingRight,
                  Control.CompoundPaddingBottom + 5);
        }
    }
}
