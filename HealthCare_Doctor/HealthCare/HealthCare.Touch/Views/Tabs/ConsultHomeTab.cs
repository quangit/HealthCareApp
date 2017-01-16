
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Touch.Views.Cells;
using Cirrious.MvvmCross.Binding.BindingContext;
using HealthCare.Core.ViewModels;
using System.Collections.Generic;
using HealthCare.Touch.Controls;

namespace HealthCare.Touch.Views.Tabs
{
	public partial class ConsultHomeTab : BaseViewController
	{
		private UIRefreshControl _refreshControl;
		public ConsultHomeTab () : base ("ConsultHomeTab", null,true)
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
			var source = new LoadMoreTableViewSource(TableView, ConsultCell.Key, ConsultCell.Key);
			source.LoadMore += (sender, e) => _vm.LoadSupportRequests ();
			TableView.Source = source;
			TableView.RowHeight = 150;
			var set = this.CreateBindingSet<ConsultHomeTab, HomeViewModel>();
			set.Bind(source).To(vm => vm.ConsultRequests);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.ShowConsultCommand);
			set.Apply();
		    
			TitleLabel.Text = Core.Resources.AppResources.Consult_Title;
			this.AddBindings(new Dictionary<object, string>() {
				{EmptyLabel, "Visible ConsultRequests,Converter=ListVis; Text [Consult_Empty]"},
			});

			var tableVC = new UITableViewController();
			tableVC.TableView = TableView;
			_refreshControl = new UIRefreshControl();
			_refreshControl.ValueChanged += Refresh;
			tableVC.RefreshControl = _refreshControl;
		}

		public async void Refresh(object sender, EventArgs e)
		{
			await ((HomeViewModel)ViewModel).LoadSupportRequests(true);
			_refreshControl.EndRefreshing();
		}
	}
}



