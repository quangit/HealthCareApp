using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.Phone.Views
{
    public partial class ImageView
    {
        public ImageView()
        {
            InitializeComponent();
        }
        public static string ImageUrl { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PanZoom.Source = new BitmapImage(new Uri(ImageUrl));
            //base.OnNavigatedTo(e);
        }
    }
}