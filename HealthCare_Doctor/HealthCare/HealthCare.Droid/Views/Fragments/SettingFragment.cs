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
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using HealthCare.Core.ViewModels;

namespace HealthCare.Droid.Views.Fragments
{
    [Register("healthcare.droid.views.fragments.SettingFragment")]
    public class SettingFragment : MvxFragment
    {
        private View _view;
        private HomeViewModel _vm;
          public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
          {
              var ignore = base.OnCreateView(inflater, container, savedInstanceState);

              _view = this.BindingInflate(Resource.Layout.SettingFragment, null);
              _vm = (HomeViewModel)ViewModel;

              var profileLayout = _view.FindViewById<LinearLayout>(Resource.Id.profileLayout);

              profileLayout.Click += (sender, args) =>
              {
                  _vm.UpdateProfileCommand.Execute(null);
              };

              

              var PushToggle = _view.FindViewById<Switch>(Resource.Id.PushToggle);
              PushToggle.Checked = _vm.Settings.PushConsent;
              PushToggle.CheckedChange += (sender, args) =>
              {
                  _vm.Settings.PushConsent = args.IsChecked;
              };



             return _view;
          }
    }
}