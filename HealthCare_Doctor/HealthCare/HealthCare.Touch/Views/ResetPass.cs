using System;
using System.Drawing;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views
{
	public partial class ResetPass : BaseViewController
	{
		public ResetPass () : base ("ResetPass", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.AddBindings (new Dictionary<object, string> () {
				{ EmailTF, "Text Email,Mode=TwoWay" },
				{ LoginButton, "Title [SignIn_Login]; TouchUpInside BackCommand" },
				{ ResetButton, "Title [ResetPass_Reset]; TouchUpInside ResetPassCommand" },

			});

			TitleLabel.Text = Core.Resources.AppResources.LoginView_ResetPass;
		}
	}
}

