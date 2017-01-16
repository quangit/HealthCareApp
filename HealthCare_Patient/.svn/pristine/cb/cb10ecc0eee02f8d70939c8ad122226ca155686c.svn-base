using System;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    /// <summary>
    ///     Healthcare's Button which can contain a image at left and text on right side.
    ///     In case of url image is empty, button only contain text
    /// </summary>
    public class ButtonHealthCare : CenterRelativeLayout
    {
        private const float PaddingRatio = 1/6f;
        private readonly ButtonCustom _Button;

        public ButtonHealthCare(string text, Color textColor, Color bgColor, string urlImage = "",
            int fontSize = 13, double width = 120, double height = 36, int radius = 5)
        {
            BackgroundColor = Color.Transparent;
            WidthRequest = width;
            HeightRequest = height;

            // Button at background
            _Button = new ButtonCustom
            {
                BackgroundColor = bgColor,
                BorderRadius = radius
            };
            AddCenter(_Button, 1f, 1f);

            // Image at left if urlImage is available
            if (!string.IsNullOrWhiteSpace(urlImage))
            {
                var image = new Image {Source = urlImage};
                Children.Add(image, Constraint.RelativeToParent(parent => { return parent.Height*PaddingRatio; }),
                    Constraint.RelativeToParent(parent => { return parent.Height*PaddingRatio; }),
                    Constraint.RelativeToParent(parent => { return parent.Height*(1f - PaddingRatio*2f); }),
                    Constraint.RelativeToParent(parent => { return parent.Height*(1f - PaddingRatio*2f); }));
            }

            // Label at right
            var label = new Label
            {
                Text = text,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center,
                FontSize = fontSize,
                TextColor = textColor
            };
            Children.Add(label,
                Constraint.RelativeToParent(
                    parent => { return (string.IsNullOrWhiteSpace(urlImage) ? 0 : (parent.Height*(1 - PaddingRatio))); }),
                Constraint.Constant(0),
                Constraint.RelativeToParent(
                    parent =>
                    {
                        return parent.Width -
                               (string.IsNullOrWhiteSpace(urlImage) ? 0 : (parent.Height*(1 - PaddingRatio)));
                    }),
                Constraint.RelativeToParent(parent => { return parent.Height; }));
        }

        public event EventHandler SingleClicked
        {
            add { _Button.SingleClicked += value; }
            remove { _Button.SingleClicked -= value; }
        }
    }
}