using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.ViewModels;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using HealthCare.Helpers;

[assembly: ExportRenderer(typeof(ChBaseWebview), typeof(ChBaseWebviewRenderer))]
namespace HealthCare.WinPhone.Renderer
{
   public class ChBaseWebviewRenderer : WebViewRenderer
    {
        public WebView _webView;
        private new ChBaseWebview Element { get { return (ChBaseWebview)base.Element; } }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            Control.IsScriptEnabled = true;
            if (e.NewElement != null)
            {
                _webView = e.NewElement;
                Control.Navigating += (sender, args) =>
                {
                    if (args.Uri.AbsolutePath.Contains(ChBaseHelper.SignupUrl) && string.IsNullOrEmpty(UserViewModel.Instance.CurrentUser.Email))
                    {
                        Control.Visibility = Visibility.Collapsed;
                        args.Cancel = true;
                        return;
                    }
                    UserDialogs.Instance.ShowLoading();
                    Control.Visibility = Visibility.Visible;
                };
                Control.LoadCompleted +=  (sender, args) =>
                {
                  
                    string s = Control.SaveToString();
                    UserDialogs.Instance.HideLoading();
                    Element.Html = s;

                    if (args.Uri.AbsolutePath.Contains(ChBaseHelper.SignupUrl))
                    {
                        Control.InvokeScript("eval", ChBaseHelper.AutoFillDataJs);
                    }
                };

            }

        }
    }
}
