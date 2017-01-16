using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(ListViewCustom), typeof(ListViewCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class ListViewCustomRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            Control.Divider = new ColorDrawable(Android.Graphics.Color.Transparent);
            Control.SetHeaderDividersEnabled(false);
            Control.SetFooterDividersEnabled(false);
        }
    }
}

