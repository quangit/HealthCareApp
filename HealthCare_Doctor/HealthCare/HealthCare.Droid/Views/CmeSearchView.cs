using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Core.ViewModels;
using HealthCare.Droid.Utilities;

namespace HealthCare.Droid.Views
{
    [Register("healthcare.droid.views.CmeSearchView"), Activity()]
    public class CmeSearchView : MvxActionBarActivity
    {
        protected override int LayoutResource => Resource.Layout.CmeSearchView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var searchBut = FindViewById<ImageButton>(Resource.Id.searchbutton);
            searchBut.Click += (sender, args) => 
            {
                var vm = ViewModel as CmeSearchViewModel;
                vm.SearchCommand.Execute(null);
            };
            var editSearch = FindViewById<EditText>(Resource.Id.editSearch);
            editSearch.KeyPress += EditSearch_KeyPress;
        }

        private void EditSearch_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            {
                var vm = ViewModel as CmeSearchViewModel;
                vm.SearchCommand.Execute(null);
                e.Handled = true;
            }
        }
    }
}