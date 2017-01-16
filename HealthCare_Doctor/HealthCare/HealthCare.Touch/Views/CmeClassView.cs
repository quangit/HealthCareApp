using System;

using UIKit;
using HealthCare.Core.ViewModels;
using Foundation;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views
{
	public partial class CmeClassView : BaseViewController
	{
		private CmeClassViewModel _vm{
			get {
				return ViewModel as CmeClassViewModel;
			}
		}

		private UIActivityIndicatorView _indicator;
		public CmeClassView () : base ("CmeClassView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			WebView.LoadHtmlString (_vm.CmeClass.full_description, null);
			HomeButton.Clicked += (sender, e) => WebView.LoadHtmlString (_vm.CmeClass.full_description, null);
			BackButton.Clicked += (sender, e) => {
				if (WebView.CanGoBack)
					WebView.GoBack ();
			};
			ForwardButton.Clicked += (sender, e) => {
				if (WebView.CanGoForward)
					WebView.GoForward();
			};

			WebView.ShouldStartLoad += WebViewShouldStart;


			_indicator = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.White);
			_indicator.StartAnimating ();
			SetRightBarButtonItems (new []{ new UIBarButtonItem (_indicator) });
			WebView.LoadFinished += (sender, e) => _indicator.StopAnimating();

			this.AddBindings(new Dictionary<object, string>() {
				{TitleLabel, "Text CmeClass.class_name"},
			});
			//WebView.LoadRequest(new Foundation.NSUrlRequest(new NSUrl(_vm.CmeClass.current_url)));
		}

		private bool WebViewShouldStart(UIWebView webview, NSUrlRequest request, UIWebViewNavigationType navigationType){
			BackButton.Enabled = WebView.CanGoBack;
			ForwardButton.Enabled = WebView.CanGoForward;
			_indicator.StartAnimating ();
			return true;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.

		}
	}
}


