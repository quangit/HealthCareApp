using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ExifLib;
using HealthCare.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.WinPhone.Renderer
{
    public partial class WPCropPhotoControl : UserControl
    {
        WriteableBitmap WB_CapturedImage;//for original image
        WriteableBitmap WB_CroppedImage;//for cropped image
        //Variables for the crop feature
        Point Point1, Point2;
        private JpegInfo exif;

        public WPCropPhotoControl()
        {
            InitializeComponent();
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            PhotoEditViewModel.Instance.CropEvent += CropBtn_Click;
        }

        public WPCropPhotoControl(byte[] byteArray)
        {
            InitializeComponent();
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            PhotoEditViewModel.Instance.CropEvent += CropBtn_Click;
            SetImageSource(byteArray);
        }

        public void SetImageSource(byte[] byteArray)
        {
            using (MemoryStream streamIn = new MemoryStream(byteArray))
            {
                exif = ExifLib.ExifReader.ReadJpeg(streamIn);
            }

            using (MemoryStream streamIn = new MemoryStream(byteArray))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(streamIn);
                WB_CapturedImage = new WriteableBitmap(bitmapImage);
            }

            OriginalImage.Source = WB_CapturedImage;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //Used for rendering the cropping rectangle on the image.
            rect.SetValue(Canvas.LeftProperty, (Point1.X < Point2.X) ? Point1.X : Point2.X);
            rect.SetValue(Canvas.TopProperty, (Point1.Y < Point2.Y) ? Point1.Y : Point2.Y);
            rect.Width = (int)Math.Abs(Point2.X - Point1.X);
            rect.Height = (int)Math.Abs(Point2.Y - Point1.Y);
        }
        //Mouse Move
        private void OriginalImage_MouseMove(object sender, MouseEventArgs e)
        {
            Point2 = e.GetPosition(OriginalImage);
        }
        //Mouse Up
        private void OriginalImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point2 = e.GetPosition(OriginalImage);
        }
        //Mouse Down
        private void OriginalImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point1 = e.GetPosition(OriginalImage);//Set first touchable cordinates as point1
            Point2 = Point1;
            rect.Visibility = Visibility.Visible;
        }


        private void CropBtn_Click(object sender, EventArgs e)
        {
            //if (Point1.X == 0 || Point1.Y == 0 || Point2.X == 0 || Point2.Y == 0)
            if ((Point1.X == 0 && Point2.X == 0) || (Point1.Y == 0 && Point2.Y == 0))
            {
            }
            else
            {
                try
                {
                    // Get the size of the source image
                    double originalImageWidth = WB_CapturedImage.PixelWidth;
                    double originalImageHeight = WB_CapturedImage.PixelHeight;

                    // Get the size of the image when it is displayed on the phone
                    double displayedWidth = OriginalImage.ActualWidth;
                    double displayedHeight = OriginalImage.ActualHeight;

                    // Calculate the ratio of the original image to the displayed image
                    //double widthRatio = originalImageWidth / displayedWidth;
                    //double heightRatio = originalImageHeight / displayedHeight;

                    double widthRatio = originalImageWidth / displayedWidth;
                    double heightRatio = originalImageHeight / displayedHeight;

                    // Create a new WriteableBitmap. The size of the bitmap is the size of the cropping rectangle
                    // drawn by the user, multiplied by the image size ratio.
                    WB_CroppedImage = new WriteableBitmap((int)(widthRatio * Math.Abs(Point2.X - Point1.X)),
                        (int)(heightRatio * Math.Abs(Point2.Y - Point1.Y)));

                    // Calculate the offset of the cropped image. This is the distance, in pixels, to the top left corner
                    // of the cropping rectangle, multiplied by the image size ratio.
                    int xoffset = (int)(((Point1.X < Point2.X) ? Point1.X : Point2.X) * widthRatio);
                    int yoffset = (int)(((Point1.Y < Point2.Y) ? Point1.Y : Point2.X) * heightRatio);

                    // Copy the pixels from the targeted region of the source image into the target image, 
                    // using the calculated offset
                    if (WB_CroppedImage.Pixels.Length > 0)
                    {
                        for (int i = 0; i < WB_CroppedImage.Pixels.Length; i++)
                        {
                            int x = (int)((i % WB_CroppedImage.PixelWidth) + xoffset);
                            int y = (int)((i / WB_CroppedImage.PixelWidth) + yoffset);
                            WB_CroppedImage.Pixels[i] = WB_CapturedImage.Pixels[y * WB_CapturedImage.PixelWidth + x];
                        }
                    }

                    byte[] byteArray = new byte[0];
                    using (MemoryStream stream = new MemoryStream())
                    {
                        WB_CroppedImage.SaveJpeg(stream, WB_CroppedImage.PixelWidth, WB_CroppedImage.PixelHeight, 0, 100);
                        stream.Close();

                        byteArray = stream.ToArray();
                    }

                    UserViewModel.Instance.UpdateAvatar(byteArray);
                    //PhotoEditViewModel.Instance.GoBack();
                }
                catch { PhotoEditViewModel.Instance.GoBack(); }
            }
        }

    }
}
