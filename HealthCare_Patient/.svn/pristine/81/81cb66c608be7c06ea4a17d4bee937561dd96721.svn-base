using System;
using System.Collections.ObjectModel;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Resx;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class PickerCustom<T> : BasePickerCustom
    {
        private static int MaxCharacters = Common.OnPlatform<int>(18, 20, 35);
        private static int MaxCharactersSchedule = Common.OnPlatform<int>(30, 20, 35);

        public PickerCustom()
        {
            BackgroundColor = Color.Transparent;
            EmptyText = AppResources.empty;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Unfocused += OnSelectedIndexChanged;
            TextColor = Color.Black;
        }

        #region Property

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<PickerCustom<T>, ObservableCollection<T>>(o => o.ItemsSource,
                new ObservableCollection<T>(), BindingMode.TwoWay,
                propertyChanged: OnItemsSourceChanged);

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<PickerCustom<T>, T>(o => o.SelectedItem, default(T), BindingMode.TwoWay,
                propertyChanged: OnSelectedItemChanged);

        public static BindableProperty RefreshProperty = BindableProperty.Create<PickerCustom<T>, bool>(
            x => x.RefreshList, true, BindingMode.TwoWay,
            propertyChanging:
                (bindable, oldValue, newValue) =>
                {
                    //OnItemsSourceChanged(bindable, null, HospitalViewModel.Instance.ListHospitalSupported);
                });

        public bool IsValid { get; set; }

        public bool RefreshList
        {
            get { return (bool)GetValue(RefreshProperty); }
            set { SetValue(RefreshProperty, value); }
        }

        public ObservableCollection<T> ItemsSource
        {
            get { return (ObservableCollection<T>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public T SelectedItem
        {
            get { return (T)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string EmptyText { get; set; }
        public string TitleText { get; set; }

        #endregion

        #region Method

        protected override void OnParentSet()
        {
            base.OnParentSet();
            OnItemsSourceChanged(this, null, ItemsSource);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, ObservableCollection<T> oldvalue,
            ObservableCollection<T> newvalue)
        {
            var picker = bindable as PickerCustom<T>;
            picker.Items.Clear();
            if (newvalue != null)
            {
                //now it works like "subscribe once" but you can improve
                foreach (var item in newvalue)
                {
                    picker.Items.Add(StringToFake(item));
                }

                if (picker.Items.Count <= 0)
                {
                    if (Common.OS == TargetPlatform.WinPhone)
                    {
                        picker.Items.Add(picker.EmptyText);
                        picker.SelectedIndex = 0;
                        picker.IsValid = false;
                        picker.Title = picker.Items[picker.SelectedIndex];
                    }
                    else
                    {
                        picker.SelectedIndex = -1;
                    }

                    picker.Title = picker.EmptyText;

                    picker.IsEnabled = false;
                }
                else
                {
                    if (picker.SelectedItem == null)
                    {
                        picker.SelectedItem = newvalue[0];
                        picker.SelectedIndex = 0;
                    }
                    else
                    {
                        var indexSelectingItem = picker.Items.IndexOf(StringToFake(picker.SelectedItem));
                        if (indexSelectingItem >= 0)
                        {
                            picker.SelectedIndex = indexSelectingItem;
                        }
                        else
                        {
                            picker.SelectedItem = newvalue[0];
                            picker.SelectedIndex = 0;
                        }
                    }
                    picker.IsValid = true;
                    picker.IsEnabled = true;

                    picker.Title = picker.TitleText;
                }
            }
            else
            {
                if (Common.OS == TargetPlatform.WinPhone)
                {
                    picker.Items.Add(picker.EmptyText);
                    picker.SelectedIndex = 1;
                    picker.IsValid = false;
                    picker.Title = picker.Items[picker.SelectedIndex];
                }
                else
                {
                    picker.SelectedIndex = -1;
                }

                picker.Title = picker.EmptyText;

                picker.IsEnabled = false;
            }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            try
            {
                if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
                    SelectedItem = default(T);
                else
                    SelectedItem = ItemsSource != null && ItemsSource.Count > SelectedIndex
                        ? ItemsSource[SelectedIndex]
                        : default(T);
            }
            catch (ArgumentOutOfRangeException)
            {
                SelectedItem = default(T);
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, T oldvalue, T newvalue)
        {
            var picker = bindable as PickerCustom<T>;
            if (newvalue != null && picker != null)
            {
                picker.SelectedIndex = picker.Items.IndexOf(StringToFake(newvalue));
            }
        }

        private static string StringToFake(T item)
        {
            var realString = ItemToString(item);

            var max = MaxCharacters;
            if (item is ScheduleModel)
                max = MaxCharactersSchedule;
            
            if (realString?.Length > max)
                return realString.Substring(0, MaxCharacters) + "...";
            return realString;
        }

        private static string ItemToString(T item)
        {
            if (item is string)
                return item as string;
            if (item is DistrictModel)
                return (item as DistrictModel).Name;
            if (item is CityModel)
                return (item as CityModel).Name;
            if (item is PersonModel)
            {
                var person = item as PersonModel;
                return (string.IsNullOrWhiteSpace(person.UserCode)
                    ? AppResources.patient_other
                    : (UserViewModel.Instance.CurrentUser.UserCode.Equals(person.UserCode)
                        ? AppResources.checkup_myselft + " (" + person.UserCode + ")"
                        : person.LastName + " " + person.FirstName + " (" + person.UserCode + ")")).Trim();
            }
            if (item is HospitalModel)
                return (item as HospitalModel).Name;
            return item.ToString().Trim();
        }

        #endregion
    }
}