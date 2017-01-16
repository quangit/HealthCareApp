using System;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using UIKit;
using Foundation;
using System.Diagnostics;


namespace HealthCare.Touch.Controls
{
	public class LoadMoreTableViewSource : MvxSimpleTableViewSource
	{
		public event EventHandler LoadMore;

		public LoadMoreTableViewSource (IntPtr handle) : base(handle)
		{
		}

		public LoadMoreTableViewSource (UITableView tableView, string nibName, string cellIdentifier = null, NSBundle bundle = null) : base(tableView, nibName, cellIdentifier, bundle)
		{
		}

		public LoadMoreTableViewSource(UITableView tableView, Type cellType, string cellIdentifier = null)
			: base(tableView, cellType, cellIdentifier)
		{
		}

		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
		    if (ItemsSource.GetPosition(item) == ItemsSource.Count()-1)
		    {
				if (LoadMore != null)
				{
					LoadMore(this, null);
				}
		    }

			//Debug.WriteLine (ItemsSource.GetPosition (item) + " == " + (ItemsSource.Count ()-1));
            return base.GetOrCreateCellFor (tableView, indexPath, item);
		}
	}
}

