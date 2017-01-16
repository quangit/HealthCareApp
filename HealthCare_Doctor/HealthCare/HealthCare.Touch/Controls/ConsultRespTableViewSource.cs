using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using UIKit;
using HealthCare.Core.ViewModels;
using Foundation;
using HealthCare.Touch.Views.Cells;

namespace HealthCare.Touch.Utilities
{

	public class ConsultRespTableViewSource : MvxSimpleTableViewSource
	{
		public ConsultViewModel ViewModel;
		public ConsultRespTableViewSource(IntPtr handle) : base(handle)
		{
		}

		public ConsultRespTableViewSource(UITableView tableView, string nibName, string cellIdentifier = null, NSBundle bundle = null) : base(tableView, nibName, cellIdentifier, bundle)
		{
		}

		public ConsultRespTableViewSource(UITableView tableView, Type cellType, string cellIdentifier = null)
			: base(tableView, cellType, cellIdentifier)
		{
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return (nfloat)UITableView.AutomaticDimension;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			var datum = ViewModel.Responses[indexPath.Row];
			NSString nsString = new NSString(datum.comment);
			UIStringAttributes attribs = new UIStringAttributes { Font = UIFont.SystemFontOfSize(14) };
			//var size = nsString.StringSize(UIFont.SystemFontOfSize(14) ,tableView.Frame.Width - 20, UILineBreakMode.CharacterWrap);

			var constraint = new CoreGraphics.CGSize (tableView.Frame.Width, 20000f);
			var context = new NSStringDrawingContext ();
			var boundingBox = nsString.GetBoundingRect (constraint, NSStringDrawingOptions.UsesLineFragmentOrigin, attribs, context).Size;

			return 150 + boundingBox.Height;
		}

	}
}

