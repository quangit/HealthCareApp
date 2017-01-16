using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CoreGraphics;
using Foundation;
using HealthCare.ViewModels;
using ObjCRuntime;
using UIKit;

namespace HealthCare.iOS.Crop
{
    internal class Class0
    {
        private static UIImagePickerController uiimagePickerController_0;

        public static Action<NSDictionary> action_0;

        private readonly UIViewController uiviewController_0;

        public Class0(UIViewController uiviewController_1)
        {
            this.uiviewController_0 = uiviewController_1;
        }

        public UIImagePickerController method_0()
        {
            if (Class0.uiimagePickerController_0 == null)
            {
                UIImagePickerController uIImagePickerController = new UIImagePickerController();
                uIImagePickerController.Delegate = (new Class0.Control0());
                Class0.uiimagePickerController_0 = uIImagePickerController;
            }
            return Class0.uiimagePickerController_0;
        }

        private void method_1(UIImagePickerController uiimagePickerController_1)
        {
            Class0.uiimagePickerController_0 = uiimagePickerController_1;
        }

        public void method_2(Action<NSDictionary> action_1)
        {
            Class0.smethod_0();
            Class0.uiimagePickerController_0.SourceType = UIImagePickerControllerSourceType.Camera;
            Class0.action_0 = action_1;
            this.uiviewController_0.PresentViewController(Class0.uiimagePickerController_0, true, null);
        }

        public void method_3(Action<NSDictionary> action_1)
        {
            Class0.smethod_0();
            Class0.uiimagePickerController_0.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            Class0.action_0 = action_1;
            //this.uiviewController_0.PresentViewController(Class0.uiimagePickerController_0, true, null);
        }

        private static void smethod_0()
        {
            if (Class0.uiimagePickerController_0 != null)
            {
                return;
            }
            UIImagePickerController uIImagePickerController = new UIImagePickerController();
            uIImagePickerController.Delegate = (new Class0.Control0());
            Class0.uiimagePickerController_0 = uIImagePickerController;
        }

        private class Control0 : UIImagePickerControllerDelegate
        {
            public Control0()
            {
            }

            public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
            {
                Action<NSDictionary> action0 = Class0.action_0;
                Class0.action_0 = null;
                if (action0 != null)
                {
                    action0(info);
                }
            }
        }
    }
    internal class Class1 : UIView
    {
        private readonly CropData cropData_0;

        public Class1(CropData cropData_1, CGRect rectangleF_0) : base(rectangleF_0)
        {
            this.cropData_0 = cropData_1;
            this.method_0();
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);
            if (this.cropData_0.CropType == CropData.CroppingTypes.Full)
            {
                this.method_1();
                return;
            }
            if (this.cropData_0.CropType == CropData.CroppingTypes.Square)
            {
                this.method_2();
                return;
            }
            if (this.cropData_0.CropType == CropData.CroppingTypes.Circle)
            {
                this.method_3();
                return;
            }
            if (this.cropData_0.CropType == CropData.CroppingTypes.Elipse)
            {
                this.method_4();
            }
        }

        private void method_0()
        {
            this.BackgroundColor = (UIColor.Clear);
            this.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
            this.UserInteractionEnabled = (false);
        }

        private void method_1()
        {
        }

        private void method_2()
        {
            using (CGContext currentContext = UIGraphics.GetCurrentContext())
            {
                this.cropData_0.FillColor.SetFill();
                var width = this.Frame.Width;
                var frame = this.Frame;
                currentContext.FillRect(new RectangleF(0f, 0f, (float)width, (float)frame.Height));
            }
            using (CGContext cGContext = UIGraphics.GetCurrentContext())
            {
                UIColor.Clear.SetFill();
                cGContext.SetBlendMode(CGBlendMode.Clear);
                cGContext.FillRect(this.cropData_0.GetSquareRectangleF());
                cGContext.SetBlendMode(CGBlendMode.Color);
            }
            using (CGContext currentContext1 = UIGraphics.GetCurrentContext())
            {
                currentContext1.SetLineWidth(this.cropData_0.BorderWidth);
                this.cropData_0.BorderColor.SetStroke();
                currentContext1.FillRect(this.cropData_0.GetSquareRectangleF());
            }
        }

