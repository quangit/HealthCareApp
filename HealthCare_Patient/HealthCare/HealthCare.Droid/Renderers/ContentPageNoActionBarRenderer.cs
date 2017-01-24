using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPageNoActionBar), typeof(ContentPageNoActionBarRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class ContentPageNoActionBarRenderer : PageRenderer
    {

        public ContentPageNoActionBarRenderer()
        {
            //global::Xamarin.Forms.Forms.SetTitleBarVisibility(Xamarin.Forms.AndroidTitleBarVisibility.Never);
            //((Activity)Context).ActionBar.Hide();
            //Forms.Context.SetTheme(Android.Resource.Style.ThemeHoloLightNoActionBar);
        }
        protected override void OnDraw(Canvas canvas)
        {
            //var actionBar = ((Activity)Context).ActionBar;
            //actionBar.Hide();            
            
            base.OnDraw(canvas);
        }
    }
}