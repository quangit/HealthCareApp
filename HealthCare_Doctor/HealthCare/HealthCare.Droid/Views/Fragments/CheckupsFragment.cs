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
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using Android.Support.V4.Widget;
using HealthCare.Droid.Controls;
using HealthCare.Droid.Views.Utilities;

namespace HealthCare.Droid.Views.Fragments
{
     [Register("healthcare.droid.views.fragments.CheckupsFragment")]
    public class CheckupsFragment : MvxFragment, AbsListView.IOnScrollListener
    {
        private View _view;
        private HomeViewModel _vm;
        private SwipeRefreshLayout _swipeContainer;
        private MvxListView _checkupsList;



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            _view = this.BindingInflate(Resource.Layout.CheckupsFragment, null);
            _vm = (HomeViewModel)ViewModel;
            var itemclick = _view.FindViewById<MvxListView>(Resource.Id.checkupsListView);
            itemclick.ItemSelected += (sender, args) => { };
            _swipeContainer = _view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container);
            _swipeContainer.Refresh += swipeContainer_Refresh;
            _checkupsList = _view.FindViewById<MvxListView>(Resource.Id.checkupsListView);
            _checkupsList.SetOnScrollListener(this);
            var adapter = new LoadMoreMvxAdapter(_view.Context, BindingContext as IMvxAndroidBindingContext);
            adapter.LoadMore += (sender, args) => _vm.LoadCheckups();
            _checkupsList.Adapter = adapter;
            return _view;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            int topRowVerticalPosition =
      (_checkupsList == null || _checkupsList.ChildCount == 0) ?
        0 : _checkupsList.GetChildAt(0).Top;
            _swipeContainer.Enabled = (topRowVerticalPosition >= 0);
        }

        private async void swipeContainer_Refresh(object sender, EventArgs e)
        {
            await _vm.LoadCheckups(true);
            _swipeContainer.Refreshing = false;
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {

        }



    }


}