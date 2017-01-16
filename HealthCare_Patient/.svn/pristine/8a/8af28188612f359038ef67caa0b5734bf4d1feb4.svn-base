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
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using HealthCare.Droid.Renderers;

//[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabRenderer1))]
namespace HealthCare.Droid.Renderers
{

    public class CustomTabRenderer1 : TabbedRenderer
    {
        private Activity activity;
        private const string COLOR = "#f7f8f8";
        //This flag is used in the case when the app is not completely closed, and the user return back.
        private bool isFirstDesign = true;
        private bool isActionBarRendered = false;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            activity = this.Context as Activity;
            ActionBar actionBar = activity.ActionBar;

            ColorDrawable colorDrawable = new ColorDrawable(global::Android.Graphics.Color.ParseColor(COLOR));
            actionBar.SetStackedBackgroundDrawable(colorDrawable);
            if (actionBar.TabCount > 0)
            {
                ActionBarTabsSetup(actionBar);
            }
        }

        protected override void OnWindowVisibilityChanged(ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);
            if (isFirstDesign)
            {
                ActionBar actionBar = activity.ActionBar;

                ColorDrawable colorDrawable = new ColorDrawable(Android.Graphics.Color.ParseColor(COLOR));
                actionBar.SetStackedBackgroundDrawable(colorDrawable);
                ActionBarTabsSetup(actionBar);
                Handler handler = new Handler(activity.MainLooper);
                if (!isActionBarRendered)
                    renderActionBar(handler, actionBar);
                isFirstDesign = false;
            }
        }

        private void renderActionBar(Handler handler, ActionBar actionBar)
        {
            handler.PostDelayed(() =>
            {
                if (!isActionBarRendered)
                {
                    ActionBarTabsSetup(actionBar);
                    handler.PostDelayed(() => { renderActionBar(handler, actionBar); }, 100);
                }
            }, 100);
        }

        private void ActionBarTabsSetup(ActionBar actionBar)
        {
            if (actionBar.TabCount >= 3)
            {
                isActionBarRendered = true;
                Android.App.ActionBar.Tab keypad = actionBar.GetTabAt(0);
                if (TabIsEmpty(keypad))
                    TabSetup(keypad, Resource.Drawable.tab_appointment, "Phiếu khám");

                Android.App.ActionBar.Tab contacts = actionBar.GetTabAt(1);
                if (TabIsEmpty(contacts))
                    TabSetup(contacts, Resource.Drawable.tab_about, "Giới thiệu");

                Android.App.ActionBar.Tab favorites = actionBar.GetTabAt(2);
                if (TabIsEmpty(favorites))
                    TabSetup(favorites, Resource.Drawable.tab_account, "Tài khoản");
            }
        }

        private bool TabIsEmpty(ActionBar.Tab tab)
        {
            if (tab != null)
                if (tab.CustomView == null)
                    return true;
            return false;
        }

        private void TabSetup(ActionBar.Tab tab, int resourceID, String title)
        {
            LinearLayout llLayout = new LinearLayout(activity);
            llLayout.Orientation = Orientation.Vertical;
            llLayout.SetGravity(GravityFlags.Center);
            llLayout.SetPadding(0, 8, 0, 0);
            llLayout.SetBackgroundColor(new Android.Graphics.Color(44, 190, 113));
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(
                             LayoutParams.WrapContent,
                             LayoutParams.WrapContent, 1.0f);
            llLayout.LayoutParameters = param;
            
            TextView tv = new TextView(activity);
            tv.Text = title;
            tv.SetTextColor(Android.Graphics.Color.White);
            tv.LayoutParameters = param;

            ImageView iv = new ImageView(activity);
            iv.SetImageResource(resourceID);
            iv.LayoutParameters = param;

            llLayout.AddView(iv);
            llLayout.AddView(tv);
            tab.SetCustomView(llLayout);
        }
    }
}