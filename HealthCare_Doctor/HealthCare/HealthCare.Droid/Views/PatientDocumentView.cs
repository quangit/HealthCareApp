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

namespace HealthCare.Droid.Views
{
     [Activity(Label = "")]
    public class PatientDocumentView : MvxActionBarActivity
    {
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.PatientDocumentView;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

        }
    }
}