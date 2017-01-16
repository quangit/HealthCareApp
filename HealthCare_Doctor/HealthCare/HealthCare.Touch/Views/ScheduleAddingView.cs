
using System;

using Foundation;
using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CoreGraphics;
using HealthCare.Touch.Utilities;
using HealthCare.Touch.Controls;
using System.Linq;
using HealthCare.Core.Models;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Resources;

namespace HealthCare.Touch.Views
{
    public partial class ScheduleAddingView : BaseViewController
    {
        public ScheduleAddingView() : base("ScheduleAddingView", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var vm = this.ViewModel as ScheduleAddingViewModel;
            this.AddBindings(new Dictionary<object, string> {
                { this.HospitalLabel, "Text [ScheduleAdding_HosTitle]" },
                { this.DateLabel, "Text [ScheduleAdding_DateTitle]" },
                { this.StartTimeLabel, "Text [ScheduleAdding_StartTitle]" },
                { this.EndTimeLabel, "Text [ScheduleAdding_EndTitle]" },
                { this.RepeatLabel, "Text [ScheduleAdding_RepeatTitle]" },
                { this.QualityLabel, "Text [ScheduleAdding_AppointmentTitle]" },
                { this.DateTF, "Text CurrentDate,Converter=StringFormat,ConverterParameter=d"},
                { this.StartTimeTF, "Text StartTime,Converter=StringFormat,ConverterParameter=HH:mm"},
                { this.EndTimeTF, "Text EndTime,Converter=StringFormat,ConverterParameter=HH:mm"},
                { this.WeekCountLabel, "Text [ScheduleAdding_WeekCount]"},
                { this.WeekCountTF, "Text WeekCount;Placeholder [ScheduleAdding_WeekCount]"},
				{ this.SaveButton,"TouchUpInside SaveCommand"},
				{ this.QualityTF,"Text Quality" }



            }
            );

			//this.QualityTF.Text = vm.Quality.ToString();
			//this.QualityTF.ValueChanged += (s, e) =>
			//{
			//    int t = 0;
			//    if (int.TryParse(this.QualityTF.Text, out t))
			//    {
			//        QualityButton.Value = vm.Quality = t;
			//    }
			//};
			//QualityTF.ShouldChangeCharacters += (textField, range, replacement) =>
			//{
			//    return false;
			//};
			QualityTF.ShouldBeginEditing = new UITextFieldCondition(delegate (UITextField tf){
				return false;
			});
            this.QualityButton.MinimumValue = 1;
            this.QualityButton.MaximumValue = 1000;
            this.QualityButton.ValueChanged += (s, e) =>
            {
				
                //if (this.QualityButton.Value > 12)
                //    this.QualityButton.Value = 12;
                this.QualityTF.Text = this.QualityButton.Value.ToString();
                vm.Quality = (int)QualityButton.Value;
            };

            SetListPicker(this.CreateBindingSet<ScheduleAddingView, ScheduleAddingViewModel>(),
                this.HospitalTF,
                "Hospitals",
                "Hospital",
                "Hospital.Name");

            var datePicker = new UIDatePicker(CGRect.Empty);
            datePicker.Mode = UIDatePickerMode.Date;
            datePicker.Date = Util.DateTimeToNSDate(vm.CurrentDate);
            datePicker.ValueChanged +=
                (s, e) => { ((ScheduleAddingViewModel)ViewModel).CurrentDate = Util.NSDateToDateTime(datePicker.Date); };
            DateTF.InputView = datePicker;

            var dateTimePicker = new UIDatePicker(CGRect.Empty);

            dateTimePicker.Date = Util.DateTimeToNSDate(vm.StartTime.Value.Subtract(TimeZoneInfo.Local.BaseUtcOffset));
            //dateTimePicker.TimeZone = NSTimeZone.FromGMT (0);
            dateTimePicker.Mode = UIDatePickerMode.Time;
            dateTimePicker.ValueChanged +=
                (s, e) =>
                {
                    vm.StartTime = Util.NSDateToDateTime(dateTimePicker.Date);
                    //StartTimeTF.Text = vm.StartTime.Value.ToString("HH:mm");
                };
            StartTimeTF.InputView = dateTimePicker;

            var endTimePicker = new UIDatePicker(CGRect.Empty);
            endTimePicker.Mode = UIDatePickerMode.Time;

            endTimePicker.Date = Util.DateTimeToNSDate(vm.EndTime.Value.Subtract(TimeZoneInfo.Local.BaseUtcOffset));
            endTimePicker.ValueChanged +=
                (s, e) =>
                {
                    vm.EndTime = Util.NSDateToDateTime(endTimePicker.Date);
                    //EndTimeTF.Text = vm.EndTime.Value.ToString("HH:mm");
                };
            EndTimeTF.InputView = endTimePicker;

            var list = vm.DaySource.Select(x => x.ToString()).ToList();

            UpdateText(vm);
            RepeatTF.ClearsOnBeginEditing = true;
            RepeatTF.ShouldBeginEditing = field =>
            {

                var multiPicker = new MultiPicker(new PickerViewModel() { Items = list });
                multiPicker.View.Frame = View.Bounds;
                if (vm.DayOfWeeks != null)
                {
                    multiPicker.ViewModel.SelectedItems = vm.DayOfWeeks.Select(x => (int)x - 1).ToList();
                }
                multiPicker.Picked += (sender1, PickerItems) =>
                {
                    if (PickerItems != null)
                        vm.DayOfWeeks = PickerItems.Select(x => (DoctorDayOfWeek)(x + 1)).ToArray();

                    UpdateText(vm);
                    ModalBgView.RemoveFromSuperview();
                };
                ShowModal(multiPicker.View, true, () =>
                {
                    UpdateText(vm);
                });
                return false;
            };

            Title = Core.Resources.AppResources.ScheduleAdding_Title;
            WeekCountTF.ShouldBeginEditing = field =>
            {

				_alliancePicker = MyPicker.Create(AppResources.ScheduleAdding_WeekCount, this, vm.Weeks.Select(x => x.ToString()).ToList(), (newText, index) =>
                {
                    vm.WeekCount = vm.Weeks[index];
                });
                return false;
            };

            var refreshButton = new UIBarButtonItem(new UIImage("refresh.png"), UIBarButtonItemStyle.Plain, null);
            refreshButton.Clicked += (s, e) =>
            {
                ((ScheduleAddingViewModel)ViewModel).RefreshCommand.Execute();
            };
            SetRightBarButtonItems(new[] { refreshButton });
            // Perform any additional setup after loading the view, typically from a nib.
			SetTFsBorders();
        }

