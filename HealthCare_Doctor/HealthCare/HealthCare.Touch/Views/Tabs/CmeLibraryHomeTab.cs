using System;

using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Core.Resources;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Touch.Views.Cells;
using HealthCare.Core.ViewModels;
using System.Diagnostics;

namespace HealthCare.Touch.Views.Tabs
{
    public partial class CmeLibraryHomeTab : BaseViewController
    {

        private HomeViewModel _vm
        {
            get
            {
                return base.ViewModel as HomeViewModel;
            }
        }

        private UIRefreshControl _refreshControl;
        public CmeLibraryHomeTab() : base("CmeLibraryHomeTab", null, true)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            CategoryCollection.RegisterNibForCell(CmeLibraryCell.Nib, CmeLibraryCell.Key);
            var source = new MvxCollectionViewSource(CategoryCollection, CmeLibraryCell.Key);
            CategoryCollection.Source = source;
            this.AddBindings(new Dictionary<object, string>() {
                {EmptyLabel, "Visible CmeCategories,Converter=ListVis; Text [Cme_CategoryEmpty]"},
                {source, "ItemsSource CmeCategories; SelectionChangedCommand CmeCommand"},
                {CategorySearchBar,"Text CmeCategoriesSearch"},
            });

            ClassSearchButton.Clicked += (sender, e) => _vm.ShowCmeSearch();
            _refreshControl = new UIRefreshControl();
            _refreshControl.ValueChanged += Refresh;
            CategoryCollection.AddSubview(_refreshControl);

            CategoryCollection.CollectionViewLayout = new CustomLayout();

        }

        public async void Refresh(object sender, EventArgs e)
        {
            await ((HomeViewModel)ViewModel).LoadCmeCategories(true);
            _refreshControl.EndRefreshing();
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.N

        }
    }

    public class CustomLayout : UICollectionViewFlowLayout
    {

        private int _numberOfItemsPerRow = 3;
        public override void PrepareLayout()
        {
            base.PrepareLayout();


            var newItemSize = ItemSize;


            MinimumInteritemSpacing = 2;
            // Calculate the sum of the spacing between cells

            _numberOfItemsPerRow = (int)Math.Max(Math.Floor(CollectionView.Bounds.Size.Width / 150), 3);
            var totalSpacing = MinimumInteritemSpacing * (_numberOfItemsPerRow - 1.0);

            // Calculate how wide items should be

            var totalItemWidths = (CollectionView.Bounds.Size.Width - totalSpacing);

            // Use the aspect ratio of the current item size to determine how tall the items should be

            newItemSize.Width = (nfloat)(totalItemWidths / _numberOfItemsPerRow);

            // Use the aspect ratio of the current item size to determine how tall the items should be
            if (ItemSize.Height > 0)
            {
                var itemAspectRatio = ItemSize.Width / ItemSize.Height;
                newItemSize.Height = newItemSize.Width / itemAspectRatio;
            }

            //			Debug.WriteLine ("totalSpacing: " + totalSpacing);
            //			Debug.WriteLine ("Screen Width: " + CollectionView.Bounds.Size.Width);
            //			Debug.WriteLine ("_numberOfItemsPerRow: " + _numberOfItemsPerRow);
            //			Debug.WriteLine ("newItemSize.Width: " + newItemSize.Width); 

            // Set the new item size
            ItemSize = newItemSize;

        }
    }

}


