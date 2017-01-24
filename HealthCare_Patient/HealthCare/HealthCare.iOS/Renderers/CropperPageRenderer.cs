using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CoreGraphics;
using Foundation;
using HealthCare.iOS.Crop;
using HealthCare.iOS.DependencyServices;
using HealthCare.iOS.Renderers;
using HealthCare.Pages;
using HealthCare.ViewModels;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PhotoEditPage), typeof(CropperPageRenderer))]

namespace HealthCare.iOS.Renderers
{
    public class CropperPageRenderer : PageRenderer
    {
        private CropperView cropperView;
        private UITapGestureRecognizer doubleTap;
        private UIImageView imageView;
        private UIPanGestureRecognizer pan;
        private UIPinchGestureRecognizer pinch;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var page = e.NewElement as PhotoEditPage;
            var view = NativeView;

            if (page != null)
            {
                PhotoEditViewModel.Instance.CreateToolBar(page);
            }

            //var label = new UILabel(new CGRect(20, 40, view.Frame.Width - 40, 40));
            //label.AdjustsFontSizeToFitWidth = true;
            //label.TextColor = UIColor.White;

            //if (page != null)
            //{
            //    label.Text = "Crop";
            //}

            //view.Add(label);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //var browserButton = new UIBarButtonItem("Browser", UIBarButtonItemStyle.Plain, BrowserButtonEventHandler);
            // var first = new UIView();
            //NavigationItem.SetRightBarButtonItem(null, true);

            // if (UserViewModel.Instance.AvatarByteArray == null) return;
            // UserViewModel.Instance.AvatarByteArray = (new ImageResize()).ResizeImage2(UserViewModel.Instance.AvatarByteArray, (int)UIScreen.MainScreen.Bounds.Width, 0);

            // using (var image = UserViewModel.Instance.AvatarByteArray.ToImage())
            // {
            //     imageView =
            //         new UIImageView(new CGRect(0, 0, image.Size.Width, image.Size.Height));
            //     imageView.Image = image;
            //     imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            // }



            // //using (var image = UserViewModel.Instance.AvatarByteArray.ToImage())
            // //{
            // //    imageView =
            // //        new UIImageView(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height));

            // //    imageView.Image = image;
            // //    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            // //}

            // cropperView = new CropperView {Frame = View.Frame};
            // View.AddSubviews(imageView, cropperView);

            // nfloat dx = 0;
            // nfloat dy = 0;

            // pan = new UIPanGestureRecognizer(() =>
            // {
            //     if ((pan.State == UIGestureRecognizerState.Began || pan.State == UIGestureRecognizerState.Changed) &&
            //         (pan.NumberOfTouches == 1))
            //     {
            //         var p0 = pan.LocationInView(View);

            //         if (dx == 0)
            //             dx = p0.X - cropperView.Origin.X;

            //         if (dy == 0)
            //             dy = p0.Y - cropperView.Origin.Y;

            //         var oriX = p0.X - dx;
            //         var oriY = p0.Y - dy;

            //         if (oriX < 0 || oriX > UIScreen.MainScreen.Bounds.Width - cropperView.CropSize.Width)
            //         {
            //             oriX = cropperView.Origin.X;
            //             dx = p0.X - oriX;
            //         }

            //         if (oriY < 0 || oriY > UIScreen.MainScreen.Bounds.Height - cropperView.CropSize.Height - 64)
            //         {
            //             oriY = cropperView.Origin.Y;
            //             dy = p0.Y - oriY;
            //         }

            //         var p1 = new CGPoint(oriX, oriY);
            //         Debug.WriteLine("{0} - {1}", p1.X, p1.Y);
            //         cropperView.Origin = p1;
            //     }
            //     else if (pan.State == UIGestureRecognizerState.Ended)
            //     {
            //         dx = 0;
            //         dy = 0;
            //     }
            // });

            // nfloat s0 = 1;

            // pinch = new UIPinchGestureRecognizer(() =>
            // {
            //     var s = pinch.Scale;
            //     var ds = (nfloat) Math.Abs(s - s0);
            //     nfloat sf = 0;
            //     const float rate = 0.5f;

            //     if (s >= s0)
            //     {
            //         sf = 1 + ds*rate;
            //     }
            //     else if (s < s0)
            //     {
            //         sf = 1 - ds*rate;
            //     }
            //     s0 = s;

            //     cropperView.CropSize = new CGSize(cropperView.CropSize.Width*sf, cropperView.CropSize.Height*sf);

            //     if (pinch.State == UIGestureRecognizerState.Ended)
            //     {
            //         s0 = 1;
            //     }
            // });

            // doubleTap = new UITapGestureRecognizer((gesture) => Crop())
            // {
            //     NumberOfTapsRequired = 2,
            //     NumberOfTouchesRequired = 1
            // };

            // cropperView.AddGestureRecognizer(pan);
            // cropperView.AddGestureRecognizer(pinch);
            // //cropperView.AddGestureRecognizer(doubleTap);