		private void SetTFsBorders(){
			SetTFBorders(HospitalTF);
			SetTFBorders(QualityTF);
			SetTFBorders (DateTF);
			SetTFBorders (StartTimeTF);
			SetTFBorders (EndTimeTF);
			SetTFBorders (WeekCountTF);
			SetTFBorders (RepeatTF);

		}
		private void SetTFBorders(UITextField tf){
			var color =  Util.UIColorFromHex("#FF2CBE71").CGColor;
			tf.Layer.BorderColor = color; 
			tf.Layer.BorderWidth = 2f;
			RepeatTF.Layer.BorderColor = color;
			RepeatTF.Layer.BorderWidth = 2f;

		}

        private void UpdateText(ScheduleAddingViewModel vm)
        {
            if (vm.DayOfWeeks != null && vm.DayOfWeeks.Length > 0)
            {
                RepeatTF.Text = vm.DayOfWeeks.Select((x) => x.ToResourceString()).Aggregate((x, y) => x + ", " + y);
                // DateTF.Enabled = false;
                WeekCountTF.Enabled = true;
            }
            else
            {
                //DateTF.Enabled = true;
                WeekCountTF.Enabled = false;
                RepeatTF.Text = AppResources.ScheduleAdding_OnlyOne;
            }
        }

        private void SetListPicker(MvxFluentBindingDescriptionSet<ScheduleAddingView, ScheduleAddingViewModel> bindingSet,
            UITextField tf,
            string itemsSourcePath,
            string selectedItemPath,
            string textPath)
        {
            var picker = new UIPickerView();

            var pickerViewModel = new MvxPickerViewModel(picker);

            tf.InputView = picker;
            picker.Model = pickerViewModel;
            picker.ShowSelectionIndicator = true;
            bindingSet.Bind(pickerViewModel).For(p => p.ItemsSource).To(itemsSourcePath);
            bindingSet.Bind(pickerViewModel).For(p => p.SelectedItem).To(selectedItemPath);
            bindingSet.Bind(tf).To(textPath);
            bindingSet.Apply();
            //View.AddGestureRecognizer(new UITapGestureRecognizer(() => tf.ResignFirstResponder()));
        }
    }
}

