using System;
using Foundation;
using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Touch.Views.Cells;
using Cirrious.MvvmCross.Binding.BindingContext;
using HealthCare.Core.ViewModels;
using System.Collections.Generic;
using HealthCare.Touch.Controls;

namespace HealthCare.Touch.Views.Tabs
{
	public partial class CheckUpHomeTab : BaseViewController	
	{
		private UIRefreshControl _refreshControl;
		public CheckUpHomeTab () : base ("CheckUpHomeTab", null, true)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		private HomeViewModel _vm {
			get {
				return base.ViewModel as HomeViewModel;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			var source = new LoadMoreTableViewSource(TableView, CheckUpCell.Key, CheckUpCell.Key);
			source.LoadMore += (sender, e) => _vm.LoadCheckups ();
			TableView.Source = source;
			TableView.RowHeight = 150;
			var set = this.CreateBindingSet<CheckUpHomeTab, HomeViewModel>();
			set.Bind(source).To(vm => vm.Checkups);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.CheckupViewCommand);
			set.Apply();
			TitleLabel.Text = Core.Resources.AppResources.Checkups_Title;
			this.AddBindings(new Dictionary<object, string>() {
				{EmptyLabel, "Visible Checkups,Converter=ListVis; Text [Checkups_Empty]"},
			});

			var tableVC = new UITableViewController();
			tableVC.TableView = TableView;
			_refreshControl = new UIRefreshControl();
			_refreshControl.ValueChanged += Refresh;
			tableVC.RefreshControl = _refreshControl;
		}

		public async void Refresh(object sender, EventArgs e)
		{
			await ((HomeViewModel)ViewModel).LoadCheckups(true);
			_refreshControl.EndRefreshing();
		}
	}
}

