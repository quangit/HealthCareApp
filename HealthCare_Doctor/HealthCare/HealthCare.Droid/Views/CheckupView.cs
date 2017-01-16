using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Core.Resources;
using HealthCare.Droid.Utilities;

namespace HealthCare.Droid.Views
{
    [Register("healthcare.droid.views.CheckupView"), Activity()]
    public class CheckupView : MvxActionBarActivity
    {
        protected override int LayoutResource
        {
            get { return Resource.Layout.CheckupView; }
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetIcon(Resource.Drawable.logo_bs);
            SupportActionBar.SetBackgroundDrawable(new ColorDrawable(Resources.GetColor(Resource.Color.ButtonGreenMainColor)));
            
            return base.OnCreateOptionsMenu(menu);
        }
    }
}