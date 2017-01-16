using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.DependencyServices;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public delegate bool ShouldTrustCertificate(ICertificate certificate);

  public  class WebViewCustom : WebView
    {
        public ShouldTrustCertificate ShouldTrustUnknownCertificate { get; set; }
        public string Javascript { get; set; }
        public bool InvalidPass { get; set; }
        public string Html { get; set; }

        public bool JavascriptLogined { get; set; }

    }
}
