using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Provider;
using Cirrious.MvvmCross.Droid.Views;
using Android.Support.V4.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using HealthCare.Droid.Utilities;
using Android.Widget;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.ViewModels;
using HealthCare.Droid.Services;
using HealthCare.Droid.Views.Fragments;
using Java.Lang;
using Java.Interop;
using HealthCare.Core.Models;

using System.Diagnostics;
using HealthCare.Droid.Controls;
//using Color = Android.Provider.Settings.System.Drawing.Color;

namespace HealthCare.Droid.Views
{
    [Activity(Label = "", AllowTaskReparenting = false, AlwaysRetainTaskState = false)]
    public class HomeView : MvxActionBarActivity, AdapterView.IOnItemClickListener, IMenuItemOnMenuItemClickListener
    {
        private MyActionBarDrawerToggle _drawerToggle;
        private string _drawerTitle;
        private string _title;
        private DrawerLayout _drawerLayout;
        private MvxListView _drawerListView;
        private MvxListView _drawerListViewBot;
        // private HomeViewModel _vm;

        private readonly MenuItem[] _sections = new[]
        {
            new MenuItem {Name = AppResources.Schedule_Title, ImageId = Resource.Drawable.Calendar_icon},
            new MenuItem {Name = AppResources.Checkups_Title, ImageId = Resource.Drawable.handbook},
            new MenuItem {Name = AppResources.CHbase_Title, ImageId = Resource.Drawable.chbase },
            new MenuItem {Name = AppResources.Consult_Title, ImageId = Resource.Drawable.askme},
            new MenuItem {Name = AppResources.CmeLibrary_Title, ImageId = Resource.Drawable.cmelibrary},
            new MenuItem {Name = AppResources.WeakTopics_Title, ImageId = Resource.Drawable.cmestudy},
           


            new MenuItem {Name = AppResources.Settings, ImageId = Resource.Drawable.ic_setting},
            new MenuItem {Name = AppResources.Logout, ImageId = Resource.Drawable.logout}
        };

