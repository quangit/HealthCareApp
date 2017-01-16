using System;

using UIKit;
using Foundation;
using HealthCare.Core.ViewModels;

namespace HealthCare.Touch.Views
{
	public partial class ChbaseHomeTab : BaseViewController
	{
		private HomeViewModel _vm => ViewModel as HomeViewModel;
		public ChbaseHomeTab () : base ("ChbaseHomeTab", null,true)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.


			WebView.LoadRequest(new NSUrlRequest(new NSUrl(_vm.CHBASE_URL)));
			//WebView.LoadFinished += (sender, e) => indicator.StopAnimating();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


