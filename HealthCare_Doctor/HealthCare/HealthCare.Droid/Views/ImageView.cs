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
    [Register("healthcare.droid.views.ImageView"), Activity(Label = "")]
    public class ImageView : MvxActionBarActivity
    {
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.ImageView;
            }
        }
    }
}