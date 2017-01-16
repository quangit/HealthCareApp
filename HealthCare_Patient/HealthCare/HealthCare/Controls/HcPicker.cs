using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class HcPicker<T> : StackLayout
    {
        // Picker with bottom line and green triangle
        public const int Style1 = 1;
        // Picker with green triangle
        public const int Style2 = 2;
        // Picker with black triangle
        public const int Style3 = 3;
        // Picker with arrow and border
        public const int Style4 = 4;
        public readonly string ArrowTriangle = "ic_arrow_dropdown.png";
        public readonly string BlackTriangle = "ic_triangle_black.png";
        public readonly string GreenTriangle = "ic_triangle_green.png";
        public readonly double HeightPicker = Device.OnPlatform<double>(28, 38, 80);
        public readonly double HeightTriangle = Device.OnPlatform<double>(12, 12, 14);
        private ContentView _border;
        private bool _isLoaded;
        private ContentView _line;
        private Image _triangle;

        public HcPicker()
        {
            InitView();
            PickerCustom.BindingContext = this;
            PickerCustom.SetBinding(PickerCustom<T>.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
            PickerCustom.SetBinding(PickerCustom<T>.ItemsSourceProperty, "ItemsSource", BindingMode.TwoWay);
            BindingContextChanged += (sender, args) =>
            {
                if (!_isLoaded && Loaded != null)
                {
                    _isLoaded = true;
                    Loaded.Invoke(this, EventArgs.Empty);
                }
            };
            Spacing = 0;
        }

        public PickerCustom<T> PickerCustom { get; set; }
        public event EventHandler Loaded;

        #region Property

        public int PickerStyle
        {
            set
            {
                switch (value)
                {
                    case Style1:
                        _line.IsVisible = true;
                        _triangle.Source = GreenTriangle;
                        break;
                    case Style2:
                        _triangle.Source = GreenTriangle;
                        break;
                    case Style3:
                        _triangle.Source = BlackTriangle;
                        break;
                    case Style4:
                        _border.Padding = 1;
                        _triangle.Source = ArrowTriangle;
                        break;
                }
            }
        }

        public string EmptyText
        {
            get { return PickerCustom.EmptyText; }
            set { PickerCustom.EmptyText = value; }
        }

        public string TitleText
        {
            get { return PickerCustom.TitleText; }
            set { PickerCustom.TitleText = value; }
        }

        public double FontSize
        {
            get { return PickerCustom.FontSize; }
            set { PickerCustom.FontSize = value; }
        }

        public ObservableCollection<T> ItemsSource
        {
            get { return (ObservableCollection<T>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        //public int TextLenght
        //{
        //    get { return PickerCustom.TextLenght; }
        //    set { if (value > 0) PickerCustom.TextLenght = value; }
        //}

        public T SelectedItem
        {
            get { return (T)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<HcPicker<T>, ObservableCollection<T>>(o => o.ItemsSource,
                new ObservableCollection<T>(), BindingMode.TwoWay,
                propertyChanged: OnItemsSourceChanged);

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create<HcPicker<T>, T>(o => o.SelectedItem, default(T), BindingMode.TwoWay,
                propertyChanged:
                    (bindable, value, newValue) =>
                    {
                        Common.DoTaskWithDelay(() =>
                        {
                            ((HcPicker<T>)bindable).SelectedItemChangedCommand?.Execute(newValue);
                        });
                    });

        #region SelectedItemChangedCommand

        /// <summary>
        ///     Identifies the <see cref="SelectedItemChangedCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedItemChangedCommandProperty =
            BindableProperty.Create<HcPicker<T>, ICommand>(p => p.SelectedItemChangedCommand, default(ICommand),
                BindingMode.Default);

        private StackLayout _mainContent;

        /// <summary>
        ///     Gets or sets the <see cref="SelectedItemChangedCommand" /> property. This is a bindable property.
        /// </summary>
        /// <value>
        /// </value>
        public ICommand SelectedItemChangedCommand
        {
            get { return (ICommand)GetValue(SelectedItemChangedCommandProperty); }
            set { SetValue(SelectedItemChangedCommandProperty, value); }
        }

        #endregion

        #endregion

        #region Method

        private static void OnItemsSourceChanged(BindableObject bindable,
            ObservableCollection<T> oldvalue, ObservableCollection<T> newvalue)
        {
            oldvalue = newvalue;
        }

        private void InitView()
        {
            _mainContent = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, Common.OnPlatform<double>(0, 0, -20), 0, 0),
                BackgroundColor = Color.White
            };
            _border = new ContentView
            {
                Padding = 0,
                BackgroundColor = HcStyles.BorderGrayColor,
            };
            _line = new ContentView
            {
                HeightRequest = 1,
                BackgroundColor = HcStyles.GreenLineColor,
                IsVisible = false
            };
            _triangle = new Image
            {
                HeightRequest = HeightTriangle,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };

            PickerCustom = new PickerCustom<T>
            {
                HeightRequest = HeightPicker,
                FontSize =
                    Common.OnPlatform(HcStyles.FontSizeSubContent - 2, HcStyles.FontSizeSubContent - 2,
                        HcStyles.FontSizeContent - 2),
                TextColor = HcStyles.BlackTextColor
            };

            var contentViewTriangle = new ContentView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = _triangle,
                Padding = new Thickness(0, Common.OnPlatform<double>(0, 0, 20), Common.OnPlatform<double>(5, 10, 6), 0)
            };
            _mainContent.Children.Add(PickerCustom);
            _mainContent.Children.Add(contentViewTriangle);
            _border.Content = _mainContent;
            Children.Add(_border);
            Children.Add(_line);

            BackgroundColor = Color.White;

           _mainContent.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new RelayCommand(() =>
                {
                    if (PickerCustom.ItemsSource?.Count > 0)
                        PickerCustom.Focus();
                })
            });
        }

        #endregion
    }
}