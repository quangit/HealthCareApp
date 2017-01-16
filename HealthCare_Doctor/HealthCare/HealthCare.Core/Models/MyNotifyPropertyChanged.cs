using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using HealthCare.Core.Resources;

namespace HealthCare.Core.Models
{
    public class MyNotifyPropertyChanged : MvxNotifyPropertyChanged
    {
        public string this[string index]
        {
            get
            {
                //if (index.Equals("currency"))
                //return Data.currency;
                return AppResources.ResourceManager.GetString(index);
            }
        }
    }
}
