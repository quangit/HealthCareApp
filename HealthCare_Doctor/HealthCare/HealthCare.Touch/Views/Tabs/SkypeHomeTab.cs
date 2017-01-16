
using System;

using Foundation;
using UIKit;
using System.Collections.Generic;
using HealthCare.Touch.Controls;

namespace HealthCare.Touch.Views.Tabs
{
	public partial class SkypeHomeTab : BaseViewController
	{
		public SkypeHomeTab () : base ("SkypeHomeTab", null, true)
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
			var list = new List<string>(){"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday",};
			var vm = new PickerViewModel(){Items = list};
			TestButton.TouchUpInside += (sender, e) => {
				

				var multiPicker = new MultiPicker(vm);
				multiPicker.View.Frame = View.Bounds;
				ShowModal(multiPicker.View);
				multiPicker.Picked += (sender1, PickerItems) => {

					ModalBgView.RemoveFromSuperview();
				};
			};
		}
	}
}

