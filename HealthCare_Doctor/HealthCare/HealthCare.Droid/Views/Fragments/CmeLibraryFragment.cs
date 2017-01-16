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
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using HealthCare.Core.ViewModels;

namespace HealthCare.Droid.Views.Fragments
{
    [Register("healthcare.droid.views.fragments.CmeLibraryFragment")]
    public class CmeLibraryFragment : MvxFragment
    {
        private View _view;
        private HomeViewModel _vm;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            _view = this.BindingInflate(Resource.Layout.CmeLibraryFragment, null);
            _vm = (HomeViewModel)ViewModel;
            var searchclassBut = _view.FindViewById<ImageButton>(Resource.Id.searchbutton);
            searchclassBut.Click += (sender, args) =>
            {
                _vm.ShowCmeSearch();
            };


            var metrics = Resources.DisplayMetrics;
            var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);


            //var cmeGridview = _view.FindViewById<MvxGridView>(Resource.Id.cmelibitemtemplate);
            


            return _view;
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
            return dp;
        }


    }
}