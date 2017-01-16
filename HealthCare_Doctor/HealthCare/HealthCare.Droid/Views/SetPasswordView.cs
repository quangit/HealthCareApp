using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Droid.Utilities;
using HealthCare.Core.Resources;

namespace HealthCare.Droid.Views
{

    [Register("healthcare.droid.views.SetPasswordView"), Activity(Label = "")]
    public class SetPasswordView : MvxActionBarActivity
    {
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.SetPasswordView;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetIcon(Resource.Drawable.logo_bs);

            SupportActionBar.Title = AppResources.ApplicationTitle;
            return base.OnCreateOptionsMenu(menu);
        }
    }
}