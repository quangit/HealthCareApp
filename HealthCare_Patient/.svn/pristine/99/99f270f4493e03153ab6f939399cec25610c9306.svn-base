using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using UIKit;

namespace HealthCare.iOS.Renderers
{
    public class CropperView : UIView
    {
        CGPoint origin;
        CGSize cropSize;
        private int width = 200;
        private int height = 200;
        public CropperView()
        {
            origin = new CGPoint((UIScreen.MainScreen.Bounds.Width - width) / 2, (UIScreen.MainScreen.Bounds.Height - height - 64) / 2);
            cropSize = new CGSize(width, height);

            BackgroundColor = UIColor.Clear;
            Opaque = false;

            Alpha = 0.8f;
        }

        public CGPoint Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
                SetNeedsDisplay();
            }
        }

        public CGSize CropSize
        {
            get
            {
                return cropSize;
            }
            set
            {
                cropSize = value;
                SetNeedsDisplay();
            }
        }

        public CGRect CropRect
        {
            get
            {
                return new CGRect(Origin, CropSize);
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (var g = UIGraphics.GetCurrentContext())
            {

                g.SetFillColor(UIColor.Gray.CGColor);
                g.FillRect(rect);

                g.SetBlendMode(CGBlendMode.Clear);
                UIColor.Clear.SetColor();

                var path = new CGPath();
                path.AddRect(new CGRect(origin, cropSize));

                g.AddPath(path);
                g.DrawPath(CGPathDrawingMode.Fill);
            }
        }
    }
}
