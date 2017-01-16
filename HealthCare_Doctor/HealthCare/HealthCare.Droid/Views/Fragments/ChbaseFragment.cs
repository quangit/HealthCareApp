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
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using HealthCare.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Java.Util;
using System.Diagnostics;
using Android.Webkit;

namespace HealthCare.Droid.Views.Fragments
{
    [Register("healthcare.droid.views.fragments.ChbaseFragment")]
    public class ChbaseFragment : MvxFragment
    {
        private HomeViewModel _vm;
        private View _view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            _view = this.BindingInflate(Resource.Layout.ChbaseFragment, null);
            _vm = (HomeViewModel)ViewModel;
            var webview = _view.FindViewById<WebView>(Resource.Id.webview);
            webview.SetWebViewClient(new HelloWebViewClient(this.Activity));
            webview.Settings.JavaScriptEnabled = true;
            webview.LoadUrl(_vm.CHBASE_URL);

            return _view;
        }
    }




    public class HelloWebViewClient : WebViewClient
    {
        public Activity mActivity;
        public HelloWebViewClient(Activity mActivity)
        {
            this.mActivity = mActivity;
        }
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}