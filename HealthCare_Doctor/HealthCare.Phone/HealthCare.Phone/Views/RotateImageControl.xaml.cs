using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using HealthCare.Core.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;

namespace HealthCare.Phone.Views
{
    public partial class RotateImageControl : PhoneApplicationPage
    {
        public RotateImageControl()
        {
            InitializeComponent();
        }

        private WriteableBitmap bitmap;
        public static TaskCompletionSource<WriteableBitmap> ChosenPhotoCompleted { get; set; }
        public static Stream ChosenPhoto { get; set; }
        private Color r = Colors.Transparent;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemTray.IsVisible = false;

            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.SignUp_SaveButton.ToLower();
            (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.ImageRotate.ToLower();

            if (App.IsDarkThemeUsed)
            {
                LayoutRoot.Background = new SolidColorBrush(Colors.Black);

                SystemTray.SetBackgroundColor(this, Colors.Transparent);
            }
            BitmapSource bitmapSource = new BitmapImage();
            bitmapSource.SetSource(ChosenPhoto);
            bitmap = new WriteableBitmap(bitmapSource);

            MainImage.Source = bitmapSource;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SystemTray.IsVisible = true;
        }

        private void ApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            var c = MainImage.RenderTransform as CompositeTransform;
            if (c != null)
            {
                c.Rotation = c.Rotation - 90;

            };
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (RotateImageControl.ChosenPhotoCompleted != null && e.NavigationMode != NavigationMode.New)
            {
                if (!RotateImageControl.ChosenPhotoCompleted.Task.IsCompleted && !RotateImageControl.ChosenPhotoCompleted.Task.IsCanceled)
                {
                    RotateImageControl.ChosenPhotoCompleted.TrySetCanceled();
                }
            }
            base.OnNavigatedFrom(e);
        }

        private void Done_Click(object sender, EventArgs e)
        {
            if (RotateImageControl.ChosenPhotoCompleted != null)
            {
                var c = MainImage.RenderTransform as CompositeTransform;
                if (c != null)
                {
                    bitmap = bitmap.RotateFree(c.Rotation);
                }
                RotateImageControl.ChosenPhotoCompleted.TrySetResult(bitmap);
                NavigationService.GoBack();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (RotateImageControl.ChosenPhotoCompleted != null)
                RotateImageControl.ChosenPhotoCompleted.TrySetCanceled();
        }
    }
}