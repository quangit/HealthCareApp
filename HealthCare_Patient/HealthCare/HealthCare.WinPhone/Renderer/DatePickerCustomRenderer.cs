using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Application = System.Windows.Application;
using Style = System.Windows.Style;
using Thickness = System.Windows.Thickness;

[assembly: ExportRenderer(typeof(DatePickerCustom), typeof(DatePickerCustomRenderer))]

namespace HealthCare.WinPhone.Renderer
{
    public class DatePickerCustomRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            Control.Style = (Style)Application.Current.Resources["DatePickerStyle"];
            var picker = (DatePickerCustom)e.NewElement;
            if (!picker.IsSelected)
            {
                Control.ValueStringFormat = string.IsNullOrWhiteSpace(picker.PlaceHolderText)
                    ? "Chưa đặt ngày"
                    : picker.PlaceHolderText;
                Control.Padding = new Thickness(0);
                Control.Value = null;
                Control.ValueChanged += (s, ev) =>
                {
                    ((DatePickerCustom)e.NewElement).IsSelected = true;
                    Control.ValueStringFormat = "{0:d}";
                };
            }
        }
    }
}