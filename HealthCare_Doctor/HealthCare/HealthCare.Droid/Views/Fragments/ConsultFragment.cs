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
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using HealthCare.Core.ViewModels;
using Android.Support.V4.Widget;
using HealthCare.Droid.Views.Utilities;
using Cirrious.MvvmCross.Binding.Droid.Views;
using HealthCare.Droid.Controls;

namespace HealthCare.Droid.Views.Fragments
{
    [Register("healthcare.droid.views.fragments.ConsultFragment")]
    public class ConsultFragment : MvxFragment, AbsListView.IOnScrollListener
    {
        private View _view;
        private HomeViewModel _vm;
        private SwipeRefreshLayout _swipeContainer;
        private SwipeRefreshHintLayout _swipeRefreshHintLayout;
        private MvxListView _consultList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
       
            _view = this.BindingInflate(Resource.Layout.ConsultFragment, null);
            _vm = (HomeViewModel)ViewModel;
            _swipeContainer = _view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container);
            _swipeContainer.Refresh += swipeContainer_Refresh;
            _consultList = _view.FindViewById<MvxListView>(Resource.Id.consultList);
            _consultList.SetOnScrollListener(this);
            var adapter = new LoadMoreMvxAdapter(_view.Context, BindingContext as IMvxAndroidBindingContext);
            adapter.LoadMore += (sender, args) => _vm.LoadSupportRequests();
            _consultList.Adapter = adapter;
            return _view;
        }


        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            int topRowVerticalPosition =
      (_consultList == null || _consultList.ChildCount == 0) ?
        0 : _consultList.GetChildAt(0).Top;
            _swipeContainer.Enabled = (topRowVerticalPosition >= 0);
        }

        private async void swipeContainer_Refresh(object sender, EventArgs e)
        {
            await ((HomeViewModel)ViewModel).LoadSupportRequests(true);
            _swipeContainer.Refreshing = false;
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
            
        }
    }
}