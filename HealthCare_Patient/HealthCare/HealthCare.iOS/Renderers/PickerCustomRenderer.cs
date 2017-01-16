using System;
using System.Drawing;
using Xamarin.Forms;
using HealthCare.Controls;
using HealthCare.iOS;
using Xamarin.Forms.Platform.iOS;
using HealthCare.Models;
using UIKit;

[assembly: ExportRenderer(typeof(BasePickerCustom), typeof(PickerCustomRenderer))]
namespace HealthCare.iOS
{
    public class PickerCustomRenderer:PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            BasePickerCustom FormPicker = (BasePickerCustom)e.NewElement;
            if (Control == null || FormPicker == null) return;
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
            if (FormPicker != null)
            {
                Control.Font = UIKit.UIFont.SystemFontOfSize((nfloat)FormPicker.FontSize);
                Control.TextColor = Utils.ConvertFormColorToIoSColor(FormPicker.TextColor);
                FormPicker.Unfocused += (s, ev) =>
                {
                    var temp = Control.Text;
                    if (temp.Length > 25)
                    {
                        Control.Text = temp.Remove(22, Control.Text.Length - 22) + "...";
                    }
                    if (((BasePickerCustom)e.NewElement).iOSOpenPicker != null)
                            ((BasePickerCustom)e.NewElement).iOSOpenPicker.Invoke();
                    };
            }
            
            this.Frame = new RectangleF(0, 0, 100, 50);

            Control.AdjustsFontSizeToFitWidth = false;
        }
    }
}

