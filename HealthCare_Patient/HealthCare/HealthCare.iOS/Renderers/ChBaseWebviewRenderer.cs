﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Acr.UserDialogs;
using Foundation;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.iOS.Renderers;
using ObjCRuntime;
using Security;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(ChBaseWebview), typeof(ChBaseWebviewRenderer))]

namespace HealthCare.iOS.Renderers
{
    public class ChBaseWebviewRenderer : WebViewRenderer
    {
            public new ChBaseWebview Element
            {
                get { return (ChBaseWebview) base.Element; }
            }

            private WebNavigationEvent _lastNavigationEvent;
            private WebViewSource _lastSource;
            private string _lastUrl;

            protected override void OnElementChanged(VisualElementChangedEventArgs e)
            {
                base.OnElementChanged(e);

            if (!(Delegate is AuthenticatingWebViewDelegate))
            {
                var originalDelegate = (NSObject)Delegate;

                // We are deliberately overwriting this delegate.
                // If we don't null it out first then we would get an exception.
                Delegate = null;
                Delegate = new AuthenticatingWebViewDelegate(this, originalDelegate);
            }

            if (e.OldElement != null)
            {
                ((WebView)e.OldElement).Navigating -= HandleElementNavigating;
            }

            if (e.NewElement != null)
            {
                ((WebView)e.NewElement).Navigating += HandleElementNavigating;
            }
        }

        private void HandleElementNavigating(object sender, WebNavigatingEventArgs e)
            {
                _lastNavigationEvent = e.NavigationEvent;
                _lastSource = e.Source;
                _lastUrl = e.Url;
            }

            private class AuthenticatingWebViewDelegate : UIWebViewDelegate, INSUrlConnectionDelegate
            {
                private readonly Renderers.ChBaseWebviewRenderer _renderer;
                private readonly NSObject _originalDelegate;
                private NSUrlRequest _request;

            public AuthenticatingWebViewDelegate(Renderers.ChBaseWebviewRenderer renderer,
                    NSObject originalDelegate)
                {
                    _renderer = renderer;
                    _originalDelegate = originalDelegate;
                }

                public override void LoadFailed(UIWebView webView, NSError error)
                {
                    UserDialogs.Instance.HideLoading();
                    // Ignore "Frame load interrupted" error that occurrs during redirects.
                    // Seems innocuous.
                    if (error.Domain != "WebKitErrorDomain" || error.Code != 102)
                    {
                        ForwardDelegateMethod("webView:didFailLoadWithError:", webView, error);
                    }
                }

                public override void LoadingFinished(UIWebView webView)
                {

                UserDialogs.Instance.HideLoading();
                    if (!webView.Request.Url.AbsoluteString.Contains(AppConstant.ChBaseUrl))
                        _renderer.Element.Html = webView.EvaluateJavascript("document.documentElement.outerHTML");
                if (webView.Request.Url.AbsoluteString.Contains(ChBaseHelper.SignupUrl))
                {
                    webView.EvaluateJavascript(ChBaseHelper.AutoFillDataJs);
                }
                ForwardDelegateMethod("webViewDidFinishLoad:", webView);
                }

                public override void LoadStarted(UIWebView webView)
                {
                    ForwardDelegateMethod("webViewDidStartLoad:", webView);
                }

                public override bool ShouldStartLoad(UIWebView webView, NSUrlRequest request,
                    UIWebViewNavigationType navigationType)
                {
                    if (_request != null)
                    {
                        _request = null;
                        return true;
                    }

                    bool originalResult = ForwardDelegatePredicate(
                        "webView:shouldStartLoadWithRequest:navigationType:", webView, request, (int) navigationType,
                        defaultResult: true);

                   

                    return originalResult;
                }

                [Export("connection:willSendRequestForAuthenticationChallenge:")]
                private void WillSendRequestForAuthenticationChallenge(NSUrlConnection connection,
                    NSUrlAuthenticationChallenge challenge)
                {
                    if (challenge.ProtectionSpace.AuthenticationMethod ==
                        NSUrlProtectionSpace.AuthenticationMethodServerTrust)
                    {
                        var trust = challenge.ProtectionSpace.ServerSecTrust;
                        var result = trust.Evaluate();
                        bool trustedCertificate = result == SecTrustResult.Proceed ||
                                                  result == SecTrustResult.Unspecified;

                        if (!trustedCertificate && trust.Count != 0)
                        {
                            var originalCertificate = trust[0].ToX509Certificate2();
                            var x509Certificate = new Certificate(challenge.ProtectionSpace.Host, originalCertificate);
                        }

                        if (trustedCertificate)
                        {
                            challenge.Sender.UseCredential(new NSUrlCredential(trust), challenge);
                        }
                        else
                        {
                            Console.WriteLine("Rejecting request");
                            challenge.Sender.CancelAuthenticationChallenge(challenge);


                            return;
                        }
                    }
                    challenge.Sender.PerformDefaultHandling(challenge);
                }

              
                

                

                #region Ugly wrapper stuff

                [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
                private static extern bool void_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

                [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
                private static extern bool void_objc_msgSend_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1,
                    IntPtr arg2);

                [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
                private static extern bool bool_objc_msgSend_IntPtr_IntPtr_int(IntPtr receiver, IntPtr selector,
                    IntPtr arg1, IntPtr arg2, int arg3);

                private void ForwardDelegateMethod(string selName, NSObject arg1)
                {
                    var sel = new Selector(selName);
                    if (_originalDelegate.RespondsToSelector(sel))
                    {
                        void_objc_msgSend_IntPtr(
                            _originalDelegate.Handle,
                            sel.Handle,
                            arg1 != null ? arg1.Handle : IntPtr.Zero);
                    }
                }

                private void ForwardDelegateMethod(string selName, NSObject arg1, NSObject arg2)
                {
                    var sel = new Selector(selName);
                    if (_originalDelegate.RespondsToSelector(sel))
                    {
                        void_objc_msgSend_IntPtr_IntPtr(
                            _originalDelegate.Handle,
                            sel.Handle,
                            arg1 != null ? arg1.Handle : IntPtr.Zero,
                            arg2 != null ? arg2.Handle : IntPtr.Zero);
                    }
                }

                private bool ForwardDelegatePredicate(string selName, NSObject arg1, NSObject arg2, int arg3,
                    bool defaultResult)
                {
                    var sel = new Selector(selName);
                    if (_originalDelegate.RespondsToSelector(sel))
                    {
                        return bool_objc_msgSend_IntPtr_IntPtr_int(
                            _originalDelegate.Handle,
                            sel.Handle,
                            arg1 != null ? arg1.Handle : IntPtr.Zero,
                            arg2 != null ? arg2.Handle : IntPtr.Zero,
                            arg3);
                    }

                    return defaultResult;
                }

                #endregion

                #region Ugly reflection stuff

                private MethodInfo _sendNavigatedMethodInfo;

                private delegate void SendNavigatedDelegate(WebNavigatedEventArgs args);

                private MethodInfo SendNavigatedMethodInfo
                {
                    get
                    {
                        if (_sendNavigatedMethodInfo == null)
                        {
                            _sendNavigatedMethodInfo = typeof (WebView).GetMethod(
                                name: "SendNavigated",
                                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                                binder: null,
                                types: new[] {typeof (WebNavigatedEventArgs)},
                                modifiers: null);
                        }

                        return _sendNavigatedMethodInfo;
                    }
                }

               

                #endregion
            }
        }
    }