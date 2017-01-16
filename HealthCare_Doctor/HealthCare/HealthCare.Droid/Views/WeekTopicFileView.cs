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
using HealthCare.Core.ViewModels;
using HealthCare.Core.Resources;
using System.Diagnostics;

namespace HealthCare.Droid.Views
{
    [Register("healthcare.droid.views.WeekTopicFileView"), Activity()]
    public class WeekTopicFileView : MvxActionBarActivity
    {
        protected override int LayoutResource
        {
            get { return Resource.Layout.WeekTopicFileView; }
        }

     

        private Action TopicItemClick;
        private WeekTopicFileViewModel _vm;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetIcon(Resource.Drawable.logo_bs);

            SupportActionBar.Title = AppResources.ApplicationTitle;
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			_vm = (WeekTopicFileViewModel)ViewModel;
           
            
            TopicItemClick = () => {

                string url = _vm.SelectedTopic.FileUri;

                url = url.Replace("https://", "http://");

                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, "application/pdf");
                StartActivity(intent);
            };

            _vm.TopicSelected = TopicItemClick;


        }




        }
}