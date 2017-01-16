using System.Windows.Media;
using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Color = System.Windows.Media.Color;

[assembly: ExportRenderer(typeof (TimePickerCustom), typeof (TimePickerCustomRenderer))]

namespace HealthCare.WinPhone.Renderer
{
    public class TimePickerCustomRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            Control.Background = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            Control.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x33, 0x33, 0x33));
            if (!((TimePickerCustom) e.NewElement).IsSelected)
            {
                Control.ValueStringFormat = "Chưa đặt giờ";

                Control.Value = null;
                Control.ValueChanged += (s, ev) =>
                {
                    ((TimePickerCustom) e.NewElement).IsSelected = true;
                    Control.ValueStringFormat = "{0:t}";
                };
            }
        }
    }
}