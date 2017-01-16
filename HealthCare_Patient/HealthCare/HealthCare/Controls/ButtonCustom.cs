using System;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class ButtonCustom : Button
    {
        public const long LongDelay = 30000000;
        public const long ShortDelay = 15000000;

        public static readonly BindableProperty IsDisabledProperty =
            BindableProperty.Create<ButtonCustom, bool>(
                p => p.IsDisabled, false, BindingMode.OneWay, propertyChanged: OnDisableStateChanged);

        private readonly long _Delay;
        private bool _CanClick = true;
        private string _WPText;
        public Action<string> WPTextChangeAction;

        public ButtonCustom()
        {
            _Delay = ShortDelay;
            Clicked += (sender, e) =>
            {
                if (_CanClick)
                {
                    Clicked -= _UserClicked;
                    _CanClick = false;
                    Device.StartTimer(new TimeSpan(_Delay), () =>
                    {
                        Clicked += _UserClicked;
                        _CanClick = true;
                        return false;
                    });
                }
            };
        }

        public ButtonCustom(long delay = ShortDelay)
        {
            _Delay = delay;
            Clicked += (sender, e) =>
            {
                if (_CanClick)
                {
                    Clicked -= _UserClicked;
                    _CanClick = false;
                    Device.StartTimer(new TimeSpan(_Delay), () =>
                    {
                        Clicked += _UserClicked;
                        _CanClick = true;
                        return false;
                    });
                }
            };
        }

        public bool IsDisabled
        {
            get { return (bool) GetValue(IsDisabledProperty); }
            set { SetValue(IsDisabledProperty, value); }
        }

        public string ImageBackGround { get; set; }
        public ImageAlign ImageAlign { get; set; }
        public Thickness TextPadding { get; set; }

        /// <summary>
        ///     This property just use for WP
        /// </summary>
        public string WPText
        {
            get { return _WPText; }
            set
            {
                _WPText = value;
                if (WPTextChangeAction != null)
                    WPTextChangeAction(WPText);
            }
        }

        private event EventHandler _UserClicked;

        public event EventHandler SingleClicked
        {
            add
            {
                _UserClicked = value;
                Clicked += _UserClicked;
            }
            remove
            {
                Clicked -= _UserClicked;
                _UserClicked = null;
            }
        }

        private static void OnDisableStateChanged(BindableObject bindable, bool oldvalue, bool newvalue)
        {
            ((Button) bindable).IsEnabled = newvalue;
            ((Button) bindable).Opacity = newvalue ? 1 : 0.5;
        }
    }

    public enum ImageAlign
    {
        ToLeft,
        ToRight,
        Behind
    }
}