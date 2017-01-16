using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Services;

namespace HealthCare.Core.Models
{
    public class Setting : MyNotifyPropertyChanged
    {
        private bool _locationConsent;
        private string _pushRegistrationId;

        public bool LocationConsent
        {
            get { return _locationConsent; }
            set
            {
                _locationConsent = value;
                RaisePropertyChanged("LocationConsent");
            }
        }

		private bool _pushConsent;
        public bool PushConsent
        {
            get { return _pushConsent; }
            set { SetProperty(ref _pushConsent, value); }
        }

        public string PushRegistrationId
        {
            get { return _pushRegistrationId; }
            set { _pushRegistrationId = value; RaisePropertyChanged("PushRegistrationId"); }
        }
    }
}
