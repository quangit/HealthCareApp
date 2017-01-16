using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HealthCare.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CmeCategoryView : Page
    {
        public CmeCategoryView()
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
            var r = Regex.Replace(content, @">\s+\r\n", ">");
            return Regex.Replace(r, "<img src=\".*?\"/>", string.Empty);
        }
        private void WebViewLoaded(object sender, RoutedEventArgs e)
        {
            var webView = sender as WebView;
            webView?.NavigateToString(Format(webView.Tag?.ToString() ?? ""));
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var vm = this.DataContext as CmeCategoryViewModel;
            vm.CmeClassCommand.Execute(e.ClickedItem);
        }
    }
}
