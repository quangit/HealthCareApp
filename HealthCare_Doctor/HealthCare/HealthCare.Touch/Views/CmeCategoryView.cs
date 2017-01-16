using System;

using UIKit;
using HealthCare.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Touch.Views.Cells;
using HealthCare.Touch.Controls;

namespace HealthCare.Touch.Views
{
	public partial class CmeCategoryView : BaseViewController
	{
		private CmeCategoryViewModel _vm{
			get {
				return ViewModel as CmeCategoryViewModel;
			}
		}

		public CmeCategoryView () : base ("CmeCategoryView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TableView.RowHeight = 200;
			var source = new CmeCategoryTableViewSource(TableView, CmeCategoryCell.Key, CmeCategoryCell.Key);
			TableView.Source = source;
			this.AddBindings(new Dictionary<object, string>() {
				{TitleLabel, "Text CmeCategory.Name"},
				{source,"ItemsSource CmeCategory.CmeClasses; SelectionChangedCommand CmeClassCommand"}
			});
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


