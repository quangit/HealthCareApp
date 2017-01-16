using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using HealthCare.Core.Resources;
using HealthCare.Droid.Utilities;
using Java.Lang;
using HealthCare.Droid.Services;
using HealthCare.Core.Services.Interfaces;
using Cirrious.CrossCore;

namespace HealthCare.Droid.Views
{
    [Activity(Label = "", WindowSoftInputMode = Android.Views.SoftInput.AdjustResize)]
    public class LoginView : MvxActionBarActivity
    {
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.LoginView;
            }
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var resetPassTextView = FindViewById<TextView>(Resource.Id.resetPassTextView);
            SpannableString content = new SpannableString(AppResources.LoginView_ResetPass);
            content.SetSpan(new UnderlineSpan(), 0, content.Length(), 0);
            resetPassTextView.SetText(content, TextView.BufferType.Spannable);
        }

        public override async void OnBackPressed()
        {
           // base.OnBackPressed();
            var x = Mvx.Resolve<IMessageService>();
            var r = x as MessageService;
            if (r != null)
            {
                r.TopActivity = this;
                x = r;
            }
            var t =
                await
                    x.ShowConfirmMessageAsync(AppResources.Exit_Promp, AppResources.ApplicationTitle,
                        AppResources.Messsage_Yes, AppResources.Messsage_No);

            if (t)
            {
                this.FinishAffinity();
                JavaSystem.Exit(0);
            }
        }
    }
}