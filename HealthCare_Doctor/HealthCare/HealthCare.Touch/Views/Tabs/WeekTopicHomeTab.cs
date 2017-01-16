
using System;
using Cirrious.CrossCore;
using Foundation;
using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Touch.Views.Cells;
using Cirrious.MvvmCross.Binding.BindingContext;
using HealthCare.Core.ViewModels;
using HealthCare.Core.Models;
using LumiaLoyalty.Touch.Utilities;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Utils;
using System.Collections.Generic;
using HealthCare.Touch.Controls;

namespace HealthCare.Touch.Views.Tabs
{
	public partial class WeekTopicHomeTab : BaseViewController
	{
		private HomeViewModel _vm
		{
			get
			{
				return base.ViewModel as HomeViewModel;
			}
		}

		private UIRefreshControl _refreshControl;
		public WeekTopicHomeTab () : base ("WeekTopicHomeTab", null, true)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			var source = new LoadMoreTableViewSource(TableView, WeekTopicCell.Key, WeekTopicCell.Key);
			source.LoadMore += (sender, e) => _vm.LoadTopics ();
			TableView.Source = source;
			TableView.RowHeight = 100;
			var set = this.CreateBindingSet<WeekTopicHomeTab, HomeViewModel>();
			set.Bind(source).To(vm => vm.WeekTopics);
			//set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.MyDealCommand);
			set.Apply();
			TitleLabel.Text = Core.Resources.AppResources.WeakTopics_Title;
			this.AddBindings(new Dictionary<object, string>() {
				{EmptyLabel, "Visible WeekTopics,Converter=ListVis; Text [WeekTopic_Empty]"},
			});
			source.SelectedItemChanged += async (sender, e) => {
				var topic = source.SelectedItem as Topic;

				if((topic!=null)){
					if (!string.IsNullOrEmpty(topic.SkypeForBusinessUrl) && topic.IsOnline)
					{
					    if (topic.IsOnlineNow)
					    {
//#if DEBUG
//					        UIApplication.SharedApplication.OpenUrl(
//					            new NSUrl("https://meet.lync.com/stdntpartners/khuong.nguyen-trung-dang/8J6PBDW7"));
//#else
							UIApplication.SharedApplication.OpenUrl(new NSUrl(topic.SkypeForBusinessUrl));
//							#endif
					    }
					    else
					    {
							if(topic.StartDateTime > Util.DateTimeToLong(DateTime.UtcNow))
                            	await Mvx.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeakTopics_NotNow, AppResources.Warning);
							else
								await Mvx.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeekTopic_Happened, AppResources.Warning);
                        }
					}
					else if (topic.Location != null)
					{
					    var vm = this.ViewModel as HomeViewModel;
                        if (vm != null)
                        {
                            var b = await vm.GoToMap(topic);
                            if (!b)
                                return;
                        }
                        string url = "http://maps.apple.com/?q="+topic.LocationStr+"&z=15";
						url = url.Replace (" ", "%20");
						if (UIApplication.SharedApplication.CanOpenUrl (new NSUrl (url))) {
							UIApplication.SharedApplication.OpenUrl (new NSUrl (url));
						} else {
							new UIAlertView ("Error", AppResources.WeekTopic_MapNotSupported, null, "Ok").Show ();
						}

					}
				}
			};



			var tableVC = new UITableViewController();
			tableVC.TableView = TableView;
			_refreshControl = new UIRefreshControl();
			_refreshControl.ValueChanged += Refresh;
			tableVC.RefreshControl = _refreshControl;


			WeekTopicFilterSegControl.SetTitle (AppResources.WeekTopic_All, 0);
			WeekTopicFilterSegControl.ValueChanged += (sender, e) => {
				_vm.FilterWeekTopic((int)WeekTopicFilterSegControl.SelectedSegment);
			};
		}


			public async void Refresh(object sender, EventArgs e)
			{
				await ((HomeViewModel)ViewModel).LoadTopics(true);
				_refreshControl.EndRefreshing();
			}


	}
}

