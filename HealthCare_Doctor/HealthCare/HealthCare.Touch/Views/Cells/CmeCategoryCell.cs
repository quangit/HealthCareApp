using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Core.Models;

namespace HealthCare.Touch.Views.Cells
{
	public partial class CmeCategoryCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString ("CmeCategoryCell");
		public static readonly UINib Nib;

		static CmeCategoryCell ()
		{
			Nib = UINib.FromName ("CmeCategoryCell", NSBundle.MainBundle);
		}

		public CmeCategoryCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{ClassNameLabel, "Text class_name"},
					{ClassDescTV, "Text short_description"},
				});


			});
		}
		public void SetDescTextView(){
			var dc = DataContext as CmeClass;
			if(dc != null){
				var htmlString = GetAttributedStringFromHtml(dc.short_description);
				ClassDescTV.AttributedText = htmlString;
				ClassDescTV.ContentOffset = CoreGraphics.CGPoint.Empty;
			}
		}
		public static NSAttributedString GetAttributedStringFromHtml(string html)
		{
			NSError error = null;
			NSAttributedString attributedString = new NSAttributedString (NSData.FromString(html), 
				new NSAttributedStringDocumentAttributes{ DocumentType = NSDocumentType.HTML, StringEncoding = NSStringEncoding.UTF8 }, 
				ref error);
			return attributedString;
		}


	}
}
