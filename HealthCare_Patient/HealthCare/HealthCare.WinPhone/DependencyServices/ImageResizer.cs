using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ExifLib;
using HealthCare.DependencyServices;
using HealthCare.WinPhone.DependencyServices;
using Microsoft.Phone;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResize))]
namespace HealthCare.WinPhone.DependencyServices
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
            byte[] resizedData;
            bool checker = IsRotateImage(imageData);

            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
                if (exif.Width <= 720)
                {
                    return imageData;
                }
                else
                {
                    if (exif.Width > 720)
                    {
                        height = exif.Height;
                        width = exif.Width;
                        height = height / (width / 720);
                        width = 720;
                    }

                }
            }
            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                WriteableBitmap bitmap = PictureDecoder.DecodeJpeg(streamIn, (int)width, (int)height);

                //
                float ZielHoehe = 0;
                float ZielBreite = 0;
                //
                float Hoehe = bitmap.PixelHeight;
                float Breite = bitmap.PixelWidth;
                //
                if (Hoehe > Breite) // Höhe (71 für Avatar) ist Master
                {
                    ZielHoehe = height;
                    float teiler = Hoehe / height;
                    ZielBreite = Breite / teiler;
                }
                else // Breite (61 für Avatar) ist Master
                {
                    ZielBreite = width;
                    float teiler = Breite / width;
                    ZielHoehe = Hoehe / teiler;
                }
                //                
                using (MemoryStream streamOut = new MemoryStream())
                {
                    if (!checker)
                        bitmap.SaveJpeg(streamOut, (int)ZielBreite, (int)ZielHoehe, 0, 100);
                    else
                    {
                        bitmap.Rotate(90).SaveJpeg(streamOut, (int)ZielHoehe, (int)ZielBreite, 0, 100);
                    }
                    resizedData = streamOut.ToArray();
                }
            }
            return resizedData;
        }

        public byte[] ResizeImageAvatar(byte[] imageData, float width, float height)
        {
            //byte[] resizedData;

            //using (MemoryStream streamIn = new MemoryStream(imageData))
            //{
            //    var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
            //    if ((width + height) >= (exif.Width + exif.Height)) return imageData;
            //}

            //bool checker = IsRotateImage(imageData);

            //using (MemoryStream streamIn = new MemoryStream(imageData))
            //{
            //    WriteableBitmap bitmap = PictureDecoder.DecodeJpeg(streamIn, (int)width, (int)height);

            //    //
            //    float ZielHoehe = 0;
            //    float ZielBreite = 0;
            //    //
            //    float Hoehe = bitmap.PixelHeight;
            //    float Breite = bitmap.PixelWidth;
            //    //
            //    if (Hoehe > Breite) // Höhe (71 für Avatar) ist Master
            //    {
            //        ZielHoehe = height;
            //        float teiler = Hoehe / height;
            //        ZielBreite = Breite / teiler;
            //    }
            //    else // Breite (61 für Avatar) ist Master
            //    {
            //        ZielBreite = width;
            //        float teiler = Breite / width;
            //        ZielHoehe = Hoehe / teiler;
            //    }
            //    //                
            //    using (MemoryStream streamOut = new MemoryStream())
            //    {
            //        if (!checker)
            //            bitmap.SaveJpeg(streamOut, (int)ZielBreite, (int)ZielHoehe, 0, 100);
            //        else
            //        {
            //            bitmap.Rotate(90).SaveJpeg(streamOut, (int)ZielHoehe, (int)ZielBreite, 0, 100);
            //        }
            //        resizedData = streamOut.ToArray();
            //    }
            //}
            //return resizedData;


            byte[] resizedData;

            bool checker = IsRotateImage(imageData);

            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
                if (exif.Width <= 256)
                {
                    return imageData;
                }
                else
                {
                    if (exif.Width > 256)
                    {
                        height = exif.Height;
                        width = exif.Width;
                        height = height / (width / 256);
                        width = 256;
                    }

                }
            }


            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                var exif = ExifLib.ExifReader.ReadJpeg(streamIn);
                if ((width + height) >= (exif.Width + exif.Height)) return imageData;
            }

            using (MemoryStream streamIn = new MemoryStream(imageData))
            {
                WriteableBitmap bitmap = PictureDecoder.DecodeJpeg(streamIn, (int)width, (int)height);

                //
                float ZielHoehe = 0;
                float ZielBreite = 0;
                //
                float Hoehe = bitmap.PixelHeight;
                float Breite = bitmap.PixelWidth;
                //
                if (Hoehe > Breite) // Höhe (71 für Avatar) ist Master
                {
                    ZielHoehe = height;
                    float teiler = Hoehe / height;
                    ZielBreite = Breite / teiler;
                }
                else // Breite (61 für Avatar) ist Master
                {
                    ZielBreite = width;
                    float teiler = Breite / width;
                    ZielHoehe = Hoehe / teiler;
                }
                //                
                using (MemoryStream streamOut = new MemoryStream())
                {
                    if (!checker)
                        bitmap.SaveJpeg(streamOut, (int)ZielBreite, (int)ZielHoehe, 0, 100);
                    else
                    {
                        bitmap.Rotate(90).SaveJpeg(streamOut, (int)ZielHoehe, (int)ZielBreite, 0, 100);
                    }
                    resizedData = streamOut.ToArray();
                }
            }
            return resizedData;
        }
    }
}
