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
using Com.Telerik.Widget.Calendar;
using HealthCare.Droid.Utilities;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using HealthCare.Core.Models;

namespace HealthCare.Droid.Views
{
    [Register("healthcare.droid.views.SignUpView"), Activity(Label = "", WindowSoftInputMode = Android.Views.SoftInput.AdjustResize)]
    public class SignUpView : MvxActionBarActivity
    {

        private SignUpViewModel vm;
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.SignUpView;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            vm = ViewModel as SignUpViewModel;
        }

       

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                vm = (SignUpViewModel)ViewModel;
                FindViewById<ImageView>(Resource.Id.profileImage).SetImageURI(data.Data);
                //vm.OnPicture(PictureHelpers.LoadInMemoryBitmap(data.Data));
                vm.Account.Images = PictureHelpers.LoadInMemoryBitmap(data.Data).ToArray();
                FindViewById<MvxImageView>(Resource.Id.avatarImage).Visibility = ViewStates.Gone;
            }
        }

        private void OnItemSelected(object sender, AdapterView.ItemSelectedEventArgs itemSelectedEventArgs)
        {
            vm.ChangeDistrict(vm.Cities[itemSelectedEventArgs.Position].Name);
            vm.Account.City = vm.Cities[itemSelectedEventArgs.Position];
        }

    }

    public class MyDatePickerDialog : DialogFragment
    {
        public class MyDateDialog : Android.App.DatePickerDialog
        {
            private string _title;
            public MyDateDialog(Context context, EventHandler<DateSetEventArgs> callBack, int year, int monthOfYear,
                int dayOfMonth)
                : base(context, callBack, year, monthOfYear, dayOfMonth)
            {


            }

            public void SetPermanentTitle(string title)
            {
                _title = title;
                SetTitle(title);
            }

            public override void OnDateChanged(DatePicker view, int year, int month, int day)
            {
                base.OnDateChanged(view, year, month, day);
                SetTitle(_title);
            }
        }
        Action<DateTime> dateChanged;

        public MyDatePickerDialog(Action<DateTime> dateChanged)
        {
            this.dateChanged = dateChanged;
        }

        public override Android.App.Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var d = new MyDateDialog(this.Activity, OnDateSet, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            d.DatePicker.CalendarViewShown = false;
            d.SetPermanentTitle(AppResources.Dialog_SetDate);
            return d;
        }

        private void OnDateSet(object sender, Android.App.DatePickerDialog.DateSetEventArgs args)
        {
            dateChanged(args.Date);
        }
    }
}