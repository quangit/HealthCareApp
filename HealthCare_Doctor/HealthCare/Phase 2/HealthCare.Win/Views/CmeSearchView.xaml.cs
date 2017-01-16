using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HealthCare.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CmeSearchView : Page
    {
        public CmeSearchView()
        {
            this.InitializeComponent();
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
        private void WebViewLoaded(object sender, RoutedEventArgs e)
        {
            var webView = sender as WebView;
            webView?.NavigateToString(Format(webView.Tag?.ToString() ?? ""));
        }

        private void SearchBox_OnQuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            var vm = this.DataContext as CmeSearchViewModel;
            vm.SearchTerm = sender.QueryText;
            vm.SearchCommand.Execute();
        }
    }
}
