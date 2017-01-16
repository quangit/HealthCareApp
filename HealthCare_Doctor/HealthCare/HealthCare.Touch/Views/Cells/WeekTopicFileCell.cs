using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Cells
{
	public partial class WeekTopicFileCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString ("WeekTopicFileCell");
		public static readonly UINib Nib;

		static WeekTopicFileCell ()
		{
			Nib = UINib.FromName ("WeekTopicFileCell", NSBundle.MainBundle);
		}

		public WeekTopicFileCell (IntPtr handle) : base (handle)
		{

			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{FileNameLabel, "Text FileName"},
				});
			});
		}
	}
}