            // //var firstController = UIApplication.SharedApplication.KeyWindow.RootViewController.ChildViewControllers.First().ChildViewControllers.Last().ChildViewControllers.First();
            // //var navcontroller = firstController as UINavigationController;
            // //var temp = View.Window.RootViewController.NavigationController;
            // //View.Window.RootViewController.NavigationController.NavigationItem.RightBarButtonItem = 
            // //    new UIBarButtonItem(UIImage.FromFile("crop.png")
            // //        , UIBarButtonItemStyle.Plain
            // //        , (sender, args) =>
            // //        {
            // //            // button was clicked
            // //            Crop();
            // //        });
            // UINavigationBar homeNavBar = new UINavigationBar(new System.Drawing.RectangleF(0f, 0f, (float)View.Frame.Width, 42f));
            // UINavigationItem homeNavItem = new UINavigationItem();
            // UIBarButtonItem mySaveButton = new UIBarButtonItem("Save", UIBarButtonItemStyle.Done, (object sender, EventArgs e) => this.Crop());
            // homeNavItem.RightBarButtonItem = mySaveButton;
            // List<UINavigationItem> lstNavs = new List<UINavigationItem>();
            // lstNavs.Add(homeNavItem);
            // homeNavBar.Items = lstNavs.ToArray();

            // this.View.Add(homeNavBar);

            //var width = 100 * UIScreen.MainScreen.Scale;
            //var height = 100 * UIScreen.MainScreen.Scale;
            //var corner = width / 2;

            //var dimentions = new RectangleF((float)View.Frame.Width / 2 - (float)width / 2, (float)View.Frame.Height / 3 - (float)height / 2, (float)width, (float)height);

            //_imageView = new UIImageView(dimentions);
            //_imageView.Layer.CornerRadius = corner;
            //_imageView.Layer.BorderColor = UIColor.LightGray.CGColor;
            //_imageView.Layer.BorderWidth = 1.0f;
            //_imageView.Layer.MasksToBounds = true;
            //_imageView.UserInteractionEnabled = true;
            //_imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            //_imageView.AddGestureRecognizer(new UITapGestureRecognizer(tap => Tap(CropData.CroppingTypes.Square)));
            //View.AddSubview(_imageView);
            //Tap(CropData.CroppingTypes.Square);

            //var image = UserViewModel.Instance.AvatarByteArray.ToImage();

            //if (class0_0 == null)
            //{
            //    class0_0 = new Class0(this);
            //}
            //if (this.CroppingData == null)
            //{
            //    this.CroppingData = new CropData();
            //}
            //this.class0_0.method_0().NavigationBarHidden = (true);
            //this.class0_0.method_0().NavigationBar.Translucent = (true);
            //Control1 control1 = new Control1(new CropData() { CropType = CropData.CroppingTypes.Square }, image);
            //this.class0_0.method_3((NSDictionary nsdictionary_0) =>
            //{

            //    if (image != null)
            //    {
            //        method_2(new CropData() { CropType = CropData.CroppingTypes.Square }, image);
            //    }
            //});
            //View.Add(control1.View);
            //control1.ViewWillAppear(false);
        }

        public override void ViewDidAppear(bool appear)
        {
            //this.NavigationController.NavigationBarHidden = (true);
            //this.NavigationController.NavigationBar.Translucent = true;
            var image = UserViewModel.Instance.AvatarByteArray.ToImage();

            if (class0_0 == null)
            {
                class0_0 = new Class0(this);
            }
            if (this.CroppingData == null)
            {
                this.CroppingData = new CropData();
            }
            //this.class0_0.method_0().NavigationBarHidden = (true);
            //this.class0_0.method_0().NavigationBar.Translucent = (true);
            Control1 control1 = new Control1(new CropData() { CropType = CropData.CroppingTypes.Square }, image);

            this.class0_0.method_3((NSDictionary nsdictionary_0) =>
            {

                if (image != null)
                {
                    method_2(new CropData() { CropType = CropData.CroppingTypes.Square }, image);
                }
            });
            control1.method_3((UIImage argument0) => this.SelectedImage = argument0);

            View.Add(control1.View);
        }
        public void method_2(CropData cropData_0, UIImage uiimage_1)
        {
            //this.class0_0.method_0().NavigationBarHidden = (true);
            //this.class0_0.method_0().NavigationBar.Translucent = (true);
            Control1 control1 = new Control1(cropData_0, uiimage_1);
            this.class0_0.method_0().PushViewController(control1, true);
            control1.method_3((UIImage argument0) => this.SelectedImage = argument0);
        }
        public UIImage SelectedImage { get; set; }

        public CropData CroppingData;

        private Class0 class0_0;

        private UIImageView _imageView;
        private void Tap(CropData.CroppingTypes cropType)
        {
            var actionSheetPhoto = new ActionSheetPhoto(this)
            {
                CroppingData = { CropType = cropType }
            };

            actionSheetPhoto.PropertyChanged += ActionSheetPhotoOnPropertyChanged;
            var image = UserViewModel.Instance.AvatarByteArray.ToImage();
            actionSheetPhoto.method_2(new CropData() { CropType = CropData.CroppingTypes.Square }, image);
            //actionSheetPhoto.ShowInView(View);
        }

        private void ActionSheetPhotoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UIImage image = ((ActionSheetPhoto)sender).SelectedImage;
            _imageView.Image = image;
        }

        private void Crop()
        {
            var inputCGImage = UserViewModel.Instance.AvatarByteArray.ToImage().CGImage;

            var image = inputCGImage.WithImageInRect(cropperView.CropRect);
            using (var croppedImage = UIImage.FromImage(image))
            {
                var rect = new CGRect(cropperView.CropRect.X, cropperView.CropRect.Y - 64, cropperView.CropRect.Width,
                    cropperView.CropRect.Height);

                imageView.Image = croppedImage;
                imageView.Frame = rect;
                imageView.Center = View.Center;

                cropperView.Origin = new CGPoint(imageView.Frame.Left, imageView.Frame.Top);
                cropperView.Hidden = true;

                UserViewModel.Instance.UpdateAvatar(imageView.Image.ToBytes());
                //PhotoEditViewModel.Instance.GoBack();
            }
        }
    }
}