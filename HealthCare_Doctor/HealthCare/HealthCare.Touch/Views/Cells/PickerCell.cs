
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Cells
{
	public partial class PickerCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PickerCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("PickerCell");

		public PickerCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {
				this.AddBindings (new Dictionary<object, string> () {
					{ NameLabel, "Text ." },
				});
			});
		}

		public static PickerCell Create ()
		{
			return (PickerCell)Nib.Instantiate (null, null) [0];
		}
	}
}

