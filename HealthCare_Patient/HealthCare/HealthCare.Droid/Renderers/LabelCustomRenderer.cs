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
using Android.Graphics;

[assembly: ExportRenderer(typeof(LabelCustom), typeof(LabelCustomRenderer))]
namespace HealthCare.Droid.Renderers
{
    class LabelCustomRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            LabelCustom label = (LabelCustom)e.NewElement;
            if (label.FontAttributes == FontAttributes.Bold)
            {
                // In case of Label is title in actionbar
                Typeface f = Typeface.CreateFromAsset(this.Context.Assets, "Roboto-Regular.ttf");
                this.Control.SetTypeface(f, TypefaceStyle.Bold);
            }
            if (label.Lines > 0)
            {
                Control.SetLines(label.Lines);
            }
            
        }
    }
}