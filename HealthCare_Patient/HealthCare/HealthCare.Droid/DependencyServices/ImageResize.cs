using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ExifLib;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices;
using HealthCare.Droid.DependencyServices.CropPhoto;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResize))]
namespace HealthCare.Droid.DependencyServices
{
    public class ImageResize : IImageResize
    {
        private bool IsRotateImage(byte[] imageData)
        {

            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
                if (exif.Orientation == ExifOrientation.TopRight)
                {
                    return true;
                }
            }
            return false;
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            bool checker = IsRotateImage(imageData);
            // Load the bitmap 
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            var _height = originalImage.Height;
            var _width = originalImage.Width;

            if (originalImage.Width <= 720)
            {
                return imageData;
            }
            else
            {
                if (originalImage.Width > 720)
                {
                    _height = originalImage.Height;
                    _width = originalImage.Width;
                    _height = _height / (_width / 720);
                    _width = 720;
                }
            }

            var rotate = 0;

            Bitmap temp = null, resizedImage = null;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                checker = true;
                rotate = PhotoEditDependencyService.GetOrientation(imageData);
            }
            else
                rotate = 90;

            if (checker && rotate > 0)
            {
                Matrix mtx = new Matrix();
                mtx.PreRotate(rotate);
                temp = Bitmap.CreateBitmap(originalImage, 0, 0, (int)originalImage.Width,
                    (int)originalImage.Height, mtx, false);
                resizedImage = Bitmap.CreateScaledBitmap(temp, (int)_height, (int)_width, false);
                mtx.Dispose();
                temp.Dispose();
                mtx = null;
            }
            else
                resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)_width, (int)_height, false);



            temp = null;

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        public byte[] ResizeImageAvatar(byte[] imageData, float width, float height)
        {
            //using (MemoryStream streamIn = new MemoryStream(imageData))
            //{
            //    var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
            //    if ((width + height) >= (exif.Width + exif.Height)) return imageData;
            //}

            //// Load the bitmap 
            //Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            //var resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
            //    return ms.ToArray();
            //}

            bool checker = IsRotateImage(imageData);
            // Load the bitmap 
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);

            var _height = originalImage.Height;
            var _width = originalImage.Width;

            if (originalImage.Width <= 256)
            {
                return imageData;
            }
            else
            {
                if (originalImage.Width > 256)
                {
                    _height = originalImage.Height;
                    _width = originalImage.Width;
                    _height = _height / (_width / 256);
                    _width = 256;
                }
            }

            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
                if ((_width + _height) >= (exif.Width + exif.Height)) return imageData;
            }

            Bitmap temp = null, resizedImage = null;

            if (checker)
            {
                Matrix mtx = new Matrix();
                mtx.PreRotate(90);
                temp = Bitmap.CreateBitmap(originalImage, 0, 0, (int)originalImage.Width, (int)originalImage.Height, mtx, false);
                resizedImage = Bitmap.CreateScaledBitmap(temp, (int)_height, (int)_width, false);
                mtx.Dispose();
                temp.Dispose();
                mtx = null;
            }
            else
                resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)_width, (int)_height, false);

            temp = null;

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        public void ResizeImage(string sourceFile, string targetFile, int reqWidth, int reqHeight)
        {
            if (!File.Exists(targetFile) && File.Exists(sourceFile))
            {
                var downImg = DecodeSampledBitmapFromFile(sourceFile, reqWidth, reqHeight);
                using (var outStream = File.Create(targetFile))
                {
                    if (targetFile.ToLower().EndsWith("png"))
                        downImg.Compress(Bitmap.CompressFormat.Png, 100, outStream);
                    else
                        downImg.Compress(Bitmap.CompressFormat.Jpeg, 95, outStream);
                }
                downImg.Recycle();
            }
        }

        public static Bitmap DecodeSampledBitmapFromFile(string path, int reqWidth, int reqHeight)
        {
            // First decode with inJustDecodeBounds=true to check dimensions
            var options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeFile(path, options);

            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;
            return BitmapFactory.DecodeFile(path, options);
        }

        public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = height / 2;
                int halfWidth = width / 2;

                // Calculate the largest inSampleSize value that is a power of 2 and keeps both
                // height and width larger than the requested height and width.
                while ((halfHeight / inSampleSize) > reqHeight
                       && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return inSampleSize;
        }
    }
}