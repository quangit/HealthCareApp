using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using HealthCare.Droid.Controls;
using HealthCare.Droid.Utilities;
using HealthCare.Core.ViewModels;

namespace HealthCare.Droid.Views
{
    [Register("healthcare.droid.views.CmeClassView"), Activity()]
    public class CmeClassView : MvxActionBarActivity
    {
        protected override int LayoutResource
        {
            get { return Resource.Layout.CmeClassView; }
        }

        private BindableWebView contentWV;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            contentWV = FindViewById<BindableWebView>(Resource.Id.classWebView);
            contentWV.SetWebViewClient(new CMEWebViewClient());
            contentWV.Settings.BuiltInZoomControls = true;
            contentWV.Settings.DisplayZoomControls = false;
            contentWV.Settings.SetSupportZoom(true);
            var backButton = FindViewById<ImageButton>(Resource.Id.backbutton);
            var forwardButton = FindViewById<ImageButton>(Resource.Id.forwardbutton);
            var homeButton = FindViewById<ImageButton>(Resource.Id.homebutton);
            CmeClassViewModel _vm = ViewModel as CmeClassViewModel;
            homeButton.Click += (sender, args) =>
            {
                contentWV.LoadData(_vm.CmeClass.full_description, "text/html", "utf-8");
            };

            backButton.Click += (sender, args) =>
            {
                if (contentWV.CanGoBack())
                    contentWV.GoBack();
            };

            forwardButton.Click += (sender, args) =>
            {
                if (contentWV.CanGoForward())
                    contentWV.GoForward();
            };
        }
    }

    public class CMEWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}