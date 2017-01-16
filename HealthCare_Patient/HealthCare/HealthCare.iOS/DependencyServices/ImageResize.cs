using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using HealthCare.DependencyServices;
using HealthCare.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResize))]
namespace HealthCare.iOS.DependencyServices
{
    public class ImageResize : IImageResize
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            UIImage originalImage = ImageFromByteArray(imageData);
            //
            var _height = originalImage.Size.Height;
            var _width = originalImage.Size.Width;

            if (originalImage.Size.Width <= 720)
            {
                return imageData;
            }
            else
            {
                if (originalImage.Size.Width > 720)
                {
                    _height = originalImage.Size.Height;
                    _width = originalImage.Size.Width;
                    _height = _height / (_width / 720);
                    _width = 720;
                }
            }

            UIGraphics.BeginImageContext(new SizeF((float)_width, (float)_height));
            originalImage.Draw(new RectangleF(0, 0, (float)_width, (float)_height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            //
            var bytesImagen = resizedImage.AsJPEG().ToArray();
            resizedImage.Dispose();
            return bytesImagen;
        }

        public byte[] ResizeImage2(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            UIImage originalImage = ImageFromByteArray(imageData);
            //
            var _height = originalImage.Size.Height;
            var _width = originalImage.Size.Width;

            if (originalImage.Size.Width <= width)
            {
                return imageData;
            }
            else
            {
                if (originalImage.Size.Width > width)
                {
                    _height = originalImage.Size.Height;
                    _width = originalImage.Size.Width;
                    _height = _height / (_width / width);
                    _width = width;
                }
            }

            UIGraphics.BeginImageContext(new SizeF((float)_width, (float)_height));
            originalImage.Draw(new RectangleF(0, 0, (float)_width, (float)_height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            //
            var bytesImagen = resizedImage.AsJPEG().ToArray();
            resizedImage.Dispose();
            return bytesImagen;
        }

        public byte[] ResizeImageAvatar(byte[] imageData, float width, float height)
        {
            //using (MemoryStream streamIn = new MemoryStream(imageData))
            //{
            //    var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
            //    if ((width + height) >= (exif.Width + exif.Height)) return imageData;
            //}
            //// Load the bitmap
            //UIImage originalImage = ImageFromByteArray(imageData);
            ////
            //UIGraphics.BeginImageContext(new SizeF((float)width, (float)height));
            //originalImage.Draw(new RectangleF(0, 0, (float)width, (float)height));
            //var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            //UIGraphics.EndImageContext();
            ////
            //var bytesImagen = resizedImage.AsJPEG().ToArray();
            //resizedImage.Dispose();
            //return bytesImagen;

            // Load the bitmap
            UIImage originalImage = ImageFromByteArray(imageData);
            //
            var _height = originalImage.Size.Height;
            var _width = originalImage.Size.Width;

            if (originalImage.Size.Width <= 256)
            {
                return imageData;
            }
            else
            {
                if (originalImage.Size.Width > 256)
                {
                    _height = originalImage.Size.Height;
                    _width = originalImage.Size.Width;
                    _height = _height / (_width / 256);
                    _width = 256;
                }
            }

            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
                if ((_width + _height) >= (exif.Width + exif.Height)) return imageData;
            }

            UIGraphics.BeginImageContext(new SizeF((float)_width, (float)_height));
            originalImage.Draw(new RectangleF(0, 0, (float)_width, (float)_height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            //
            var bytesImagen = resizedImage.AsJPEG().ToArray();
            resizedImage.Dispose();
            return bytesImagen;
        }

        //
        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            //
            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}
