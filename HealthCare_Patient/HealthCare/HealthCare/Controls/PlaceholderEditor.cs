using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCare
{
    public class PlaceholderEditor : Editor
    {
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create<PlaceholderEditor, string>(view => view.Placeholder, String.Empty);

        public double FontSize { get; set; }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
    }

}
