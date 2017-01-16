using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Style = System.Windows.Style;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;
using Colors = System.Windows.Media.Colors;

[assembly: ExportRenderer(typeof(RatingControl), typeof(RatingControlRenderer))]
namespace HealthCare.WinPhone.Renderer
{
    public class RatingControlRenderer : FrameRenderer
    {
        private WPRatingControl ratingControl;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            base.OnElementChanged(e);
            var view = e.NewElement as RatingControl;
            if (Control != null && view != null)
            {
                ratingControl = new WPRatingControl();

                ratingControl.SetRatingValue(view.Value);


                ratingControl.ValueChanged += (object sender, double args) =>
                {
                    view.Value = args;
                };

                ratingControl.IsRatingEnabled = view.IsEnabled;

                var border = new Border
                {
                    Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent),
                    BorderThickness = new System.Windows.Thickness(0),
                    BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent),
                    Child = ratingControl,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                };

                SetNativeControl(border);
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = sender as RatingControl;
            if (!string.IsNullOrWhiteSpace(e.PropertyName) && e.PropertyName.Equals("IsEnabled") && ratingControl != null && view != null)
            {
                ratingControl.IsRatingEnabled = view.IsEnabled;
            }
        }
    }
}
