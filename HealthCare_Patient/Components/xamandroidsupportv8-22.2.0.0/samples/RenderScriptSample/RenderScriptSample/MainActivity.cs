using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

using Android.Support.V8.Renderscript;


namespace RenderScriptSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private ImageView _imageView;
		private SeekBar _seekbar;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
			_imageView = FindViewById<ImageView> (Resource.Id.originalImageView);

			_seekbar = FindViewById<SeekBar> (Resource.Id.seekBar1);
			_seekbar.StopTrackingTouch += BlurImageHandler;
		}


		private void BlurImageHandler (object sender, SeekBar.StopTrackingTouchEventArgs e)
		{
			int radius = e.SeekBar.Progress;
			if (radius == 0) {
				// We don't want to blur, so just load the un-altered image.
				_imageView.SetImageResource (Resource.Drawable.dog_and_monkeys);
			} else {
				DisplayBlurredImage (radius);
			}

		}

		private Bitmap CreateBlurredImage (int radius)
		{
			// Load a clean bitmap and work from that.
			Bitmap originalBitmap = BitmapFactory.DecodeResource (Resources, Resource.Drawable.dog_and_monkeys);

			// Create another bitmap that will hold the results of the filter.
			Bitmap blurredBitmap;
			blurredBitmap = Bitmap.CreateBitmap (originalBitmap);

			// Create the Renderscript instance that will do the work.
			RenderScript rs = RenderScript.Create (this);

			// Allocate memory for Renderscript to work with
			Allocation input = Allocation.CreateFromBitmap (rs, originalBitmap, Allocation.MipmapControl.MipmapFull, Allocation.UsageScript);
			Allocation output = Allocation.CreateTyped (rs, input.Type);

			// Load up an instance of the specific script that we want to use.
			ScriptIntrinsicBlur script = ScriptIntrinsicBlur.Create (rs, Element.U8_4 (rs));
			script.SetInput (input);

			// Set the blur radius
			script.SetRadius (radius);

			// Start Renderscript working.
			script.ForEach (output);

			// Copy the output to the blurred bitmap
			output.CopyTo (blurredBitmap);

			return blurredBitmap;
		}

		private void DisplayBlurredImage (int radius)
		{
			// Disable the event handler and the Seekbar to prevent this from 
			// happening multiple times.
			_seekbar.StopTrackingTouch -= BlurImageHandler;
			_seekbar.Enabled = false;

			// Do the processing in a background thread.
			Task.Factory.StartNew (() => {
				Bitmap bmp = CreateBlurredImage (radius);
				return bmp;
			})
				.ContinueWith (task => {
					// Processing is done - display the image and re-enable all of our
					// event handlers and widgets. This work is done on the UI thread.
					Bitmap bmp = task.Result;
					_imageView.SetImageBitmap (bmp);
					_seekbar.StopTrackingTouch += BlurImageHandler;
					_seekbar.Enabled = true;
				}, TaskScheduler.FromCurrentSynchronizationContext ());
		}
	}
}


