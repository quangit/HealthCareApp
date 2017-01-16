using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(RatingControl), typeof(RatingControlRenderer))]

namespace HealthCare.Droid.Renderers
{
  public  class RatingControlRenderer: ViewRenderer<Frame, LinearLayout>
  {
      private RatingBar _rtBar;
      protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
      {
          base.OnElementChanged(e);
             _rtBar = new RatingBar(Context, null, Android.Resource.Attribute.RatingBarStyleSmall);
            LinearLayout linearLayout = new LinearLayout(Context);
            var view = e.NewElement as RatingControl;
          if (view != null)
          {
                _rtBar.NumStars = 5;
                _rtBar.Rating =(float)view.Value;
                _rtBar.IsIndicator = !view.IsEnabled;
                _rtBar.StepSize = _rtBar.IsIndicator ? 0.1f : 1;
                _rtBar.RatingBarChange += (o, ev) =>
                {
                    view.Value = ev.Rating;
                };
                linearLayout.AddView(_rtBar);
                this.SetNativeControl(linearLayout);
            }
          
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = sender as RatingControl;
            if (!string.IsNullOrWhiteSpace(e.PropertyName) && e.PropertyName.Equals("IsEnabled") && _rtBar != null && view != null)
            {
                _rtBar.IsIndicator = !view.IsEnabled;
            }
        }
    }
}