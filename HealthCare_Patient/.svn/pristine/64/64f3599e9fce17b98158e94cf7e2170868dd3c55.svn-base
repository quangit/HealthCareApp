using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(WebView), typeof(HealthCare.iOS.Renderers.WebViewRenderer))]

namespace HealthCare.iOS.Renderers
{
    public class WebViewRenderer : Xamarin.Forms.Platform.iOS.WebViewRenderer
    {
        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var webView = this;
            Frame = new System.Drawing.RectangleF(0, 0, (float)UIScreen.MainScreen.Bounds.Width, (float)UIScreen.MainScreen.Bounds.Height);
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, ssl) => true;

            webView.AutoresizingMask = UIViewAutoresizing.All;
            webView.ScalesPageToFit = true;
        }


    }
}
