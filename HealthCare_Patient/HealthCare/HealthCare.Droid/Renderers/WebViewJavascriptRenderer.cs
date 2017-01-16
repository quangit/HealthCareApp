using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Object = Java.Lang.Object;
using WebView = Xamarin.Forms.WebView;

[assembly: ExportRenderer(typeof(WebViewJavascript), typeof(WebViewJavascriptRenderer))]

namespace HealthCare.Droid.Renderers
{
    public class WebViewJavascriptRenderer : WebViewRenderer
    {
        private WebNavigationEvent _lastNavigationEvent;
        private WebViewSource _lastSource;
        private string _lastUrl;
        public WebView _webView;
        private new WebViewJavascript Element { get { return (WebViewJavascript)base.Element; } }
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            //var proxiedClient = CreateXamarinWebViewClient();
            //var proxyClient = new AuthenticatingWebViewClient(this, proxiedClient);

            //Control.SetWebViewClient(proxyClient);
            //if (e.OldElement != null)
            //{
            //    ((WebView)e.OldElement).Navigating -= HandleElementNavigating;
            //}

            //if (e.NewElement != null)
            //{
            //    ((WebView)e.NewElement).Navigating += HandleElementNavigating;
            //    _webView = e.NewElement;

            //}
        }

        private void HandleElementNavigating(object sender, WebNavigatingEventArgs e)
        {
            _lastNavigationEvent = e.NavigationEvent;
            _lastSource = e.Source;
            _lastUrl = e.Url;
        }

        private class AuthenticatingWebViewClient : Android.Webkit.WebViewClient
        {
            private readonly WebViewJavascriptRenderer _renderer;
            private readonly Android.Webkit.WebViewClient _originalClient;

            public AuthenticatingWebViewClient(WebViewJavascriptRenderer renderer, Android.Webkit.WebViewClient originalClient)
            {
                _renderer = renderer;
                _renderer.Element.Html = "";
                _originalClient = originalClient;
            }

            public override void DoUpdateVisitedHistory(Android.Webkit.WebView view, string url, bool isReload)
            {
                if (_originalClient != null)
                {
                    _originalClient.DoUpdateVisitedHistory(view, url, isReload);
                }
            }

            public override void OnFormResubmission(Android.Webkit.WebView view, Android.OS.Message dontResend, Android.OS.Message resend)
            {
                if (_originalClient != null)
                {
                    _originalClient.OnFormResubmission(view, dontResend, resend);
                }
            }

            public override void OnLoadResource(Android.Webkit.WebView view, string url)
            {
                if (_originalClient != null)
                {
                    _originalClient.OnLoadResource(view, url);
                }
            }

            public override void OnPageFinished(Android.Webkit.WebView view, string url)
            {
                if (_originalClient != null)
                {
                    UserDialogs.Instance.HideLoading();
                    _originalClient.OnPageFinished(view, url);
                    if(!url.Contains(AppConstant.ChBaseUrl))
                    view.EvaluateJavascript("document.getElementsByTagName('html')[0].innerHTML;", new MyJavaScriptInterface(_renderer.Element));

                }
            }

            public override void OnPageStarted(Android.Webkit.WebView view, string url, Android.Graphics.Bitmap favicon)
            {
                
                if (_originalClient != null)
                {
                    if(_renderer.Element.IsVtcPay)
                        UserDialogs.Instance.ShowLoading();
                    //view.RemoveJavascriptInterface("droid");
                    //view.AddJavascriptInterface(new MyJavaScriptInterface(_renderer.Element), "droid");
                    _originalClient.OnPageStarted(view, url, favicon);
                }
            }

            public override void OnReceivedError(Android.Webkit.WebView view, Android.Webkit.ClientError errorCode, string description, string failingUrl)
            {
                if (_originalClient != null)
                {
                    UserDialogs.Instance.HideLoading();
                    Common.ShowErrorNetWork();
                    _originalClient.OnReceivedError(view, errorCode, description, failingUrl);
                }
            }

            public override void OnReceivedHttpAuthRequest(Android.Webkit.WebView view, Android.Webkit.HttpAuthHandler handler, string host, string realm)
            {
                if (_originalClient != null)
                {
                    _originalClient.OnReceivedHttpAuthRequest(view, handler, host, realm);
                }
            }

            public override void OnReceivedSslError(Android.Webkit.WebView view, Android.Webkit.SslErrorHandler handler, Android.Net.Http.SslError error)
            {
                bool success = false;
                if (_renderer.Element.ShouldTrustUnknownCertificate != null)
                {
                    var certificate = new Certificate(error.Url, error.Certificate);
                    var result = _renderer.Element.ShouldTrustUnknownCertificate(certificate);
                    if (result)
                    {
                        success = true;
                        handler.Proceed();
                    }
                    else
                    {
                        handler.Cancel();
                    }
                }

                if (!success)
                {
                    SendNavigated(
                        new WebNavigatedEventArgs(
                            _renderer._lastNavigationEvent,
                            _renderer._lastSource,
                            _renderer._lastUrl,
                            WebNavigationResult.Failure));
                }
            }

