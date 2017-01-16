
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Touch.Views.Cells;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;

namespace HealthCare.Touch.Controls
{
	public class PickerViewModel : MvxViewModel{
		private List<string> _items;
		public List<string> Items
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }
		}

		private List<int> _selectedItems = new List<int>();
		public List<int> SelectedItems
		{
			get { return _selectedItems; }
			set { SetProperty(ref _selectedItems, value); }
		}

		public void AddSelectedItem(int index){
			if (!SelectedItems.Contains(index)) {
				SelectedItems.Add (index);
			}
		}

		public void RemoveSelectedItem(int index){
			if (SelectedItems.Contains(index)) {
				SelectedItems.Remove (index);
			}
		}

	}


	public partial class MultiPicker : MvxViewController
	{
		
		public new PickerViewModel ViewModel{
			get { return base.ViewModel as PickerViewModel;}
		}
		public event EventHandler<List<int>> Picked;

		public MultiPicker (PickerViewModel vm) : base ("MultiPicker", null)
		{ 
			base.ViewModel = vm;// new PickerViewModel (){ Items = items };
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

	    public override void ViewDidUnload()
	    {
	        base.ViewDidUnload();
	    }

	    public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			TableView.RowHeight = 50;

			var source = new MvxSimpleTableViewSource(TableView, PickerCell.Key, PickerCell.Key);
			TableView.Source = source;

			this.AddBindings (new Dictionary<object, string> () {
				{ source,"ItemsSource Items" },
			});

			SubmitButton.TouchUpInside += (sender, e) => {
				if(Picked != null)
					Picked(null,ViewModel.SelectedItems);
			};
			TableView.WeakDelegate = new PickerTableViewDelegate (ViewModel,TableView);
			TableView.ReloadData();
            
			//foreach(var index in ViewModel.SelectedItems)
			//	TableView.SelectRow ( NSIndexPath.FromRowSection(index,0),false,UITableViewScrollPosition.None);
		
		}
	}

	public class PickerTableViewDelegate : UITableViewDelegate{
		private PickerViewModel _vm;
		private UITableView _tableView;
		public PickerTableViewDelegate(PickerViewModel vm,UITableView tableView){
			_vm = vm;
			_tableView = tableView;
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItems")
            {
                LoadSelected();
            }
        }

        private void LoadSelected(){
			foreach (var index in _vm.SelectedItems) {
				var cell = _tableView.CellAt ( NSIndexPath.FromRowSection(index,0));
			    if (cell != null)
			    {
			        cell.Accessory = UITableViewCellAccessory.Checkmark;
                    this._tableView.SelectRow(NSIndexPath.FromRowSection(index, 0),false, UITableViewScrollPosition.None);
			    }
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt (indexPath);
			cell.Accessory = UITableViewCellAccessory.Checkmark;
			_vm.AddSelectedItem (indexPath.Row);
		}

		public override void RowDeselected (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt (indexPath);
			cell.Accessory = UITableViewCellAccessory.None;
			_vm.RemoveSelectedItem (indexPath.Row);
		}
	}
}

