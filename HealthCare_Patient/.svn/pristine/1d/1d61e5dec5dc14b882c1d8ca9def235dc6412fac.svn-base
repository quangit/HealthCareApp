using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class BusyControl : Frame
    {
        public ActivityIndicator indicator;

        public BusyControl()
        {
            indicator = new ActivityIndicator
            {
                Color = HcStyles.GreenColor,
                //BackgroundColor = Color.Black.MultiplyAlpha(.8),
                IsRunning = true,
                IsVisible = true,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            if (Common.OS == TargetPlatform.WinPhone)
                indicator.HorizontalOptions = LayoutOptions.FillAndExpand;

            BackgroundColor = Color.FromHex("#732d2d2d").MultiplyAlpha(.9);
            //this.BackgroundColor = Color.White.MultiplyAlpha(.9);
            IsVisible = false;
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Content = indicator;
        }
    }
}