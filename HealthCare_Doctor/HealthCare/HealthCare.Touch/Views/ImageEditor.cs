using System;

using UIKit;
using CoreGraphics;
using System.Drawing;

namespace HealthCare.Touch.Views
{
	public partial class ImageEditor : UIViewController
	{
		private UIImageView imageView;
		private CropperView cropperView;
		private UIPanGestureRecognizer pan;
		private UIPinchGestureRecognizer pinch;
		private UITapGestureRecognizer doubleTap;
		public UIImage Image {get;set;}

		public event EventHandler OnFinish;

		public ImageEditor () : base ("ImageEditor", null)
		{
		}
		public UIImage MaxResizeImage(UIImage sourceImage, nfloat maxWidth, nfloat maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = maxWidth / sourceSize.Width;
			if (maxResizeFactor > 1) return sourceImage;
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContext(new CGSize(width, height));
			sourceImage.Draw(new CGRect(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			CropBarButton.Clicked += (sender, e) => Crop();
			RotateBarButton.Enabled = false;
			RotateBarButton.Clicked += (sender, e) => Rotate();
			ConfirmBarButton.Enabled = false;
			ConfirmBarButton.Clicked += (sender, e) => {

				OnFinished();
			};
		}

		private void OnFinished()
		{
			NavigationController.PopViewController(true);
			EventHandler handler = OnFinish;
			if (handler != null)
			{
				handler(this, null);
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			//var imgHeight = View.Frame.Width * Image.Size.Height / Image.Size.Width;
			Image = MaxResizeImage (Image, View.Frame.Width, View.Frame.Height);
			imageView = new UIImageView (new CGRect (0, 0, Image.Size.Width, Image.Size.Height));
			imageView.Image = Image;
			cropperView = new CropperView { Frame = imageView.Frame };
			//View.Frame = new CGRect (0, 0, Image.Size.Width, Image.Size.Height);

			View.AddSubviews (imageView,cropperView);

			nfloat dx = 0;
			nfloat dy = 0;

			pan = new UIPanGestureRecognizer (() => {
				if ((pan.State == UIGestureRecognizerState.Began || pan.State == UIGestureRecognizerState.Changed) && (pan.NumberOfTouches == 1)) {

					var p0 = pan.LocationInView (View);

					if (dx == 0)
						dx = p0.X - cropperView.Origin.X;

					if (dy == 0)
						dy = p0.Y - cropperView.Origin.Y;

					var p1 = new CGPoint (p0.X - dx, p0.Y - dy);

					cropperView.Origin = p1;
				} else if (pan.State == UIGestureRecognizerState.Ended) {
					dx = 0;
					dy = 0;
				}
			});

			nfloat s0 = 1;

			pinch = new UIPinchGestureRecognizer (() => {
				nfloat s = pinch.Scale;
				nfloat ds = (nfloat)Math.Abs (s - s0);
				nfloat sf = 0;
				const float rate = 0.5f;

				if (s >= s0) {
					sf = 1 + ds * rate;
				} else if (s < s0) {
					sf = 1 - ds * rate;
				}
				s0 = s;

				cropperView.CropSize = new CGSize (cropperView.CropSize.Width * sf, cropperView.CropSize.Height * sf);  

				if (pinch.State == UIGestureRecognizerState.Ended) {
					s0 = 1;
				}
			});

//			doubleTap = new UITapGestureRecognizer ((gesture) => Crop ()) { 
//				NumberOfTapsRequired = 2, NumberOfTouchesRequired = 1 
//			};


			cropperView.AddGestureRecognizer (pan);
			cropperView.AddGestureRecognizer (pinch);
//			cropperView.AddGestureRecognizer (doubleTap);
		}

		private bool _cropped = false;
		private void Crop()
		{
			if (!_cropped) {
				var inputCGImage = Image.CGImage;
				var image = inputCGImage.WithImageInRect (cropperView.CropRect);
				var croppedImage = UIImage.FromImage (image);

					Image = croppedImage;
					imageView.Image = croppedImage;
					imageView.Frame = cropperView.CropRect;
					//imageView.Center = View.Center;

					//cropperView.Origin = new CGPoint (imageView.Frame.Left, imageView.Frame.Top);
					cropperView.Hidden = true;

				CropBarButton.Enabled = false;
				RotateBarButton.Enabled = true;
				ConfirmBarButton.Enabled = true;
			} else {
				cropperView.Hidden = false;
			}
			_cropped = true;
		}

		private void Rotate(){
			Image = ScaleAndRotateImage (Image);
			imageView.Image = Image;
		}

		private UIImage ScaleAndRotateImage(UIImage imageIn) {
			int kMaxResolution = 2048;

			CGImage imgRef = imageIn.CGImage;
			float width = imgRef.Width;
			float height = imgRef.Height;
			CGAffineTransform transform = CGAffineTransform.MakeIdentity ();
			RectangleF bounds = new RectangleF( 0, 0, width, height );

			if ( width > kMaxResolution || height > kMaxResolution )
			{
				float ratio = width/height;

				if (ratio > 1)
				{
					bounds.Width  = kMaxResolution;
					bounds.Height = bounds.Width / ratio;
				}
				else
				{
					bounds.Height = kMaxResolution;
					bounds.Width  = bounds.Height * ratio;
				}
			}

			float scaleRatio = bounds.Width / width;
			SizeF imageSize = new SizeF( width, height);
			UIImageOrientation orient = UIImageOrientation.Left;
			float boundHeight;
			switch(orient)
			{
			case UIImageOrientation.Up:                                        //EXIF = 1
				transform = CGAffineTransform.MakeIdentity();
				break;

			case UIImageOrientation.UpMirrored:                                //EXIF = 2
				transform = CGAffineTransform.MakeTranslation (imageSize.Width, 0f);
				transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
				break;

			case UIImageOrientation.Down:                                      //EXIF = 3
				transform = CGAffineTransform.MakeTranslation (imageSize.Width, imageSize.Height);
				transform = CGAffineTransform.Rotate(transform, (float)Math.PI);
				break;

			case UIImageOrientation.DownMirrored:                              //EXIF = 4
				transform = CGAffineTransform.MakeTranslation (0f, imageSize.Height);
				transform = CGAffineTransform.MakeScale(1.0f, -1.0f);
				break;

			case UIImageOrientation.LeftMirrored:                              //EXIF = 5
				boundHeight = bounds.Height;
				bounds.Height = bounds.Width;
				bounds.Width = boundHeight;
				transform = CGAffineTransform.MakeTranslation (imageSize.Height, imageSize.Width);
				transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
				transform = CGAffineTransform.Rotate(transform, 3.0f * (float)Math.PI/ 2.0f);
				break;

			case UIImageOrientation.Left:                                      //EXIF = 6
				boundHeight = bounds.Height;
				bounds.Height = bounds.Width;
				bounds.Width = boundHeight;
				transform = CGAffineTransform.MakeTranslation (0.0f, imageSize.Width);
				transform = CGAffineTransform.Rotate(transform, 3.0f * (float)Math.PI / 2.0f);
				break;

			case UIImageOrientation.RightMirrored:                             //EXIF = 7
				boundHeight = bounds.Height;
				bounds.Height = bounds.Width;
				bounds.Width = boundHeight;
				transform = CGAffineTransform.MakeScale(-1.0f, 1.0f);
				transform = CGAffineTransform.Rotate(transform, (float)Math.PI / 2.0f);
				break;

			case UIImageOrientation.Right:                                     //EXIF = 8
				boundHeight = bounds.Height;
				bounds.Height = bounds.Width;
				bounds.Width = boundHeight;
				transform = CGAffineTransform.MakeTranslation(imageSize.Height, 0.0f);
				transform = CGAffineTransform.Rotate(transform, (float)Math.PI  / 2.0f);
				break;

			default:
				throw new Exception("Invalid image orientation");
				break;
			}

			UIGraphics.BeginImageContext(bounds.Size);

			CGContext context = UIGraphics.GetCurrentContext ();

			if ( orient == UIImageOrientation.Right || orient == UIImageOrientation.Left )
			{
				context.ScaleCTM(-scaleRatio, scaleRatio);
				context.TranslateCTM(-height, 0);
			}
			else
			{
				context.ScaleCTM(scaleRatio, -scaleRatio);
				context.TranslateCTM(0, -height);
			}

			context.ConcatCTM(transform);
			context.DrawImage (new RectangleF (0, 0, width, height), imgRef);

			UIImage imageCopy = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();

			return imageCopy;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


