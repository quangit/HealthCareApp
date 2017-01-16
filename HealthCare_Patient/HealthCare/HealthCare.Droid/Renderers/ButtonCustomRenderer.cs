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

[assembly: ExportRenderer(typeof(ButtonCustom), typeof(ButtonCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class ButtonCustomRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            ButtonCustom button = (ButtonCustom)e.NewElement;

//            if (button.ImageBackGround != null)
//            {
//                string[] separators = { ".", " " };
//                int resID = Resources.GetIdentifier(button.ImageBackGround.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0], "drawable", MainActivity.sPackageName);
//                Control.SetBackgroundResource(resID);
//            }

            Thickness p = button.TextPadding;
            this.Control.SetPadding((int)p.Left, (int)p.Top, (int)p.Right, (int)p.Bottom);

        }
    }
}
