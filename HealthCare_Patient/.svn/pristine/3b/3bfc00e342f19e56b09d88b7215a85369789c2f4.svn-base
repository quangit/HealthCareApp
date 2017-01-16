using System;
using System.Collections.Generic;
using System.Text;
using HealthCare.Controls;
using HealthCare.Pages;
using HealthCare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HealthVaultPage), typeof(HealthCare.iOS.Renderers.PageRenderer))]

namespace HealthCare.iOS.Renderers
{
  public  class PageRenderer: Xamarin.Forms.Platform.iOS.PageRenderer
    {
      public override void ViewWillDisappear(bool animated)
      {
            SettingViewModel.Instance.HidePopup();
            base.ViewWillDisappear(animated);
      }
    }
}