        private void method_3()
        {
            using (CGContext currentContext = UIGraphics.GetCurrentContext())
            {
                this.cropData_0.FillColor.SetFill();
                var width = this.Frame.Width;
                var frame = this.Frame;
                currentContext.FillRect(new RectangleF(0f, 0f, (float)width, (float)frame.Height));
            }
            using (CGContext cGContext = UIGraphics.GetCurrentContext())
            {
                cGContext.SetLineWidth(this.cropData_0.BorderWidth);
                this.cropData_0.BorderColor.SetStroke();
                this.cropData_0.FillColor.SetFill();
                cGContext.AddEllipseInRect(this.cropData_0.GetCircleRectangleF());
                UIColor.Clear.SetFill();
                cGContext.SetBlendMode(CGBlendMode.Clear);
                cGContext.DrawPath(CGPathDrawingMode.EOFillStroke);
                cGContext.SetBlendMode(CGBlendMode.Color);
            }
            using (CGContext currentContext1 = UIGraphics.GetCurrentContext())
            {
                currentContext1.SetLineWidth(this.cropData_0.BorderWidth);
                this.cropData_0.BorderColor.SetStroke();
                currentContext1.AddEllipseInRect(this.cropData_0.GetCircleRectangleF());
                currentContext1.DrawPath(CGPathDrawingMode.Stroke);
            }
        }