            public override void OnScaleChanged(Android.Webkit.WebView view, float oldScale, float newScale)
            {
                if (_originalClient != null)
                {
                    _originalClient.OnScaleChanged(view, oldScale, newScale);
                }
            }

            [Obsolete("deprecated")]
            public override void OnTooManyRedirects(Android.Webkit.WebView view, Android.OS.Message cancelMsg, Android.OS.Message continueMsg)
            {
                if (_originalClient != null)
                {
                    _originalClient.OnTooManyRedirects(view, cancelMsg, continueMsg);
                }
            }

            [Obsolete("deprecated")]
            public override void OnUnhandledKeyEvent(Android.Webkit.WebView view, Android.Views.KeyEvent e)
            {
                if (_originalClient != null)
                {
                    _originalClient.OnUnhandledKeyEvent(view, e);
                }
            }

            public override bool ShouldOverrideKeyEvent(Android.Webkit.WebView view, Android.Views.KeyEvent e)
            {
                if (_originalClient != null)
                {
                    return _originalClient.ShouldOverrideKeyEvent(view, e);
                }

                return false;
            }

            public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, string url)
            {
                if (_originalClient != null)
                {
                    return _originalClient.ShouldOverrideUrlLoading(view, url);
                }

                return false;
            }

            #region Ugly reflection stuff

            private MethodInfo _sendNavigatedMethodInfo;

            private delegate void SendNavigatedDelegate(WebNavigatedEventArgs args);

            private MethodInfo SendNavigatedMethodInfo
            {
                get
                {
                    if (_sendNavigatedMethodInfo == null)
                    {
                        _sendNavigatedMethodInfo = typeof(WebView).GetMethod(
                            name: "SendNavigated",
                            bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                            binder: null,
                            types: new[] { typeof(WebNavigatedEventArgs) },
                            modifiers: null);
                    }

                    return _sendNavigatedMethodInfo;
                }
            }

            private void SendNavigated(WebNavigatedEventArgs args)
            {
                var method = SendNavigatedMethodInfo;
                if (method != null)
                {
                    var methodDelegate = (SendNavigatedDelegate)method.CreateDelegate(typeof(SendNavigatedDelegate), _renderer.Element);
                    methodDelegate(args);
                }
            }

            #endregion
        }

        #region Ugly reflection stuff

        private Type _xamarinWebViewClientType;
        private ConstructorInfo _xamarinWebViewClientConstructorInfo;

        private Type XamarinWebViewClientType
        {
            get
            {
                if (_xamarinWebViewClientType == null)
                {
                    _xamarinWebViewClientType = typeof(WebViewRenderer).GetNestedType("WebClient", BindingFlags.NonPublic);
                }

                return _xamarinWebViewClientType;
            }
        }

        private ConstructorInfo XamarinWebViewClientConstructorInfo
        {
            get
            {
                if (_xamarinWebViewClientConstructorInfo == null)
                {
                    if (XamarinWebViewClientType != null)
                    {
                        _xamarinWebViewClientConstructorInfo = XamarinWebViewClientType.GetConstructor(new[] { typeof(WebViewRenderer) });
                    }
                }

                return _xamarinWebViewClientConstructorInfo;
            }
        }

        private Android.Webkit.WebViewClient CreateXamarinWebViewClient()
        {
            if (XamarinWebViewClientConstructorInfo != null)
            {
                return (Android.Webkit.WebViewClient)XamarinWebViewClientConstructorInfo.Invoke(new[] { this });
            }

            return null;
        }

        #endregion

        class MyJavaScriptInterface:Java.Lang.Object, IValueCallback
        {
            private WebViewJavascript _webViewJavascript;
            public MyJavaScriptInterface(WebViewJavascript webView)
            {
                _webViewJavascript = webView;
            }
          public void ProcessHTML(String html)
          {
              _webViewJavascript.Html = html;
          }

            public async void OnReceiveValue(Object value)
            {
                if(_webViewJavascript == null)
                    return;
                _webViewJavascript.Html = value.ToString();
                if (_webViewJavascript.Html.Contains(AppConstant.ChBaseErrorLogin))
                {
                    _webViewJavascript.IsVisible = false;
                    await UserDialogs.Instance.AlertAsync(SettingViewModel.Instance.Get_rs_failure_login_chbase_fail());
                    SettingViewModel.Instance.Email = Settings.ChBaseEmail;
                    SettingViewModel.Instance.Password = Settings.ChBasePass;

                    Settings.ChBaseEmail = Settings.ChBasePass = String.Empty;
                    SettingViewModel.Instance.ShowPopup = true;
                }
                else if (_webViewJavascript.Html.Contains(AppConstant.ChBaseNeedLogin))
                {
                    _webViewJavascript.Eval(_webViewJavascript.Javascript);
                }

            }
        }
    }

}