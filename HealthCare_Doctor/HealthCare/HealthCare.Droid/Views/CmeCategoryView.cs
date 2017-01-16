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
using Cirrious.MvvmCross.Binding.Droid.Views;
using HealthCare.Droid.Utilities;

namespace HealthCare.Droid.Views
{
    [Register("healthcare.droid.views.CmeCategoryView"), Activity()]
    public class CmeCategoryView : MvxActionBarActivity
    {
        protected override int LayoutResource {
            get { return Resource.Layout.CmeCategoryView; }
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var cmeCateListView = FindViewById<MvxListView>(Resource.Id.cmeCateListView);
            
        }
    }
}