using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare;
using HealthCare.Controls;
using HealthCare.Droid.Renderers;
using HealthCare.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(PlaceholderEditor), typeof(PlaceholderEditorRenderer))]
namespace HealthCare.Droid.Renderers
{
    public class PlaceholderEditorRenderer : EditorRenderer
    {
        public PlaceholderEditorRenderer()
        {
        }

        protected override void OnElementChanged(
            ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = e.NewElement as PlaceholderEditor;
                this.Control.Hint = element.Placeholder;
                Control.TextSize =(float) HcStyles.FontSizeContent - 2;
                Control.SetBackgroundColor(Android.Graphics.Color.Rgb(228, 228, 228));
            }
        }

        protected override void OnElementPropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == PlaceholderEditor.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as PlaceholderEditor;
                this.Control.Hint = element.Placeholder;
            }
        }
    }
}