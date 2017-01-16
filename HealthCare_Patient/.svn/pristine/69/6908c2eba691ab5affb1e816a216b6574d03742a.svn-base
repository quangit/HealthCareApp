using System;
using Xamarin.Forms;
using HealthCare;
using HealthCare.iOS;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;
using HealthCare.Controls;


[assembly: ExportRenderer (typeof(EntryLogin), typeof(EntryLoginRenderer))]
namespace HealthCare.iOS
{
	public class EntryLoginRenderer:EntryRenderer
	{

		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);
            var FormsEntry = (EntryLogin)e.NewElement;
			if (FormsEntry != null) {
				Control.BorderStyle = UIKit.UITextBorderStyle.None;
				Control.AttributedPlaceholder = new NSAttributedString (
					Control.Placeholder,
                    foregroundColor: new UIKit.UIColor ((float)FormsEntry.PlaceHolderColor.R, 
                        (float)FormsEntry.PlaceHolderColor.B, (float)FormsEntry.PlaceHolderColor.G, 
                        (float)FormsEntry.PlaceHolderColor.A)
				);
                Control.Font = UIFont.SystemFontOfSize((int)FormsEntry.FontSize==0? 13: (float)FormsEntry.FontSize);
            }
		}
	}
}

