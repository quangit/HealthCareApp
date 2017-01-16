using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using HealthCare.WinPhone.Renderer;
using HealthCare.WinPhone.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(WebViewJavascript), typeof(WebViewJavascriptRenderer))]

namespace HealthCare.WinPhone.Renderer
{
   public class WebViewJavascriptRenderer: WebViewRenderer
    {
        public WebView _webView;
        
        private new WebViewJavascript Element { get { return (WebViewJavascript)base.Element; } }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
       {
           base.OnElementChanged(e);
         //Control.IsScriptEnabled = true;
         //   if (e.NewElement != null)
         //   {
         //       _webView = e.NewElement;
         //       Control.Navigating += (sender, args) =>
         //       {
         //          UserDialogs.Instance.ShowLoading();
         //       };
         //       Control.LoadCompleted += async (sender, args) =>
         //       {
         //           string s = Control.SaveToString();
         //           UserDialogs.Instance.HideLoading(); 
         //           if (!Control.Source.AbsolutePath.Contains(AppConstant.ChBaseUrl))
         //           {
         //               if (s.Contains(AppConstant.ChBaseErrorLogin))
         //               {
         //                   await
         //                       UserDialogs.Instance.AlertAsync(
         //                           SettingViewModel.Instance.Get_rs_failure_login_chbase_fail());
         //                   SettingViewModel.Instance.Email = Settings.ChBaseEmail;
         //                   SettingViewModel.Instance.Password = Settings.ChBasePass;

         //                   Settings.ChBaseEmail = Settings.ChBasePass = String.Empty;
         //                   SettingViewModel.Instance.ShowPopup = true;
         //               }
         //               else if (s.Contains(AppConstant.ChBaseNeedLogin))
         //               {
         //                   try
         //                   {
         //                       Control.InvokeScript("eval",
         //                           Element.Javascript);
         //                   }
         //                   catch (Exception ex)
         //                   {
         //                       MessageBox.Show(ex.ToString());
         //                   }
         //               }
         //           }

         //       };
               
         //   }

        }
    }
}
