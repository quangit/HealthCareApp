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
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExifLib;
using FFImageLoading;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices.CropPhoto;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PhotoEditDependencyService))]
namespace HealthCare.Droid.DependencyServices.CropPhoto
{
    public class PhotoEditDependencyService : IPhotoEdit
    {
        public const string IMAGE_BYTE_ARRAY = "IMAGE_BYTE_ARRAY";
        public void LaunchEditorControl(byte[] imageByteArray)
        {
            var activity = Forms.Context as MainActivity;
            Intent photoEditIntent = new Intent(activity, typeof(CropImageActivity));
            //photoEditIntent.PutExtra(IMAGE_BYTE_ARRAY, imageByteArray);
            activity.StartActivity(photoEditIntent);
        }

        public byte[] CorrectOrientation(byte[] imageByteArray)
        {
            return BitmapToByteArray(GetRightOrientationBitmap(imageByteArray));
        }


        public static Bitmap GetRightOrientationBitmap(byte[] byteArray)
        {
            JpegInfo exif = null;
            using (MemoryStream streamIn = new MemoryStream(byteArray))
            {
                exif = ExifLib.ExifReader.ReadJpeg(streamIn);
            }

            Bitmap bm = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);

            Matrix matrix = new Matrix();
            int rotate = 0;


            //int orientation = ei.GetAttributeInt(ExifInterface.TAG_ORIENTATION, ExifInterface.ORIENTATION_UNDEFINED);

            //            int orientation = 0;//ei.getAttributeInt(ExifInterface.TAG_ORIENTATION, ExifInterface.ORIENTATION_UNDEFINED);
            //
            //            switch (orientation)
            //            {
            //                case ExifInterface.ORIENTATION_ROTATE_90:
            //                    rotateImage(bitmap, 90);
            //                    break;
            //                case ExifInterface.ORIENTATION_ROTATE_180:
            //                    rotateImage(bitmap, 180);
            //                    break;
            //                    // etc.
            //            }

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                rotate = GetOrientation(byteArray);
            }
            else {
                if (exif.Orientation == ExifOrientation.TopRight)
                {
                    rotate = 90;
                }
                else if (exif.Orientation == ExifOrientation.BottomRight)
                {
                    rotate = 180;
                }
                else if (exif.Orientation == ExifOrientation.BottomLeft)
                {
                    rotate = 270;
                }
            }

            if (rotate > 0)
            {
                matrix.PostRotate(rotate);

                BitmapFactory.Options bmpFactoryOptions = new BitmapFactory.Options();
                bmpFactoryOptions.InJustDecodeBounds = true;
                BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length, bmpFactoryOptions);

                int heightRatio = (int)Math.Ceiling(bmpFactoryOptions.OutHeight / (float)600);
                int widthRatio = (int)Math.Ceiling(bmpFactoryOptions.OutWidth / (float)800);

                if (heightRatio > 1 || widthRatio > 1)
                {
                    if (heightRatio > widthRatio)
                    {
                        bmpFactoryOptions.InSampleSize = heightRatio;
                    }
                    else {
                        bmpFactoryOptions.InSampleSize = widthRatio;
                    }
                }

                bmpFactoryOptions.InJustDecodeBounds = false;

                bm = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length, bmpFactoryOptions);

                using (MemoryStream streamOut = new MemoryStream())
                {
                    Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, matrix, false)
                         .Compress(Bitmap.CompressFormat.Jpeg, 100, streamOut); // rotating bitmap

                    var arr = streamOut.ToArray();
                    return BitmapFactory.DecodeByteArray(arr, 0, arr.Length);
                }
            }
            else
                return bm;
        }

        public static Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            if (byteArray != null)
            {
                var imageBitmap = BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length);
                //RunOnUiThread(() => avatarImageView.SetImageBitmap(imageBitmap));
                return imageBitmap;
            }
            return null;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                bitmapData = stream.ToArray();
            }
            return bitmapData;
        }





        #region Get Orienttation

        // Returns the degrees in clockwise. Values are 0, 90, 180, or 270.
        public static int GetOrientation(byte[] jpeg)
        {
            if (jpeg == null)
            {
                return 0;
            }

            int offset = 0;
            int length = 0;

            // ISO/IEC 10918-1:1993(E)
            while (offset + 3 < jpeg.Length && (jpeg[offset++] & 0xFF) == 0xFF)
            {
                int marker = jpeg[offset] & 0xFF;

                // Check if the marker is a padding.
                if (marker == 0xFF)
                {
                    continue;
                }
                offset++;

                // Check if the marker is SOI or TEM.
                if (marker == 0xD8 || marker == 0x01)
                {
                    continue;
                }
                // Check if the marker is EOI or SOS.
                if (marker == 0xD9 || marker == 0xDA)
                {
                    break;
                }

                // Get the length and check if it is reasonable.
                length = pack(jpeg, offset, 2, false);
                if (length < 2 || offset + length > jpeg.Length)
                {
                    // Log.e(TAG, "Invalid length");
                    return 0;
                }

                // Break if the marker is EXIF in APP1.
                if (marker == 0xE1 && length >= 8 &&
                        pack(jpeg, offset + 2, 4, false) == 0x45786966 &&
                        pack(jpeg, offset + 6, 2, false) == 0)
                {
                    offset += 8;
                    length -= 8;
                    break;
                }

                // Skip other markers.
                offset += length;
                length = 0;
            }

            // JEITA CP-3451 Exif Version 2.2
            if (length > 8)
            {
                // Identify the byte order.
                int tag = pack(jpeg, offset, 4, false);
                if (tag != 0x49492A00 && tag != 0x4D4D002A)
                {
                    //Log.e(TAG, "Invalid byte order");
                    return 0;
                }
                bool littleEndian = (tag == 0x49492A00);

                // Get the offset and check if it is reasonable.
                int count = pack(jpeg, offset + 4, 4, littleEndian) + 2;
                if (count < 10 || count > length)
                {
                    //Log.e(TAG, "Invalid offset");
                    return 0;
                }
                offset += count;
                length -= count;

                // Get the count and go through all the elements.
                count = pack(jpeg, offset - 2, 2, littleEndian);
                while (count-- > 0 && length >= 12)
                {
                    // Get the tag and check if it is orientation.
                    tag = pack(jpeg, offset, 2, littleEndian);
                    if (tag == 0x0112)
                    {
                        // We do not really care about type and count, do we?
                        int orientation = pack(jpeg, offset + 8, 2, littleEndian);
                        switch (orientation)
                        {
                            case 1:
                                return 0;
                            case 3:
                                return 180;
                            case 6:
                                return 90;
                            case 8:
                                return 270;
                        }
                        //Log.i(TAG, "Unsupported orientation");
                        return 0;
                    }
                    offset += 12;
                    length -= 12;
                }
            }

            //Log.i(TAG, "Orientation not found");
            return 0;
        }

        private static int pack(byte[] bytes, int offset, int length,
                bool littleEndian)
        {
            int step = 1;
            if (littleEndian)
            {
                offset += length - 1;
                step = -1;
            }

            int value = 0;
            while (length-- > 0)
            {
                value = (value << 8) | (bytes[offset] & 0xFF);
                offset += step;
            }
            return value;
        }

        #endregion

    }
}