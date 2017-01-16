using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class CreditCardView : Frame
    {
        public static BindableProperty NumberProperty =
            BindableProperty.Create<CreditCardView, string>(ctrl => ctrl.Number, string.Empty, BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (CreditCardView) bindable;
                    ctrl.Number = newValue;
                });

        public static BindableProperty DescriptionProperty =
            BindableProperty.Create<CreditCardView, string>(ctrl => ctrl.Description, string.Empty, BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (CreditCardView) bindable;
                    ctrl.Description = newValue;
                });

        public static BindableProperty LogoProperty =
            BindableProperty.Create<CreditCardView, ImageSource>(ctrl => ctrl.Logo, "", BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (CreditCardView) bindable;
                    ctrl.Logo = newValue;
                });

        public CreditCardView()
        {
            InitializeComponent();
        }

        public string Number
        {
            get { return (string) GetValue(NumberProperty); }
            set
            {
                SetValue(NumberProperty, value);
                idNumber.Text = value;
            }
        }

        public string Description
        {
            get { return (string) GetValue(DescriptionProperty); }
            set
            {
                SetValue(DescriptionProperty, value);
                idDes.Text = value;
            }
        }

        public ImageSource Logo
        {
            get { return (string) GetValue(LogoProperty); }
            set
            {
                SetValue(LogoProperty, value);
                idLogo.Source = value;
            }
        }

        public void SetClickEvent(TapGestureRecognizer e)
        {
            if (idBound.GestureRecognizers.Count == 0)
                idBound.GestureRecognizers.Add(e);
        }
    }
}