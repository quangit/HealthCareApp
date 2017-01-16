using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Cells
{
	public partial class CmeLibraryCell : MvxCollectionViewCell
	{
		public static readonly NSString Key = new NSString ("CmeLibraryCell");
		public static readonly UINib Nib;

		static CmeLibraryCell ()
		{
			Nib = UINib.FromName ("CmeLibraryCell", NSBundle.MainBundle);
		}

		public CmeLibraryCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{CategoryName, "Text ."},
				});
			});
		}
	}
}
