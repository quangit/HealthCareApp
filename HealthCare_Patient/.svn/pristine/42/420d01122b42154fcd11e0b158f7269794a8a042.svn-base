using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace HealthCare.Models
{
    public class GalleryImage : ObservableObject
    {
        private ImageSource _source;

        public string ImageId
        {
            get;
            set;
        }

        public ImageSource Source
        {
            get
            {
                if (_source == null)
                {
                    if (ImageId != null)
                    {
                        return  new UriImageSource() {Uri =  new Uri(ImageId)};
                    }
                }
                return _source;
            }
            set { _source = value; }
        }

        public byte[] OrgImage
        {
            get;
            set;
        }

    }
}