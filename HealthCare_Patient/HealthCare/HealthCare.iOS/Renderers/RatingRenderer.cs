using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using HealthCare.Controls;
using HealthCare.iOS.Renderers;
using PatridgeDev;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(RatingControl), typeof(RatingRenderer))]

namespace HealthCare.iOS.Renderers
{
   public class RatingRenderer: ViewRenderer<Frame, Rating>
   {
       private Rating _ratingControl;
       protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
       {
           base.OnElementChanged(e);
           if (e.NewElement != null)
           {
               var temp = e.NewElement as RatingControl;
               if (temp != null)
               {
                   _ratingControl = new Rating(temp);
                   SetNativeControl(_ratingControl);
               }
           }
       }

       protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
       {
           base.OnElementPropertyChanged(sender, e);
            var view = sender as RatingControl;
            if (!string.IsNullOrWhiteSpace(e.PropertyName) && e.PropertyName.Equals("IsEnabled") && _ratingControl != null && view != null)
            {
                _ratingControl.ratingView.UserInteractionEnabled = view.IsEnabled;
            }
        }
    }

    public class Rating : UIView
    {
      public  PDRatingView ratingView;
        public Rating(RatingControl rateControl)
        {

            RatingConfig ratingConfig = new RatingConfig(UIImage.FromBundle("empty"), UIImage.FromBundle("chosen"),
                UIImage.FromBundle("chosen"));
            decimal averageRating = (decimal)rateControl.Value;
            ratingView = new PDRatingView(new RectangleF(0f, 0f, 110f, 20f), ratingConfig, averageRating);
            ratingView.UserInteractionEnabled = rateControl.IsEnabled;
            if (rateControl.IsEnabled)
            {
                ratingView.RatingChosen += (sender, e) =>
                {
                    rateControl.Value = e.Rating;
                };
            }
            this.Add(ratingView);
        }
    }
}
