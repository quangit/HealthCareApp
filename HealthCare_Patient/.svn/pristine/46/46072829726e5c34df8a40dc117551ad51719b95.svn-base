using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(TabbedPageCustom), typeof(TabbedPageRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class TabbedPageRenderer : TabbedRenderer
    {
        private Activity _activity;
        private bool _isFirstDesign = true;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _activity = this.Context as Activity;
        }

        protected override void OnWindowVisibilityChanged(ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);
            if (_isFirstDesign)
            {
                ActionBar actionBar = _activity.ActionBar;

                ColorDrawable colorDrawable = new ColorDrawable(Color.White);
                actionBar.SetStackedBackgroundDrawable(colorDrawable);

                // TODO: set tab text color

                _isFirstDesign = false;
            }
        }
    }
}