using System;
using Xamarin.Forms;
using HealthCare;
using Xamarin.Forms.Platform.iOS;
using HealthCare.iOS;
using HealthCare.Controls;

[assembly: ExportRenderer(typeof(ButtonCustom), typeof(ButtonCustomRenderer))]
namespace HealthCare.iOS
{
    public class ButtonCustomRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var newBt = (ButtonCustom)e.NewElement;

            if (newBt != null && !string.IsNullOrWhiteSpace(newBt.ImageBackGround))
                Control.SetBackgroundImage(new UIKit.UIImage(newBt.ImageBackGround), UIKit.UIControlState.Normal);
        }
    }
}

