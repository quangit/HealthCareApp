using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class RatingControl : Frame
    {
        public static readonly BindableProperty ValueProperty =
           BindableProperty.Create<RatingControl, double>(p => p.Value, 0d, BindingMode.TwoWay);

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}