        private static MvxFragment[] _fragments;


        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.HomeView;
            }
        }


        protected override void OnResume()
        {
            base.OnResume();
            var vm = this.ViewModel as HomeViewModel;
            vm.RaisePropertyChanged("Doctor");
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _title = _drawerTitle = Title;
            Title = AppResources.ApplicationTitle;
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _drawerListView = FindViewById<MvxListView>(Resource.Id.leftmenu_drawer);
            _drawerListView.ItemsSource = _sections.Take(6);
            _drawerListView.OnItemClickListener = (this);

            _drawerListViewBot = FindViewById <MvxListView>(Resource.Id.setting_drawer);
            _drawerListViewBot.ItemsSource = _sections.Skip(6);//.Take(4);
            _drawerListViewBot.OnItemClickListener = (this);
            
            UpdateHeight();
            //System.Diagnostics.Debug.WriteLine("So lan xuat ra:" + coutable);

            _drawerToggle = new MyActionBarDrawerToggle(this, _drawerLayout,
                Toolbar,
                Resource.String.drawer_open,
                Resource.String.drawer_close);
            _drawerToggle.SyncState();


            _drawerToggle.DrawerClosed += (o, args) =>
            {
                SupportActionBar.Title = AppResources.ApplicationTitle;
                InvalidateOptionsMenu();
            };
            _drawerToggle.DrawerOpened += (o, args) =>
            {
                SupportActionBar.Title = AppResources.ApplicationTitle;
                InvalidateOptionsMenu();
            };
            _drawerLayout.SetDrawerListener(_drawerToggle);
            InitFragments();
            SupportActionBar.SetIcon(Resource.Drawable.menu_icon);

            PushRegister();
        }



        public void UpdateHeight()
        {
            SetListViewHeightBasedOnChildren(_drawerListView);
            SetListViewHeightBasedOnChildren(_drawerListViewBot);
            var scrollview = FindViewById<ScrollView>(Resource.Id.scrollView);
            //var lp = scrollview.LayoutParameters;
            //lp = new ScrollView.LayoutParams(ScrollView.LayoutParams.MatchParent, ScrollView.LayoutParams.WrapContent);
            //scrollview.LayoutParameters = lp;
            //scrollview.RequestLayout();
            scrollview.Post(new Runnable(delegate
            {
                scrollview.ScrollTo(0, 0);
            }));
        }

     
        public bool SetListViewHeightBasedOnChildren(MvxListView listView)
        {
                var listAdapter = listView.Adapter;
                if (listAdapter == null)
                {
                    // pre-condition
                    return true;
                }

                // int totalHeight = listView.PaddingTop + listView.PaddingBottom;
                int totalHeight = 0;
               // int desiredWidth = View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.AtMost);
                for (int i = 0; i < listAdapter.Count; i++)
                {

                    View listItem = listAdapter.GetView(i, null, listView);

                    if (listItem != null)
                    {
                    // This next line is needed before you call measure or else you won't get measured height at all. The listitem needs to be drawn first to know the height.
                    //listItem.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
                    //listItem.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);
                    listItem.Measure(0,0);
                    totalHeight += listItem.MeasuredHeight;
                    System.Diagnostics.Debug.WriteLine("So lan xuat ra:" + totalHeight);

                }
                }
               
                ViewGroup.LayoutParams p = listView.LayoutParameters;
                p.Height = totalHeight;
                listView.LayoutParameters = p;
                listView.RequestLayout();
                return true;
        }

   


        private void InitFragments()
        {
            if (_fragments == null)
            {
                _fragments = new MvxFragment[_sections.Length];
                _fragments[0] = new ScheduleFragment() { ViewModel = this.ViewModel };
                _fragments[1] = new CheckupsFragment() { ViewModel = this.ViewModel };
                _fragments[2] = new ChbaseFragment() { ViewModel = this.ViewModel };
                _fragments[3] = new ConsultFragment() { ViewModel = this.ViewModel };
                _fragments[4] = new CmeLibraryFragment() { ViewModel = this.ViewModel };
                _fragments[5] = new WeekTopicFragment() { ViewModel = this.ViewModel };
                
                _fragments[6] = new SettingFragment() { ViewModel = this.ViewModel };
               
            }
            SupportFragmentManager.BackStackChanged += (sender, args) =>
            {
                var f = SupportFragmentManager.FindFragmentById(Resource.Id.content_frame);
            };

            //SupportFragmentManager.PopBackStack(null, 1);
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, _fragments[0])
                .Commit();

            _drawerListView.SetItemChecked(0, true);
        }

        private void ReplaceFragment(MvxFragment fragment)
        {
            if (fragment == null)
                return;

            var backStateName = fragment.Class.Name;

            var manager = SupportFragmentManager;
            var fragmentPopped = manager.PopBackStackImmediate(backStateName, 0);

            var ft = manager.BeginTransaction();
            if (!fragmentPopped)
            { //fragment not in back stack, create it.
                ft.Replace(Resource.Id.content_frame, fragment);

            }

            ft.AddToBackStack(backStateName);
            ft.Commit();
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {

            try
            {


                if (_drawerListView == parent)
                {
                    _drawerListView.SetItemChecked(position, true);
                    _drawerListViewBot.SetItemChecked(0, false);
                    _drawerListViewBot.SetItemChecked(1, false);
                }
                else if (_drawerListViewBot == parent)
                {
                    position += 6;
                    if (position == 6)
                        _drawerListViewBot.SetItemChecked(0, false);
                    else
                    {
                        _drawerListView.SetItemChecked(0, false);
                        _drawerListView.SetItemChecked(1, false);
                        _drawerListView.SetItemChecked(2, false);
                        _drawerListView.SetItemChecked(3, false);
                        _drawerListView.SetItemChecked(4, false);
                        _drawerListView.SetItemChecked(5, false);
                    }
                }
                if (position == 7)
                {
                    Logout();

                    return;
                }
                var fragment = _fragments[position];
                ReplaceFragment(fragment);

                string sTitle = "";
                //Set title
                switch (position)
                {
                    case 0:
                        sTitle = AppResources.Schedule_Title;
                        break;
                    case 1:
                        sTitle = AppResources.Checkups_Title;
                        break;
                    case 2:
                        sTitle = AppResources.CHbase_Title;
                        break;
                    case 3:
                        sTitle = AppResources.Consult_Title;
                        break;
                    case 4:
                        sTitle = AppResources.WeakTopics_Title;
                        break;
                    case 5:
                        sTitle = AppResources.Settings;
                        break;

                }

                //SupportActionBar.Title = _title = sTitle;
                SupportActionBar.Title = AppResources.ApplicationTitle;
            }
            catch (Exception)
            {

            }
            finally
            {
                _drawerLayout.CloseDrawers();
            }

        }

        private async void Logout()
        {
            var x = Mvx.Resolve<IMessageService>();
            var r = x as MessageService;
            if (r != null)
            {
                r.TopActivity = this;
                x = r;
            }
            var t =
                await
                    x.ShowConfirmMessageAsync(AppResources.Logout_contentMessageBox, AppResources.ApplicationTitle,
                        AppResources.Messsage_Yes, AppResources.Messsage_No);
            if (t)
            {
                try
                {
                    var mainViewModel = this.ViewModel as HomeViewModel;
                    if (mainViewModel != null) mainViewModel.DoLogoutTap();

                    Java.Lang.JavaSystem.Exit(0);
                }
                catch (System.Exception)
                {
                    //throw;
                }
            }

        }


        public bool OnMenuItemClick(IMenuItem item)
        {
            return true;
        }

        public override async void OnBackPressed()
        {
            var x = Mvx.Resolve<IMessageService>();
            var r = x as MessageService;
            if (r != null)
            {
                r.TopActivity = this;
                x = r;
            }
            var t =
                await
                    x.ShowConfirmMessageAsync(AppResources.Exit_Promp, AppResources.ApplicationTitle,
                        AppResources.Messsage_Yes, AppResources.Messsage_No);

            if (t)
            {
                this.FinishAffinity();
                JavaSystem.Exit(0);
            }
            //base.OnBackPressed();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (GetType() == typeof(LoginView) || GetType() == typeof(SignUpView))
                return base.OnCreateOptionsMenu(menu);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetIcon(Resource.Drawable.logo_bs);
            SupportActionBar.SetBackgroundDrawable(new ColorDrawable(Resources.GetColor(Resource.Color.ButtonGreenMainColor)));

            SupportActionBar.Title = AppResources.ApplicationTitle;
            //SupportActionBar.SetTitle(Resource.Drawable.logo_bs);

            return base.OnCreateOptionsMenu(menu);
        }

        private void PushRegister()
        {
            string senders = DeveloperKey.SenderID;
            Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
            intent.SetPackage("com.google.android.gsf");
            intent.PutExtra("app", PendingIntent.GetBroadcast(this, 0, new Intent(), 0));
            intent.PutExtra("sender", senders);
            this.StartService(intent);
        }


//        [Export]
//        public async void PdfClick(View view)
//        {
//            var listView = (MvxBaseListItemView)(view.Parent.Parent.Parent.Parent);
//            var weektopicDatum = (Topic)listView.DataContext;
//            var _vm = (HomeViewModel)ViewModel;
//            if (!weektopicDatum.isFiles)
//            {
//                var x = Mvx.Resolve<IMessageService>();
//                var r = x as MessageService;
//                if (r != null)
//                {
//                    r.TopActivity = this;
//                    x = r;
//                }
//                
//                await x.ShowMessageAsync(AppResources.WeekTopicFile_Empty, AppResources.ApplicationTitle);
//                return;
//            }
//
//           _vm.ShowWeekTopicFileCommand.Execute(weektopicDatum);
//        }
    }

    internal class DeveloperKey
    {
        public static string SenderID = "195044918345";
    }
}