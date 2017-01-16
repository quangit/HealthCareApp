using System;
using System.Diagnostics;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;
using HealthCare.Core.ViewModels;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;

namespace HealthCare.Touch.Utilities
{
	public class DeleteTableViewSource : MvxSimpleTableViewSource
	{
		public HomeViewModel ViewModel;
		public DeleteTableViewSource(IntPtr handle) : base(handle)
		{
		}

		public DeleteTableViewSource(UITableView tableView, string nibName, string cellIdentifier = null, NSBundle bundle = null) : base(tableView, nibName, cellIdentifier, bundle)
		{
		}

		public DeleteTableViewSource(UITableView tableView, Type cellType, string cellIdentifier = null)
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
			return (nfloat)100;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				var schedule = (Schedule)GetItemAt(indexPath);
				(ViewModel).DeleteScheduleCommand.Execute(schedule);
			}
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
		{
			return AppResources.Button_Delete;
		}
	}
}



