using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace HealthCare.Phone.Views
{
    public partial class UpdateProfileView
    {
        private DatePickerCustom datePicker;

        private PhotoChooserTask photoChooserTask;
        public UpdateProfileView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (datePicker == null)
            {
                datePicker = new DatePickerCustom();
                datePicker.IsTabStop = false;
                datePicker.MaxHeight = 0;
                datePicker.ValueChanged += DatePicker_ValueChanged;

                LayoutRoot.Children.Add(datePicker);
            }

            ListPicker.SelectionChanged += ListPicker_SelectionChanged;
            //ListPickerGender.ItemsSource = new string[] { AppResources.SignUp_Male, AppResources.SignUp_Female };
        }

        private void ListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListPicker.Tag != null && (string)ListPicker.Tag == "City")
            {
                var vm = (ViewModel as UpdateProfileViewModel);
                vm.Account.City = ListPicker.SelectedItem as City;
                if (vm.Account.District != null && vm.Account.District.CityId != vm.Account.CityId)
                {
                    vm.Account.District = null;
                }
            }

            if (ListPicker.Tag != null && (string)ListPicker.Tag == "District")
            {
                var vm = (ViewModel as UpdateProfileViewModel);
                vm.Account.District = ListPicker.SelectedItem as District;
            }

            if (ListPicker.Tag != null && (string)ListPicker.Tag == "CheckUp")
            {
                var vm = (ViewModel as UpdateProfileViewModel);
                vm.Account.CheckupType = ListPicker.SelectedItem as CheckupType;
            }
        }

        private void DatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            (ViewModel as UpdateProfileViewModel).Account.BirthDate = datePicker.Value.Value;
        }

        private void ImageTab(object sender, System.Windows.Input.GestureEventArgs e)
        {

            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.PixelWidth = 500;
            photoChooserTask.PixelHeight = 500;

            photoChooserTask.ShowCamera = true;
            photoChooserTask.Show();
        }

        private async void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.ChosenPhoto != null)
            {
                try
                {
                    RotateImageControl.ChosenPhotoCompleted = new TaskCompletionSource<WriteableBitmap>();
                    RotateImageControl.ChosenPhoto = e.ChosenPhoto;
                    NavigationService.Navigate(new Uri("/Views/RotateImageControl.xaml", UriKind.Relative));

                    await RotateImageControl.ChosenPhotoCompleted.Task;

                    if (RotateImageControl.ChosenPhotoCompleted.Task.IsCompleted)
                    {
                        var bitmapSource = RotateImageControl.ChosenPhotoCompleted.Task.Result;
                        var vm = (ViewModel as UpdateProfileViewModel);


                        imageProfile.ImageSource = bitmapSource;

                        MemoryStream pixelStream = new MemoryStream();
                        bitmapSource.SaveJpeg(pixelStream, 200, 200, 0, 100);
                        pixelStream.Position = 0;
                        vm.Account.Images = SignUpView.ReadToEnd(pixelStream);
                    }
                }
                catch (Exception)
                {
                    //throw;
                }
            }
        }

        private void BirthdayTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var updateProfileViewModel = ViewModel as UpdateProfileViewModel;
            if (updateProfileViewModel != null)
                datePicker.Value = updateProfileViewModel.Account.BirthDate ?? DateTime.Now;
            datePicker.ClickTemplateButton();
        }

        private void GenderTap(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as UpdateProfileViewModel);
            vm.Account.Gender = new Gender() { IsFemale = !vm.Account.Gender.IsFemale };
        }

        private void CityTab(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as UpdateProfileViewModel);
            ListPicker.Tag = "City";
            ListPicker.FullModeHeader = AppResources.SignUp_City;
            ListPicker.DisplayMemberPath = "Name";



            try
            {
                if (vm.Account.City != null)
                {
                    var s = vm.Cities.FirstOrDefault(x => x.Id == vm.Account.City.Id);
                    ListPicker.ItemsSource = vm.Cities;
                    if (s != null)
                        vm.Account.City = s;
                    ListPicker.SelectedItem = vm.Account.City;
                }
                else
                {
                    ListPicker.ItemsSource = vm.Cities;
                }
            }
            catch (Exception)
            {
                //throw;
            }

            ListPicker.Open();
        }

        private void DistrictTap(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as UpdateProfileViewModel);
            if (vm.Account.City != null)
            {
                ListPicker.Tag = "District";
                ListPicker.FullModeHeader = AppResources.SignUp_District;
                ListPicker.DisplayMemberPath = "Name";

                try
                {
                    if (vm.Account.District != null)
                    {
                        var city = vm.Cities.FirstOrDefault(x => x.Id == vm.Account.City.Id);
                        if (city != null)
                            vm.Account.City = city;
                        var s = vm.Account.City.Districts.FirstOrDefault(x => x.Id == vm.Account.District.Id);
                        ListPicker.ItemsSource = vm.Account.City.Districts;
                        if (s != null)
                            vm.Account.District = s;
                        ListPicker.SelectedItem = vm.Account.District;
                    }
                    else { ListPicker.ItemsSource = vm.Account.City.Districts; }
                }
                catch (Exception)
                {
                    // ignored
                }
                ListPicker.Open();
            }
        }

        private void CheckUpTap(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as UpdateProfileViewModel);
            ListPicker.DisplayMemberPath = "Name";
            ListPicker.Tag = "CheckUp";
            ListPicker.FullModeHeader = AppResources.SignUp_CheckUp;
            try
            {

                if (vm.Account.CheckupType != null)
                {
                    var s = vm.CheckupTypes.FirstOrDefault(x => x.Id == vm.Account.CheckupType.Id);
                    ListPicker.ItemsSource = vm.CheckupTypes;
                    if (s != null)
                        vm.Account.CheckupType = s;
                    ListPicker.SelectedItem = vm.Account.CheckupType;
                }
                else
                    ListPicker.ItemsSource = vm.CheckupTypes;
            }
            catch (Exception)
            {
                //throw;
            }
            ListPicker.Open();
        }

        private void CallSupportButton(object sender, RoutedEventArgs e)
        {
            HomeView.CallSupport();
        }
    }
}