using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HealthCare;
using HealthCare.Controls;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Button = Xamarin.Forms.Button;
using Grid = System.Windows.Controls.Grid;
using Image = System.Windows.Controls.Image;
using Thickness = System.Windows.Thickness;

[assembly: ExportRenderer(typeof (ButtonCustom), typeof (ButtonCustomRenderer))]

namespace HealthCare.WinPhone.Renderer
{
    public class ButtonCustomRenderer : ButtonRenderer
    {
        private Image img;
        private TextBlock label;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            //var sourceButton = this.Element as ImageButton;
            var targetButton = (ButtonCustom) e.NewElement;
            if (targetButton != null && !string.IsNullOrWhiteSpace(targetButton.ImageBackGround))
            {
                img = new Image();
                var colorConverter = new ColorConverter();
                var fontConverter = new FontSizeConverter();

                var bitmap = new BitmapImage(new Uri(targetButton.ImageBackGround, UriKind.Relative));
                var grd = new Grid();
                img.Source = bitmap;

                grd.Children.Add(img);
                if (!string.IsNullOrWhiteSpace(targetButton.WPText))
                {
                    label = new TextBlock
                    {
                        Text = targetButton.WPText,
                        Foreground = (Brush) colorConverter.Convert(targetButton.TextColor, null, null, null),
                        FontSize = targetButton.FontSize,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(targetButton.TextPadding.Left,
                            targetButton.TextPadding.Top, targetButton.TextPadding.Right,
                            targetButton.TextPadding.Bottom)
                    };

                    grd.Children.Add(label);
                }

                targetButton.WPTextChangeAction = s => { Dispatcher.BeginInvoke(() => { label.Text = s; }); };

                Control.Content = grd;
                Control.Padding = new Thickness(0);
            }
        }
    }
}