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
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Utils;
using HealthCare.Core.ViewModels;
using Android.Support.V4.Widget;
using HealthCare.Droid.Controls;
using HealthCare.Droid.Views.Utilities;

namespace HealthCare.Droid.Views.Fragments
{
    [Register("healthcare.droid.views.fragments.WeekTopicFragment")]
    public class WeekTopicFragment : MvxFragment, AbsListView.IOnScrollListener
    {
        private View _view;
        private HomeViewModel _vm;
        private Action TopicItemClick;
        private SwipeRefreshLayout _swipeContainer;
        private SwipeRefreshHintLayout _swipeRefreshHintLayout;
        private RadioGroup _filterGroup;

        private MvxListView _topicsList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            _view = this.BindingInflate(Resource.Layout.WeekTopicFragment, null);
            _vm = (HomeViewModel)ViewModel;
            _swipeContainer = _view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container);
            _swipeContainer.Refresh += swipeContainer_Refresh;
            //var skypeBut = _view.FindViewById<Button>(Resource.Id.skypeBut);

            //skypeBut.Click += (sender, args) =>
            //{
            //    var uri = Android.Net.Uri.Parse(_vm.SelectedTopic.SkypeForBusinessUrl.ToString());
            //    var intent = new Intent(Intent.ActionView, uri);
            //    StartActivity(intent);
            //};

            _filterGroup = _view.FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            _filterGroup.CheckedChange += _filterGroup_CheckedChange;

            _topicsList = _view.FindViewById<MvxListView>(Resource.Id.TopiclistView);
            var adapter = new LoadMoreMvxAdapter(_view.Context, BindingContext as IMvxAndroidBindingContext);
            adapter.LoadMore += (sender, args) => _vm.LoadTopics();
            _topicsList.Adapter = adapter;
            _topicsList.SetOnScrollListener(this);
            TopicItemClick = async () =>
            {
                var selected = _vm.SelectedTopic;
                if (!string.IsNullOrEmpty(selected.SkypeForBusinessUrl) && selected.IsOnline)
                {
                    if (selected.IsOnlineNow)
                    {                       
                        var uri = Android.Net.Uri.Parse(_vm.SelectedTopic.SkypeForBusinessUrl);
                        var intent = new Intent(Intent.ActionView, uri);
                        StartActivity(intent);
                    }
					else
					{
						if(selected.StartDateTime > Util.DateTimeToLong(DateTime.UtcNow))
							await Mvx.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeakTopics_NotNow, AppResources.Warning);
						else
							await Mvx.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeekTopic_Happened, AppResources.Warning);
					}
                }
                else if (selected.Location != null)
                {
                    //var vm = this.DataContext as HomeViewModel;
                    if (_vm != null)
                    {
                        var b = await _vm.GoToMap(_vm.SelectedTopic);
                        if (!b)
                            return;
                    }
                    string loc = selected.LocationStr;
                    var geoUri =
                        Android.Net.Uri.Parse("geo:0,0?q=" + loc + "(" + (string.IsNullOrEmpty(_vm.SelectedTopic.Address)
                            ? _vm.SelectedTopic.Title
                            : _vm.SelectedTopic.Address) + ")");
                    var mapIntent = new Intent(Intent.ActionView, geoUri);
                    StartActivity(mapIntent);
                }
            };
            _vm.TopicSelected = TopicItemClick;
            return _view;
        }



       


        private void _filterGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            var radio1 = _view.FindViewById<RadioButton>(Resource.Id.radio0);
            var radio2 = _view.FindViewById<RadioButton>(Resource.Id.radio1);
            var radio3 = _view.FindViewById<RadioButton>(Resource.Id.radio2);
            if (e.CheckedId == radio1.Id)
                _vm.FilterWeekTopic(0);
            else if (e.CheckedId == radio2.Id)
                _vm.FilterWeekTopic(1);
            else
                _vm.FilterWeekTopic(2);
        }

        private async void swipeContainer_Refresh(object sender, EventArgs e)
        {
            await ((HomeViewModel)ViewModel).LoadTopics(true);
            _swipeContainer.Refreshing = false;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            int topRowVerticalPosition =
       (_topicsList == null || _topicsList.ChildCount == 0) ?
         0 : _topicsList.GetChildAt(0).Top;
            _swipeContainer.Enabled = (topRowVerticalPosition >= 0);
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
           
        }
    }
}