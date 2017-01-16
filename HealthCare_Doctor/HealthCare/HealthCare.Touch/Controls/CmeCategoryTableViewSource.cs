using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using UIKit;
using Foundation;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using HealthCare.Touch.Views.Cells;

namespace HealthCare.Touch.Controls
{
	public class CmeCategoryTableViewSource : MvxSimpleTableViewSource
	{
		public CmeCategoryTableViewSource (IntPtr handle) : base(handle)
		{
		}

		public CmeCategoryTableViewSource (UITableView tableView, string nibName, string cellIdentifier = null, NSBundle bundle = null) : base(tableView, nibName, cellIdentifier, bundle)
		{
		}

		public CmeCategoryTableViewSource(UITableView tableView, Type cellType, string cellIdentifier = null)
			: base(tableView, cellType, cellIdentifier)
		{
		}

		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = base.GetOrCreateCellFor (tableView, indexPath, item) as CmeCategoryCell;
			cell.DataContext = item;
			cell.SetDescTextView ();

			//Debug.WriteLine (ItemsSource.GetPosition (item) + " == " + (ItemsSource.Count ()-1));
			return cell;
		}
	}
}

