using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.Phone.Views
{
    public partial class CmeCategoryView
    {
        public CmeCategoryView()
        {
            InitializeComponent();
        }

        private void CallSupportButton(object sender, RoutedEventArgs e)
        {
            HomeView.CallSupport();
        }

        public string Format(string html)
        {
            var script =
                "<script type=\"text/javascript\">\r\n    window.onload = function () {\r\n        var elem = document.getElementById('myContent');\r\n        window.external.Notify(elem.scrollHeight + '');\r\n    }\r\n</script>";
            var content =
                $"<!DOCTYPE html PUBLIC \" -//W3C//DTD HTML 4.01 Transitional//EN\"><html><head><meta name=\"viewport\" content=\"width=456\" /><style type=\"text/css\">h1{{ font-size:26px}} #myContent {{ padding: 5px; width: 436px; }}</style></head><body><div id=\"myContent\">\r\n{html}\r\n</div>{script}</body></html>";
            content = content.Replace("<div id=\"content-area\">", string.Empty)
                .Replace("<div id=\"content-inner\">", string.Empty);
            return Regex.Replace(content, @">\s+\r\n", ">");
        }
        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var b = sender as WebBrowser;

            var content = Format(b.Tag.ToString().Trim());
            b.NavigateToString(content);
        }
        protected void WebBrowser_ScriptNotify(object sender, NotifyEventArgs e)
        {
            WebBrowser thisBrowser = (WebBrowser)sender;
            int height = Convert.ToInt32(e.Value);
            double newHeight = height * 1.2;

            thisBrowser.Height = newHeight;
        }
        private void ItemTapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ((CmeCategoryViewModel)this.DataContext).CmeClassCommand.Execute((sender as FrameworkElement).DataContext);
        }
    }
}