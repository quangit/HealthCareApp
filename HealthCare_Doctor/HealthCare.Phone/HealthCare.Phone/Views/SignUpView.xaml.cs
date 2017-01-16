using System;
using System.IO;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace HealthCare.Phone.Views
{
    public class DatePickerCustom : DatePicker
    {
        public void ClickTemplateButton()
        {
            var btn = (GetTemplateChild("DateTimeButton") as Button);
            if (btn == null)
                return;
            var peer = new ButtonAutomationPeer(btn);
            var provider = (peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider);

            provider.Invoke();
        }
    }

    public partial class SignUpView
    {
        private DatePickerCustom datePicker;

        private PhotoChooserTask photoChooserTask;

        public SignUpView()
        {
            InitializeComponent();
            //Loaded += SignUpView_Loaded;
        }

        private void SignUpView_Loaded(object sender, RoutedEventArgs e)
        {
            //if (datePicker == null)
            //{
            //    datePicker = new DatePickerCustom();
            //    datePicker.IsTabStop = false;
            //    datePicker.MaxHeight = 0;
            //    datePicker.ValueChanged += DatePicker_ValueChanged;

            //    LayoutRoot.Children.Add(datePicker);
            //}

            //ListPicker.SelectionChanged += ListPicker_SelectionChanged;
            ////ListPickerGender.ItemsSource = new string[] { AppResources.SignUp_Male, AppResources.SignUp_Female };
        }

        private void DatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
            (ViewModel as SignUpViewModel).Account.BirthDate = datePicker.Value.Value;
        }

        private void BirthdayTap(object sender, GestureEventArgs e)
        {
            datePicker.ClickTemplateButton();
        }

        private void GenderTap(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as SignUpViewModel);
            vm.Account.Gender = new Gender() { IsFemale = !vm.Account.Gender.IsFemale };
        }

        

        private void ListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListPicker.Tag != null && (string) ListPicker.Tag == "City")
            {
                var vm = (ViewModel as SignUpViewModel);
                vm.Account.City = ListPicker.SelectedItem as City;
                if (vm.Account.District != null && vm.Account.District.CityId != vm.Account.CityId)
                {
                    vm.Account.District = null;
                }
            }

            if (ListPicker.Tag != null && (string) ListPicker.Tag == "District")
            {
                var vm = (ViewModel as SignUpViewModel);
                vm.Account.District = ListPicker.SelectedItem as District;
            }

            if (ListPicker.Tag != null && (string) ListPicker.Tag == "CheckUp")
            {
                var vm = (ViewModel as SignUpViewModel);
                vm.Account.CheckupType = ListPicker.SelectedItem as CheckupType;
            }
        }

        private void DistrictTap(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as SignUpViewModel);
            if (vm.Account.City != null)
            {
                ListPicker.Tag = "District";
                ListPicker.FullModeHeader = AppResources.SignUp_District;
                ListPicker.DisplayMemberPath = "Name";
                ListPicker.ItemsSource = vm.Account.City.Districts;
                //ListPicker.SelectedItem = null;
                ListPicker.Open();
            }
        }

        private void CityTab(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as SignUpViewModel);
            ListPicker.Tag = "City";
            ListPicker.FullModeHeader = AppResources.SignUp_City;
            ListPicker.DisplayMemberPath = "Name";
            ListPicker.ItemsSource = vm.Cities;
            if (vm.Account.City != null)
            {
                ListPicker.SelectedItem = vm.Account.City;
            }

            ListPicker.Open();
        }

        private void CheckUpTap(object sender, GestureEventArgs e)
        {
            var vm = (ViewModel as SignUpViewModel);
            ListPicker.DisplayMemberPath = "Name";
            ListPicker.Tag = "CheckUp";
            ListPicker.FullModeHeader = AppResources.SignUp_CheckUp;
            ListPicker.ItemsSource = vm.CheckupTypes;
            ListPicker.Open();
        }
        private void ImageTab(object sender, GestureEventArgs e)
        {
            photoChooserTask = new PhotoChooserTask();
           // photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.ShowCamera = true;
            photoChooserTask.Show();
        }

        //private void photoChooserTask_Completed(object sender, PhotoResult e)
        //{
        //    if (e.ChosenPhoto != null)
        //    {
        //        var vm = (ViewModel as SignUpViewModel);
        //        vm.Account.Images = ReadToEnd(e.ChosenPhoto);
        //        BitmapSource bitmapSource = new BitmapImage();
        //        bitmapSource.SetSource(e.ChosenPhoto);
        //        imageProfile.Source = bitmapSource;
        //    }
        //}

        public static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var readBuffer = new byte[4096];

                var totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        var nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            var temp = new byte[readBuffer.Length*2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte) nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                var buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        
    }
}