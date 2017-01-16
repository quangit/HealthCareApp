using System;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class BasePickerCustom : Picker
    {
        #region Property

        public Action iOSOpenPicker;
        public Action WinOpenPicker;

        public double FontSize { get; set; }

        public Color TextColor { get; set; }

        #endregion
    }
}