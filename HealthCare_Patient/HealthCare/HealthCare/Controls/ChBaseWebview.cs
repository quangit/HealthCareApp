using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using HealthCare.Helpers;

using Xamarin.Forms;

namespace HealthCare.Controls
{
   public class ChBaseWebview : WebView
    {
        private string _html;

        public string Html
        {
            get { return _html; }
            set
            {
                _html = value;
                if (_html.Contains("APPAUTHSUCCESS"))
                {
                    this.IsVisible = false;
                    RegexToken();
                    if (Common.OS == TargetPlatform.Android)
                    {
                        this.Source = new UrlWebViewSource()
                        {
                            Url = "http://chbase.bacsi24x7.vn/"
                        };
                    }
                }
            }
        }

        public string Token { get; set; }
        public void RegexToken()
        {
            Regex regex = new Regex(@"(?<=\bvalue=')[^']*");
            string temp = Html.Replace("\\\"", "'").Replace("\"","'");
            Match match = regex.Match(temp);
            Token = match.Value;
        }
    }
}
