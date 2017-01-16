using System;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class ImageRounderCorner : Frame
    {

        public static BindableProperty SourceProperty =
            BindableProperty.Create<ImageRounderCorner, ImageSource>(ctrl => ctrl.Source, null,
                BindingMode.Default, propertyChanged: (bindable, value, newValue) =>
                {
                    var v = bindable as ImageRounderCorner;
                    if (v?.img != null)
                    {
                        v.img.Source = newValue;
                    }
                });


        public static BindableProperty UriProperty =
            BindableProperty.Create<ImageRounderCorner, string>(ctrl => ctrl.Uri, null,
                BindingMode.Default, propertyChanged: (bindable, value, newValue) =>
                {
                    var v = bindable as ImageRounderCorner;
                    if (v?.img != null)
                    {
                        v.Uri = newValue;
                        v.img.Source = newValue;
                    }
                });

        public ImageRounderCorner()
        {
            InitializeComponent();
            if ((string)GetValue(UriProperty) != null)
                img.Source = (string)GetValue(UriProperty);
            else if ((ImageSource) GetValue(SourceProperty) != null)
                img.Source = (ImageSource) GetValue(SourceProperty);
            else img.Source = null;
        }

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}