using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Color;
using HealthCare.Core.Utils;

namespace HealthCare.Core.Converters
{
    public class ExaminationStatusValueConverter : MvxColorValueConverter
    {

        protected override MvxColor Convert(object value, object parameter, CultureInfo culture)
        {
            //Like Button
            var ret = "#000";

            switch ((int)value)
            {
                case 1: // Approve
                    ret = "#FFF47521";
                    return Parse(ret);
                case 2: // Approved
                    ret = "#FFD81474"; // disable like
                    return Parse(ret);
                case 3: // Cancel
                    ret = "#FF25BEBE";
                    return Parse(ret);
            }

            return Parse(ret);
        }

        private static MvxColor Parse(string color)
        {

            var offset = color.StartsWith("#") ? 1 : 0;

            var a = Byte.Parse(color.Substring(0 + offset, 2), NumberStyles.HexNumber);
            var r = Byte.Parse(color.Substring(2 + offset, 2), NumberStyles.HexNumber);
            var g = Byte.Parse(color.Substring(4 + offset, 2), NumberStyles.HexNumber);
            var b = Byte.Parse(color.Substring(6 + offset, 2), NumberStyles.HexNumber);

            return new MvxColor(r, g, b, a);
        }

     
    }
}
