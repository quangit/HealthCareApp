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
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;

namespace HealthCare.Droid.Views
{
    public class ReplyDetailDialog : MvxDialogFragment
    {
        public override Dialog OnCreateDialog(Android.OS.Bundle savedInstanceState)
        {
            base.EnsureBindingContextSet(savedInstanceState);
            var _view = this.BindingInflate(Resource.Layout.ReplyDetailDialog, null);
			var dialog = new AlertDialog.Builder(this.Activity,Resource.Style.Theme_AppCompat_Light_Dialog);
			var layout = _view.FindViewById<LinearLayout>(Resource.Id.linearLayout);
			layout.SetMinimumWidth(Activity.Window.DecorView.Width - 50);
			layout.SetMinimumHeight(layout.MinimumWidth * 2/3);
            dialog.SetView(_view);
            var result = dialog.Create();

            var cancelBut = _view.FindViewById<Button>(Resource.Id.cancelBut);
            cancelBut.Click += (sender, args) => {
                result.Dismiss();
            };
            //var canncelButton = _view.FindViewById<Button>(Resource.Id.cancelBut);
            //canncelButton.Click += (sender, args) =>
            //{
            //    result.Dismiss();
            //};
            //var vm = ViewModel as ConsultViewModel;
            //vm.MesssageSent += (s, e) => {
            //    result.Dismiss();
            //};
            return result;
        }
    }
}