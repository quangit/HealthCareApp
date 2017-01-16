using System;

using UIKit;
using HealthCare.Touch.Controls;
using HealthCare.Touch.Views.Cells;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views
{
	public partial class CmeSearchView : BaseViewController
	{
		public CmeSearchView () : base ("CmeSearchView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			TableView.RowHeight = 200;
			var source = new CmeCategoryTableViewSource(TableView, CmeCategoryCell.Key, CmeCategoryCell.Key);
			TableView.Source = source;
			this.AddBindings(new Dictionary<object, string>() {
				{EmptyLabel, "Visible CmeClasses,Converter=ListVis; Text [CmeSearch_Empty]"},
				{SearchTF, "Text SearchTerm; Placeholder [CmeSearch_Hint]"},
				{source,"ItemsSource CmeClasses; SelectionChangedCommand CmeClassCommand"},
				{SearchButton,"TouchUpInside SearchCommand"}
			});
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


