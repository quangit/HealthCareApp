using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using Cirrious.CrossCore.Droid;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Droid.Views;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Uri = Android.Net.Uri;

namespace HealthCare.Droid.Utilities
{
    public class PictureHelpers
    {
        const int MaxPixelDimension = 1000;
        const int Quality = 100;
        public static MemoryStream LoadInMemoryBitmap(Uri uri)
        {
            var memoryStream = new MemoryStream();
            var bitmap = LoadScaledBitmap(uri);
            if (bitmap == null)
                return null;
            using (bitmap)
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, Quality, memoryStream);
            }
            memoryStream.Seek(0L, SeekOrigin.Begin);
            return memoryStream;
        }

        public static Bitmap LoadScaledBitmap(Uri uri)
        {
            ContentResolver contentResolver = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext.ContentResolver;
            var maxDimensionSize = GetMaximumDimension(contentResolver, uri);
            var sampleSize = (int)Math.Ceiling((maxDimensionSize) /
                ((double)MaxPixelDimension));
            if (sampleSize < 1)
            {
                // this shouldn't happen, but if it does... then trace the error and set sampleSize to 1
                //MvxTrace.Trace(
                 //   "Warning - sampleSize of {0} was requested - how did this happen - based on requested {1} and returned image size {2}",
                //    sampleSize,
                //    MaxPixelDimension,
                //    maxDimensionSize);
                // following from https://github.com/MvvmCross/MvvmCross/issues/565 we return null in this case
                // - it suggests that Android has returned a corrupt image uri
                return null;
            }
            var sampled = LoadResampledBitmap(contentResolver, uri, sampleSize);
            try
            {
                var rotated = ExifRotateBitmap(contentResolver, uri, sampled);
                return rotated;
            }
            catch (Exception)
            {
                //Mvx.Trace("Problem seem in Exit Rotate {0}", pokemon.ToLongString());
                return sampled;
            }
        }

        private static Bitmap LoadResampledBitmap(ContentResolver contentResolver, Android.Net.Uri uri, int sampleSize)
        {
            using (var inputStream = contentResolver.OpenInputStream(uri))
            {
                var optionsDecode = new BitmapFactory.Options { InSampleSize = sampleSize };

                return BitmapFactory.DecodeStream(inputStream, null, optionsDecode);
            }
        }

        private static int GetMaximumDimension(ContentResolver contentResolver, Android.Net.Uri uri)
        {
            using (var inputStream = contentResolver.OpenInputStream(uri))
            {
                var optionsJustBounds = new BitmapFactory.Options
                {
                    InJustDecodeBounds = true
                };
                var metadataResult = BitmapFactory.DecodeStream(inputStream, null, optionsJustBounds);
                var maxDimensionSize = Math.Max(optionsJustBounds.OutWidth, optionsJustBounds.OutHeight);
                return maxDimensionSize;
            }
        }

        private static Bitmap ExifRotateBitmap(ContentResolver contentResolver, Android.Net.Uri uri, Bitmap bitmap)
        {
            if (bitmap == null)
                return null;

            var exif = new Android.Media.ExifInterface(GetRealPathFromUri(contentResolver, uri));
            var rotation = exif.GetAttributeInt(Android.Media.ExifInterface.TagOrientation, (Int32)Android.Media.Orientation.Normal);
            var rotationInDegrees = ExifToDegrees(rotation);
            if (rotationInDegrees == 0)
                return bitmap;

            using (var matrix = new Matrix())
            {
                matrix.PreRotate(rotationInDegrees);
                return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
            }
        }

        private static String GetRealPathFromUri(ContentResolver contentResolver, Uri uri)
        {
            var proj = new String[] { MediaStore.Images.ImageColumns.Data };
            using (var cursor = contentResolver.Query(uri, proj, null, null, null))
            {
                var columnIndex = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Data);
                cursor.MoveToFirst();
                return cursor.GetString(columnIndex);
            }
        }

        private static Int32 ExifToDegrees(Int32 exifOrientation)
        {
            switch (exifOrientation)
            {
                case (Int32)Android.Media.Orientation.Rotate90:
                    return 90;
                case (Int32)Android.Media.Orientation.Rotate180:
                    return 180;
                case (Int32)Android.Media.Orientation.Rotate270:
                    return 270;
            }

            return 0;
        }

    }
}