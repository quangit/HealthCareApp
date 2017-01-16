using System;

using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using CoreGraphics;
using System.Collections.Generic;

namespace HealthCare.Touch.Views
{
	public partial class ImageZoomView : BaseViewController
	{
		public ImageZoomView () : base ("ImageZoomView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		//private MvxImageViewLoader _descloader;
		private MvxImageView DescImage;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			if (DescImage == null) {
				DescImage = new MvxImageView ((CGRect)View.Bounds,ImageLoaded);
				DescImage.ContentMode = UIViewContentMode.ScaleAspectFit;
			}
			//_descloader = new MvxImageViewLoader (() => DescImage,ImageLoaded);
			this.AddBindings(new Dictionary<object, string>() {
				{DescImage, "ImageUrl Request.LandscapeImage"},
			});

			ImageScrollView.ViewForZoomingInScrollView += (UIScrollView sv) => { return DescImage; };
			UITapGestureRecognizer doubletap = new UITapGestureRecognizer(OnDoubleTap) {
				NumberOfTapsRequired = 2 // double tap
			};
			ImageScrollView.AddGestureRecognizer(doubletap);
			ImageScrollView.AddSubview (DescImage);
			//ImageScrollView.ContentMod
		}

		private void ImageLoaded(){
			//ImageScrollView.ContentSize = DescImage.Image.Size;
			//ImageScrollView.AddSubview (DescImage);

		}


		public override void ViewDidAppear (bool animated)
		{
			//DescImage.Frame = new RectangleF(PointF.Empty,View.Frame.Size);
			base.ViewDidAppear (animated);
			DescImage.Frame = View.Bounds;
			var scaleWidth = ImageScrollView.Frame.Size.Width / DescImage.Frame.Width;
			var scaleHeight = ImageScrollView.Frame.Size.Height / DescImage.Frame.Height;
			var minscale = (nfloat) Math.Min (scaleWidth, scaleHeight);
			ImageScrollView.MaximumZoomScale = 2f;
			ImageScrollView.MinimumZoomScale = minscale;
			ImageScrollView.ZoomScale = minscale;
			//ImageScrollView.ContentSize = DescImage.Image.Size;

		}

		private void OnDoubleTap (UIGestureRecognizer gesture) {
			//Debug.WriteLine ("ImageScrollView.ZoomScale: " + ImageScrollView.ZoomScale);
			if ((float)ImageScrollView.ZoomScale > 1)
				ImageScrollView.SetZoomScale((nfloat)0.25f, true);
			else
				ImageScrollView.SetZoomScale((nfloat)2f, true);


		}
	}
}


