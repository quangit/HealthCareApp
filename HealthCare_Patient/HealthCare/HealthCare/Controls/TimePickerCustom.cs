using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class TimePickerCustom : TimePicker
    {
        public Color TextColor { get; set; }
        public Color PlaceHolderColor { get; set; }
        public double FontSize { get; set; }

        /// <summary>
        ///     If false, please set placeholder text is "Chưa đặt giờ"
        /// </summary>
        public bool IsSelected { get; set; }
    }
}