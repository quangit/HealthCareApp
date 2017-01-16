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

[assembly: ExportRenderer(typeof(Editor), typeof(EditorCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class EditorCustomRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);
            //Control.SetBackgroundColor(Android.Graphics.Color.Argb(60, 44, 190, 113));
            Control.SetBackgroundColor(Android.Graphics.Color.Rgb(228, 228, 228));

        }
    }
}
