using System;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public class ImageButtonCustom : Button
    {
        public const long LongDelay = 30000000;
        public const long ShortDelay = 10000000;
        private readonly long _Delay;
        private bool _CanClick = true;
        private string _ImageSourceWP;
        private bool _IsChecked;

        public ImageButtonCustom(long delay = ShortDelay)
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

        public string Source { get; set; }

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    if (OnCheckedPropertyChanged != null)
                        OnCheckedPropertyChanged.Invoke(this, this);
                }
            }
        }

        public string ImageSourceWP
        {
            get { return _ImageSourceWP; }
            set
            {
                _ImageSourceWP = value;
                OnPropertyChanged("ImageSourceWP");
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

        public event EventHandler<ImageButtonCustom> OnCheckedPropertyChanged;
    }
}