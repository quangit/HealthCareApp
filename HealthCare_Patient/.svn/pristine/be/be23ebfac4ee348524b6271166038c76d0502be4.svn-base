using HealthCare.Helpers;
using HealthCare.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HealthCare.Controls
{
    public class HcMap : Map
    {
        public static readonly BindableProperty DoctorNameProperty =
            BindableProperty.Create<HcMap, string>(p => p.DoctorName, string.Empty, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) => { ((HcMap)bindable)._doctorPin.Label = newValue; });

        public static readonly BindableProperty DoctorAddressProperty =
            BindableProperty.Create<HcMap, string>(p => p.DoctorAddress, string.Empty, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) => { ((HcMap)bindable)._doctorPin.Address = newValue; });

        public static readonly BindableProperty DoctorPositionProperty =
            BindableProperty.Create<HcMap, Position>(p => p.DoctorPosition, new Position(), BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var map = (HcMap)bindable;
                    map._doctorPin.Position = newValue;
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(map._doctorPin.Position, Distance.FromMiles(0.9)));
                });

        public static readonly BindableProperty PatientPositionProperty =
            BindableProperty.Create<HcMap, Position>(p => p.PatientPosition, new Position(), BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var map = (HcMap)bindable;
                    map._patientPin.Position = newValue;
                });

        private readonly Pin _doctorPin;
        private readonly Pin _patientPin;

        public HcMap()
        {
            IsShowingUser = Common.OS != TargetPlatform.Windows && Common.OS != TargetPlatform.WinPhone;
            _doctorPin = new Pin
            {
                Label = DoctorName,
                Address = DoctorAddress,
                Type = PinType.Place,
                Position = DoctorPosition
            };

            _patientPin = new Pin
            {
                Label = AppResources.your_position,
                Type = PinType.Generic,
                Position = PatientPosition
            };
            
            if (Common.OS == TargetPlatform.Windows || Common.OS == TargetPlatform.WinPhone)
                this.Pins.Add(_patientPin);

            Pins.Add(_doctorPin);
        }

        public Position DoctorPosition
        {
            get { return (Position)GetValue(DoctorPositionProperty); }
            set { SetValue(DoctorPositionProperty, value); }
        }

        public Position PatientPosition
        {
            get { return (Position)GetValue(PatientPositionProperty); }
            set { SetValue(PatientPositionProperty, value); }
        }

        public string DoctorName
        {
            get { return (string)GetValue(DoctorNameProperty); }
            set { SetValue(DoctorNameProperty, value); }
        }

        public string DoctorAddress
        {
            get { return (string)GetValue(DoctorAddressProperty); }
            set { SetValue(DoctorAddressProperty, value); }
        }
    }
}