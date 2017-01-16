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
using Xamarin.Forms.Platform.Android;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using HealthCare.Helpers;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class EntryCustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Entry entry = e.NewElement;
            if (entry.BackgroundColor != null)
            {
                Color color = entry.BackgroundColor;
                this.Control.SetBackgroundColor(Android.Graphics.Color.Argb((int)color.A, (int)color.R, (int)color.G, (int)color.B));
            }
            else
            {
                this.Control.Background = this.Resources.GetDrawable(Resource.Drawable.noBorderEditText);
            }
            Control.SetTextColor(Android.Graphics.Color.Black);
            Control.SetPadding(
                  Control.CompoundPaddingLeft,
                  Control.CompoundPaddingTop + 5,
                  Control.CompoundPaddingRight,
                  Control.CompoundPaddingBottom + 5);
        }
    }
}