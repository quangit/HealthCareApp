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
using Android.Util;

[assembly: ExportRenderer(typeof(EntryLogin), typeof(EntryLoginRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class EntryLoginRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            EntryLogin entry = (EntryLogin)e.NewElement;
            if (entry.IsLogin)
            {
                Control.SetTextColor(Android.Graphics.Color.White);
                Control.SetHintTextColor(Android.Graphics.Color.White);
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            else
            {
                Control.SetTextColor(Android.Graphics.Color.Black);
                Control.SetHintTextColor(Android.Graphics.Color.Black);
                this.Control.Background = this.Resources.GetDrawable(Resource.Drawable.noBorderEditText);
            }
            Typeface f = Typeface.CreateFromAsset(this.Context.Assets, "Roboto-Regular.ttf");
            if (entry.IsPassword)
            {
                Control.SetTypeface(f, TypefaceStyle.Normal);
            }
            else
            {
                Control.SetTypeface(f, TypefaceStyle.Normal);
            }
            if (!entry.IsEnabled)
            {
                Control.SetTextColor(Android.Graphics.Color.Gray);
                Control.SetHintTextColor(Android.Graphics.Color.Gray);
            }

            Xamarin.Forms.Color bgColor = entry.BackgroundColor;
            if (bgColor.R != -1 && bgColor.G != -1 && bgColor.B != -1)
                this.Control.SetBackgroundColor(Utils.ConvertFormColorToAndroidColor(bgColor));

            Xamarin.Forms.Color textColor = entry.TextColor;
            if (textColor.R != -1 && textColor.G != -1 && textColor.B != -1)
            {
                this.Control.SetHintTextColor(Utils.ConvertFormColorToAndroidColor(textColor));
                this.Control.SetTextColor(Utils.ConvertFormColorToAndroidColor(textColor));
            }
            if (entry.FontSize > 0)
            {
                this.Control.SetTextSize(ComplexUnitType.Sp, (float)entry.FontSize);
            }

            Control.SetPadding(
                  Control.CompoundPaddingLeft,
                  //Control.CompoundPaddingTop + 5,
                  Control.CompoundPaddingTop + (entry.IsLogin ? 5 : 0),

                  Control.CompoundPaddingRight,
                  Control.CompoundPaddingBottom + (entry.IsLogin ? 5 : 0));
        }


    }
}
