using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using HealthCare.Droid.Utilities;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Android.Util;
using Android.Text.Method;
using HealthCare.Droid.Controls;

namespace HealthCare.Droid.Views
{
    [Register("healthcare.droid.views.ConsultView"), Activity()]
    public class ConsultView : MvxActionBarActivity, View.IOnTouchListener
    {

        private Action<string> ConsultDetailDialog;
        private ConsultViewModel _vm;

        public Android.Support.V7.Widget.Toolbar botToolbar
        {
            get;
            set;
        }


        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.ConsultView;
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (v.Id == Resource.Id.contentText)
            {
                v.Parent.RequestDisallowInterceptTouchEvent(true);
            }
            else
            {
                v.Parent.RequestDisallowInterceptTouchEvent(false);
            }
            return false;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _vm = ViewModel as ConsultViewModel;
            ConsultDetailDialog t = new ConsultDetailDialog();
            Action<string> consultDialog = new Action<string>((str)=>
            {
                t.ViewModel = this.ViewModel;
                t.Show(SupportFragmentManager, "");
            });
            ((ConsultViewModel) ViewModel).InviteDialogAction = consultDialog;

            ReplyDetailDialog r = new ReplyDetailDialog();
            Action<string> replyDialog = new Action<string>((str) =>
            {
                r.ViewModel = this.ViewModel;
                r.Show(SupportFragmentManager, "");
            });
            ((ConsultViewModel)ViewModel).ReplyDialogAction = replyDialog;

            var consultListView = FindViewById<MvxListViewWithHeader>(Resource.Id.consultlistview);
            //var view = this.BindingInflate(Resource.Layout.ConsultHeaderTemplate, null);
            //  var contentTextview = FindViewById<TextView>(Resource.Id.contentText1);
            //consultListView.AddHeaderView(view);

            //  contentTextview.MovementMethod = new ScrollingMovementMethod();
            //contentTextview.SetOnTouchListener(this);
            //consultListView.SetOnTouchListener(this);

            Action<string> imageClick = new Action<string>((str) => {
                ((ConsultViewModel)ViewModel).ImageZoomCommand.Execute(null);
            });

            consultListView.ImageAction = imageClick;


            botToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_bot);
         
            var InviteBut = botToolbar.FindViewById<ImageButton>(Resource.Id.invitebutton);


            var skypeBut = botToolbar.FindViewById<ImageButton>(Resource.Id.skypebutton);
			if (string.IsNullOrEmpty(_vm.Request.PatientSkypeUrl))
			{
				skypeBut.Enabled = false;
				skypeBut.Alpha = 0.5f;
			}
            skypeBut.Click += (sender, args) =>
            {
                PackageManager pm = this.PackageManager;
                bool installed = false;
                try
                {
                    pm.GetPackageInfo("com.skype.raider", PackageInfoFlags.Activities);
                    installed = true;
                }
                catch (PackageManager.NameNotFoundException e)
                {
                    installed = false;
                }
                if (installed)
                {
                    Android.Net.Uri marketUri = Android.Net.Uri.Parse(_vm.Request.PatientSkypeUrl);
                    Intent myIntent = new Intent(Intent.ActionView, marketUri);
                    myIntent.SetComponent(new ComponentName("com.skype.raider", "com.skype.raider.Main"));
                    myIntent.SetFlags(ActivityFlags.NewTask);
                    BaseContext.StartActivity(myIntent);
                }
                else
                {
                    Android.Net.Uri marketUri = Android.Net.Uri.Parse("market://details?id=com.skype.raider");
                    Intent myIntent = new Intent(Intent.ActionView, marketUri);
                    myIntent.SetFlags(ActivityFlags.NewTask);
                    BaseContext.StartActivity(myIntent);
                }

            };

            // InviteBut.Click += InviteBut_Click;
            //botToolbar.TextAlignment = TextAlignment.Center;
            //botToolbar.InflateMenu(Resource.Menu.tuvan);

            //botToolbar.MenuItemClick += BotToolbar_MenuItemClick;
        }

        //private void InviteBut_Click(object sender, EventArgs e)
        //{
        //    ((ConsultViewModel)ViewModel).ShowInviteCommand.Execute(null);
        //}

        private void BotToolbar_MenuItemClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    




}