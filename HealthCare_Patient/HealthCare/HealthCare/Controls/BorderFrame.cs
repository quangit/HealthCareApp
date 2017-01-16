using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    internal class BorderFrame : ContentView
    {
        public BorderFrame()
        {
            BackgroundColor = HcStyles.BorderGrayColor;
        }

        public BorderFrame(View content, int left, int right, int top, int bottom)
        {
            BackgroundColor = HcStyles.BorderGrayColor;
            Padding = new Thickness(left, top, right, bottom);
            Content = content;
        }
    }
}