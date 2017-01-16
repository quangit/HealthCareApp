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
using Xamarin.Forms;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using HealthCare.Models;

[assembly: ExportRenderer(typeof(BasePickerCustom), typeof(PickerCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class PickerCustomRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            var formPicker = (BasePickerCustom)e.NewElement;
            Control.SetBackgroundResource(Android.Graphics.Color.Transparent);
            Control.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)formPicker.FontSize);
            Control.SetSingleLine();
            Xamarin.Forms.Color textColor = formPicker.TextColor;
            if (textColor.R != -1 && textColor.G != -1 && textColor.B != -1)
                this.Control.SetTextColor(Utils.ConvertFormColorToAndroidColor(textColor));
        }
    }
}