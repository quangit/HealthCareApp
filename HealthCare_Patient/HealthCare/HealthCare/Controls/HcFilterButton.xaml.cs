using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class HcFilterButton : Button
    {
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create<HcFilterButton, bool>(p => p.IsSelected, false, BindingMode.TwoWay,
                propertyChanged:
                    (bindable, oldValue, newValue) => { ((HcFilterButton) bindable).IsSelected = newValue; });

        private bool _isSelected;

        public HcFilterButton()
        {
            InitializeComponent();
        }

        public bool IsSelected
        {
            set
            {
                if (_isSelected != value && value)
                {
                    if (Common.OS == TargetPlatform.WinPhone)
                    {
                        TextColor = Color.Black;
                    }
                    else
                    {
                        FontAttributes = FontAttributes.Bold;
                        TextColor = Color.White.MultiplyAlpha(0.5);
                    }
                }
                else if (_isSelected != value && !value)
                {
                    if (Common.OS == TargetPlatform.WinPhone)
                    {
                        TextColor = Color.White;
                    }
                    else
                    {
                        FontAttributes = FontAttributes.None;
                        TextColor = Color.White;
                    }
                }
                _isSelected = value;
            }
            get { return _isSelected; }
        }
    }
}