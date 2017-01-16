using System;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class LoadingContentPage : ContentPage
    {
        /// <summary>
        ///     The Text state property.
        /// </summary>
        public static readonly BindableProperty IsRunningProperty =
            BindableProperty.Create<LoadingContentPage, bool>(
                p => p.IsRunning, false, BindingMode.TwoWay, propertyChanged: OnIsRunningPropertyChanged);

        private readonly Grid _grdContainer;
        private readonly BusyControl _indicator;

        public LoadingContentPage()
        {
            _indicator = new BusyControl();
            //grdContainer = new AbsoluteLayout();
            _grdContainer = new Grid
            {
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                }
            };
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control is Text.
        /// </summary>
        /// <value>The Text state.</value>
        public bool IsRunning
        {
            get { return (bool) GetValue(IsRunningProperty); }
            set
            {
                if (Common.OS == TargetPlatform.iOS)
                {
                    SetValue(IsRunningProperty, value);
                    IsRunningChanged.Invoke(this, value);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        SetValue(IsRunningProperty, value);
                        IsRunningChanged.Invoke(this, value);
                    });
                }
            }
        }

        /// <summary>
        ///     The Text changed event.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsRunningChanged;

        /// <summary>
        ///     Called when [checked property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">if set to <c>true</c> [oldvalue].</param>
        /// <param name="newvalue">if set to <c>true</c> [newvalue].</param>
        private static void OnIsRunningPropertyChanged(BindableObject bindable, bool oldvalue, bool newvalue)
        {
            //Task.Factory.StartNew(() =>
            //{
            //    var control = (LoadingContentPage)bindable;
            //    control.IsRunning = newvalue;
            //    control.indicator.IsVisible = newvalue;
            //    return true;
            //}, TaskCreationOptions.DenyChildAttach);

            var control = (LoadingContentPage) bindable;
            control.IsRunning = newvalue;

            control._indicator.IsVisible = newvalue;
        }

        protected override void OnChildAdded(Element child)
        {
            if (child != _grdContainer)
            {
                Content = _grdContainer;

                if (child is Layout)
                    _grdContainer.Children.Add((Layout) child);
                else if (child is ListView)
                    _grdContainer.Children.Add((ListView) child);
                else if (child is ScrollView)
                    _grdContainer.Children.Add((ScrollView) child);

                _grdContainer.Children.Add(_indicator);
            }
            else
            {
                base.OnChildAdded(child);
            }
        }
    }
}