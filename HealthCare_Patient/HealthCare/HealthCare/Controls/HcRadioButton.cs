using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Resx;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class HcRadioButton<T> : StackLayout
    {
        private readonly List<HcImageButton> _listImageButton;

        public readonly Thickness HcRadioPadding = new Thickness(0, Common.OnPlatform<double>(0, 0, -12), 0,
            Common.OnPlatform<double>(0, 0, -32));

        public readonly double SizeImageInButton = Device.OnPlatform<double>(18, 18, 25);
        public readonly double WidthButton = Device.OnPlatform<double>(80, 80, 150);

        public HcRadioButton()
        {
            _listImageButton = new List<HcImageButton>();
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = VerticalOptions = LayoutOptions.FillAndExpand;
            Padding = HcRadioPadding;
            Spacing = 0;
        }

        public void InitView()
        {
            Children.Clear();
            if (ListItem != null)
            {
                _listImageButton.Clear();
                foreach (var item in ListItem)
                {
                    var text = ItemToString(item);
                    var icon = text.Equals(ItemToString(SelectedItem))
                        ? "ic_radio_checked.png"
                        : "ic_radio_uncheck.png";
                    var button = new HcImageButton
                    {
                        Text = text,
                        Image = icon,
                        TextColor = Color.Black,
                        ButtonHorizontalOptions = LayoutOptions.FillAndExpand,
                        FontAttributes = FontAttributes.None,
                        WidthRequest = WidthButton,
                        ImageWidth = SizeImageInButton,
                        ImageHeight = SizeImageInButton
                    };

                    button.Clicked += ClickEventHandler;
                    Children.Add(button);
                    _listImageButton.Add(button);
                }
            }
        }

        #region Property

        public T SelectedItem
        {
            get { return (T)GetValue(SelectedItemProperty); }

            set
            {
                if (SelectedItem == null || !SelectedItem.Equals(value))
                {
                    SetValue(SelectedItemProperty, value);
                }
            }
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create<HcRadioButton<T>, T>(
                p => p.SelectedItem, default(T), BindingMode.TwoWay, propertyChanged: OnSelectedItemPropertyChanged);

        public ObservableCollection<T> ListItem
        {
            get { return (ObservableCollection<T>)GetValue(ListItemProperty); }
            set { SetValue(ListItemProperty, value); }
        }

        public static readonly BindableProperty ListItemProperty =
            BindableProperty.Create<HcRadioButton<T>,
                ObservableCollection<T>>(p => p.ListItem, new ObservableCollection<T>(), BindingMode.Default,
                    propertyChanged:
                        (bindable, value, newValue) => { ((HcRadioButton<T>)bindable).InitView(); });

        #endregion

        #region Method

        private static void OnSelectedItemPropertyChanged(BindableObject bindable, T oldvalue, T newvalue)
        {
            var radioGroup = (HcRadioButton<T>)bindable;
            radioGroup.SelectedItem = newvalue;
            if (radioGroup.ListItem?.Count > 0)
            {
                HcImageButton oldImageButton = null;

                if (radioGroup.ListItem.Contains(oldvalue))
                    oldImageButton = radioGroup._listImageButton[radioGroup.ListItem.IndexOf(oldvalue)];

                HcImageButton newImageButton = null;
                if (radioGroup.ListItem.Contains(newvalue))
                    newImageButton = radioGroup._listImageButton[radioGroup.ListItem.IndexOf(newvalue)];

                if (oldImageButton != null)
                    oldImageButton.Image = "ic_radio_uncheck.png";
                if (newImageButton != null)
                    newImageButton.Image = "ic_radio_checked.png";
            }
        }

        private string ItemToString(T item)
        {
            if (item == null)
                return string.Empty;
            if (item is string)
                return item.ToString();
            if (item is Gender)
            {
                Gender r = Gender.Male;
                Enum.TryParse(item.ToString(), true, out r);
                return r == Gender.None ? "" : r == Gender.Male ? AppResources.male : AppResources.female;
            }
            if (item is MaritalStatus)
            {
                MaritalStatus r = MaritalStatus.Single;
                Enum.TryParse(item.ToString(), true, out r);
                return r == MaritalStatus.Single ? AppResources.single : AppResources.married;
            }
            if (item is Enum)
                return Description.ToString(item as Enum);
            return "";
        }

        private void ClickEventHandler(object sender, EventArgs e)
        {
            var index = _listImageButton.IndexOf((HcImageButton)((ButtonCustom)sender).Parent);
            SelectedItem = ListItem[index];
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            InitView();
        }

        #endregion
    }
}