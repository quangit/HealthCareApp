using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Geolocator.Plugin;
using HealthCare.Services.Interfaces;
using Xamarin.Forms.Maps;

namespace HealthCare.ViewModels
{
    public class DoctorLocationViewModel : BaseViewModel<DoctorLocationViewModel>
    {
        public readonly Position DefaultPostion = new Position(16.0710218, 108.21358539999997);
        private Position _currentLocation;
        private RelayCommand _getCurrentLocationCommand;

        public DoctorLocationViewModel(INavigationService navigationService) : base(navigationService)
        {
            GetCurrentLocation();
        }

        public Position CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                RaisePropertyChanged("CurrentLocation");
            }
        }

        public ICommand GetCurrentLocationCommand => _getCurrentLocationCommand ??
                                                     (_getCurrentLocationCommand = new RelayCommand(GetCurrentLocation))
            ;

        public async void GetCurrentLocation()
        {
            //try
            //{
            //    var locator = CrossGeolocator.Current;
            //    locator.DesiredAccuracy = 50;
            //    var position = await locator.GetPositionAsync(10000);
            //    CurrentLocation = new Position(position.Latitude, position.Longitude);
            //}
            //catch (Exception)
            //{
            //    CurrentLocation = DefaultPostion;
            //}
        }
    }
}