        private void method_4()
        {
            using (CGContext currentContext = UIGraphics.GetCurrentContext())
            {
                this.cropData_0.FillColor.SetFill();
                var width = this.Frame.Width;
                var frame = this.Frame;
                currentContext.FillRect(new RectangleF(0f, 0f, (float)width, (float)frame.Height));
            }
            using (CGContext cGContext = UIGraphics.GetCurrentContext())
            {
                cGContext.SetLineWidth(this.cropData_0.BorderWidth);
                this.cropData_0.BorderColor.SetStroke();
                this.cropData_0.FillColor.SetFill();
                cGContext.AddEllipseInRect(this.cropData_0.GetElipseRectangleF());
                UIColor.Clear.SetFill();
                cGContext.SetBlendMode(CGBlendMode.Clear);
                cGContext.DrawPath(CGPathDrawingMode.EOFillStroke);
                cGContext.SetBlendMode(CGBlendMode.Color);
            }
            using (CGContext currentContext1 = UIGraphics.GetCurrentContext())
            {
                currentContext1.SetLineWidth(this.cropData_0.BorderWidth);
                this.cropData_0.BorderColor.SetStroke();
                currentContext1.AddEllipseInRect(this.cropData_0.GetElipseRectangleF());
                currentContext1.DrawPath(CGPathDrawingMode.EOFillStroke);
            }
        }
    }
    internal class Class2
    {
        public static bool bool_0;

        static Class2()
        {
            Class2.bool_0 = true;
        }

        public Class2()
        {
        }

        public static bool smethod_0()
        {
            return Runtime.Arch == 0;
        }
    }

    internal static class Class5
    {
        public static string smethod_0(this string string_0)
        {
            return NSBundle.MainBundle.LocalizedString(string_0, "", "");
        }
    }
    internal static class Class6
    {
        public static float smethod_0(this float float_0)
        {
            return float_0 * (float)UIScreen.MainScreen.Scale;
        }
    }
    internal class Control1 : UIViewController
    {
        private const int int_0 = 1;

        private readonly UIImage uiimage_0;

        private UIScrollView uiscrollView_0;

        private UIImageView uiimageView_0;

        private static Action<UIImage> action_0
        {
            get;
            set;
        }

        private readonly CropData cropData_0;

        public Control1(CropData cropData_1, UIImage uiimage_1)
        {
            this.cropData_0 = cropData_1;
            this.uiimage_0 = uiimage_1;
            this.method_1();
        }

        private void method_0(object sender, EventArgs e)
        {
            Action<UIImage> action0 = Control1.action_0;
            Control1.action_0 = null;
            UIImage uiimage0 = this.uiimage_0;
            if (this.cropData_0.CropType != CropData.CroppingTypes.Full)
            {
                uiimage0 = this.method_5();
                uiimage0 = this.method_4(uiimage0, this.cropData_0.GetCroppingRectangleF());
            }
            UserViewModel.Instance.SetAvatarByByteArray(uiimage0.ToBytes());
            PhotoEditViewModel.Instance.GoBack();
        }

        private void method_1()
        {
            this.View.BackgroundColor = (UIColor.Clear);
            UIImageView uIImageView = new UIImageView(this.uiimage_0);
            var width = this.uiimage_0.Size.Width;
            var size = this.uiimage_0.Size;
            uIImageView.Frame = (new RectangleF(0f, 10f, (float) width, (float) size.Height));
            uIImageView.Tag = (1);
            this.uiimageView_0 = uIImageView;
            var single = this.View.Frame.Width;
            var frame = this.View.Frame;
            UIScrollView uIScrollView = new UIScrollView(new RectangleF(0f, 0f, (float) single, (float) frame.Height));
            var width1 = this.View.Frame.Width;
            var rectangleF = this.View.Frame;
            uIScrollView.ContentSize = (new SizeF((float) width1, (float) rectangleF.Height));
            this.uiscrollView_0 = uIScrollView;
            this.uiscrollView_0.AddSubview(this.uiimageView_0);
            this.View.AddSubview(this.uiscrollView_0);
            var class1 = new Class1(this.cropData_0, this.uiscrollView_0.Frame);
            this.View.AddSubview(class1);
            this.uiscrollView_0.MaximumZoomScale = (this.cropData_0.MaximumZoomScale);
            this.uiscrollView_0.MaximumZoomScale = (this.cropData_0.MinimumZoomScale);
            UIScrollView uiscrollView0 = this.uiscrollView_0;
            uiscrollView0.ViewForZoomingInScrollView= ((UIScrollViewGetZoomView)Delegate.Combine(uiscrollView0.ViewForZoomingInScrollView, new UIScrollViewGetZoomView(view => this.uiimageView_0)));

            this.uiscrollView_0.ZoomingEnded += (sender, args) =>
            {
                Console.WriteLine(string.Concat("Z: ", this.uiscrollView_0.ZoomScale));
            };

            this.uiscrollView_0.ZoomScale = (this.cropData_0.StartZoomScale);
            this.uiscrollView_0.ContentInset = (this.method_2(0f));
        }

        private UIEdgeInsets method_2(float float_0)
        {
            var height = this.uiscrollView_0.Frame.Height;
            RectangleF croppingRectangleF = this.cropData_0.GetCroppingRectangleF();
            var single = (height - croppingRectangleF.Height)/2f - 20f + float_0;
            var width = this.uiscrollView_0.Frame.Width;
            RectangleF rectangleF = this.cropData_0.GetCroppingRectangleF();
            var width1 = (width - rectangleF.Width)/2f;
            var height1 = this.uiscrollView_0.Frame.Height;
            RectangleF croppingRectangleF1 = this.cropData_0.GetCroppingRectangleF();
            var single1 = (height1 - croppingRectangleF1.Height)/2f;
            var width2 = this.uiscrollView_0.Frame.Width;
            RectangleF rectangleF1 = this.cropData_0.GetCroppingRectangleF();
            var single2 = (width2 - rectangleF1.Width)/2f + float_0;
            return new UIEdgeInsets(single, width1, single1, single2);
        }

        public void method_3(Action<UIImage> action_1)
        {
            Control1.action_0 = action_1;
        }

        private UIImage method_4(UIImage uiimage_1, RectangleF rectangleF_0)
        {
            var size = uiimage_1.Size;
            UIGraphics.BeginImageContext(new SizeF(rectangleF_0.Width, rectangleF_0.Height));
            CGContext currentContext = UIGraphics.GetCurrentContext();
            RectangleF rectangleF = new RectangleF(0f, 0f, rectangleF_0.Width, rectangleF_0.Height);
            currentContext.ClipToRect(rectangleF);
            RectangleF rectangleF1 = new RectangleF(-rectangleF_0.X, -rectangleF_0.Y, (float) size.Width,
                (float) size.Height);
            uiimage_1.Draw(rectangleF1);
            UIImage imageFromCurrentImageContext = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return imageFromCurrentImageContext;
        }

        private UIImage method_5()
        {
            var frame = this.View.Frame;
            UIGraphics.BeginImageContext(frame.Size);
            if (!UIDevice.CurrentDevice.CheckSystemVersion(7, 0))
            {
                this.View.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            }
            else
            {
                this.View.DrawViewHierarchy(this.View.Frame, true);
            }
            UIImage imageFromCurrentImageContext = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return imageFromCurrentImageContext;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            float single = 25f.smethod_0();
            var frame = this.View.Frame;
            UILabel uILabel = new UILabel(new RectangleF(0f, single, (float)frame.Width, 20f.smethod_0()));
            uILabel.Text = ("Move and scale".smethod_0());
            uILabel.Font = (UIFont.SystemFontOfSize(18f));
            uILabel.TextColor = (UIColor.White);
            uILabel.TextAlignment = UITextAlignment.Right;
            UILabel uILabel1 = uILabel;
            float single1 = 10f.smethod_0();
            var rectangleF = this.View.Frame;
            float height = (float)rectangleF.Height - 32f.smethod_0();
            var frame1 = this.View.Frame;
            UIButton uIButton =
                new UIButton(new RectangleF(single1, height, (float)frame1.Width/2f - 15f.smethod_0(), 25f.smethod_0()));
            uIButton.SetTitle("Cancel".smethod_0(), 0);
            uIButton.Font = (UIFont.SystemFontOfSize(18f));
            uIButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            uIButton.BackgroundColor = (UIColor.Clear);
            uIButton.TouchUpInside +=
                (new EventHandler((object sender, EventArgs e) => PhotoEditViewModel.Instance.GoBack()));
            var rectangleF1 = this.View.Frame;
            var width = rectangleF1.Width/2f;
            var frame2 = this.View.Frame;
            var height1 = frame2.Height - 32f.smethod_0();
            var rectangleF2 = this.View.Frame;
            UIButton uIButton1 =
                new UIButton(new RectangleF((float) width, (float)height1, (float)rectangleF2.Width/2f - 10f.smethod_0(),
                    25f.smethod_0()));
            uIButton1.SetTitle("Select".smethod_0(), 0);
            uIButton1.Font = (UIFont.SystemFontOfSize(18f));
            uIButton1.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
            uIButton1.BackgroundColor = (UIColor.Clear);
            uIButton1.TouchUpInside+=(new EventHandler(this.method_0));

            PhotoEditViewModel.Instance.CropEvent += this.method_0;

            //this.View.AddSubviews(new UIView[] {uIButton, uIButton1});
            //if (Class2.bool_0)
            //{
            //    var frame3 = this.View.Frame;
            //    var height2 = frame3.Height/2f - 50f.smethod_0();
            //    var rectangleF3 = this.View.Frame;
            //    UILabel uILabel2 = new UILabel(new RectangleF(0f, (float)height2, (float)rectangleF3.Width, 50f.smethod_0()));
            //    uILabel2.Font = (UIFont.BoldSystemFontOfSize(60f));
            //    uILabel2.TextColor = (UIColor.Red);
            //    uILabel2.TextAlignment = UITextAlignment.Center;
            //    this.View.AddSubview(uILabel2);
            //}
        }
    }
}