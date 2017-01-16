using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace HealthCare.iOS
{
    public static class Utils
    {
        public static DateTime NSDateToDateTime(this NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

        public static NSDate DateTimeToNSDate(this DateTime date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - reference).TotalSeconds);
        }

        public static UIKit.UIColor ConvertFormColorToIoSColor(Xamarin.Forms.Color color)
        {
            byte a = (byte)(color.A * 255d);
            byte g = (byte)(color.G * 255d);
            byte r = (byte)(color.R * 255d);
            byte b = (byte)(color.B * 255d);
            return UIKit.UIColor.FromRGBA(r, g, b, a);
        }

        public static byte[] ToNSData(this UIImage image)
        {

            if (image == null)
            {
                return null;
            }
            NSData data = null;

            try
            {
                data = image.AsPNG();
                return data.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                if (data != null)
                {
                    data.Dispose();
                    data = null;
                }
            }
        }

        public static UIImage ToImage(this byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            UIImage image = null;
            try
            {
                image = new UIImage(NSData.FromArray(data));
                data = null;
            }
            catch (Exception)
            {
                return null;
            }
            return image;
        }

        public static byte[] ToBytes(this UIImage image)
        {
            using (NSData imageData = image.AsPNG())
            {
                var myByteArray = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));
                return myByteArray;
            }
        }

        public static UIColor ToUIColor(this string hexString)
        {
            hexString = hexString.Replace("#", "");

            if (hexString.Length == 3)
                hexString = hexString + hexString;

            if (hexString.Length != 6)
                throw new Exception("Invalid hex string");

            int red = Int32.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int green = Int32.Parse(hexString.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int blue = Int32.Parse(hexString.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

            return UIColor.FromRGB(red, green, blue);
        }
    }
}