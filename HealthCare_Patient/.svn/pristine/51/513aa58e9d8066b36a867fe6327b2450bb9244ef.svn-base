using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.WinPhone.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Frame = Xamarin.Forms.Frame;
using Image = Xamarin.Forms.Image;
using Thickness = System.Windows.Thickness;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ImageRounderCorner), typeof(ImageRounderCornerRenderer))]
namespace HealthCare.WinPhone.Renderer
{
    public class ImageRounderCornerRenderer : FrameRenderer
    {

        protected async override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            var frame = e.NewElement as ImageRounderCorner;

            if (Control != null && Control.Background != null)
            {
                BitmapImage bitmapImage = ((ImageBrush)Control.Background).ImageSource as BitmapImage;
                if (bitmapImage != null) bitmapImage.UriSource = null;
                ((ImageBrush)Control.Background).ImageSource = null;
                Control.Background = null;
            }


            if (Control != null && frame != null)
            {
                //var border = new Border
                //{
                //    Width = 150,
                //    Height = 150,
                //    Background = new SolidColorBrush(Colors.Transparent),
                //    BorderBrush = new SolidColorBrush(Colors.Transparent),
                //    BorderThickness = new Thickness(0),
                //    CornerRadius = new CornerRadius(20)
                //};
                if (!string.IsNullOrEmpty(frame.Uri))
                {
                    BitmapImage bm = frame.Uri.StartsWith("/") ? new BitmapImage(new Uri(frame.Uri, UriKind.Relative))
                        : new BitmapImage(new Uri(frame.Uri));
                    bm.DecodePixelHeight = 48;
                    bm.DecodePixelWidth = 48;
                    Control.CornerRadius = new CornerRadius(10);

                    Control.Background = new ImageBrush()
                    {
                        Stretch = Stretch.UniformToFill,
                        ImageSource = bm
                    };
                }
                else if (frame.Source != null)
                {
                    var imgBrush = await SetSource(frame.Source);
                    var image = new ImageBrush
                    {
                        Stretch = Stretch.UniformToFill,
                        ImageSource = imgBrush
                    };

                    Control.CornerRadius = new CornerRadius(10);
                    Control.Background = image;
                }

            }
        }


        private async Task<System.Windows.Media.ImageSource> SetSource(Xamarin.Forms.ImageSource _imageSource)
        {
            if (_imageSource == null) return null;

            System.Windows.Media.ImageSource imageSource;
            try
            {
                if (_imageSource is UriImageSource)
                {
                    var handler = new ImageLoaderSourceHandler();
                    imageSource = await handler.LoadImageAsync(_imageSource, new CancellationToken());
                }
                else if (_imageSource is FileImageSource)
                {
                    var fImage = new FileImageSourceHandler();
                    imageSource = await fImage.LoadImageAsync(_imageSource, new CancellationToken());
                }
                else
                {
                    //var fImage = new StreamImagesourceHandler();
                    imageSource = await LoadImageAsync(_imageSource, new CancellationToken());
                }
            }
            catch (TaskCanceledException ex)
            {
                imageSource = (System.Windows.Media.ImageSource)null;
            }
            return imageSource;
        }

        public async Task<System.Windows.Media.ImageSource> LoadImageAsync(Xamarin.Forms.ImageSource imagesource, CancellationToken cancelationToken)
        {
            BitmapImage bitmapImage = (BitmapImage)null;
            StreamImageSource streamImageSource = imagesource as StreamImageSource;
            if (streamImageSource != null && streamImageSource.Stream != null)
            {
                using (Stream streamAsync = await streamImageSource.Stream.Invoke(cancelationToken))
                {
                    bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(streamAsync);
                }
            }
            return (System.Windows.Media.ImageSource)bitmapImage;
        }

        protected async override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Common.OS == TargetPlatform.WinPhone)
                if (e.PropertyName == "Source")
                {
                    var frame = sender as ImageRounderCorner;
                    if (frame != null)
                    {
                        var imgBrush = await SetSource(frame.Source);
                        var image = new ImageBrush
                        {
                            Stretch = Stretch.UniformToFill,
                            ImageSource = imgBrush
                        };
                        Control.CornerRadius = new CornerRadius(10);
                        Control.Background = image;
                    }
                }
        }
    }
}
