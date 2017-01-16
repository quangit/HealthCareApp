using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class HealthCareCheckBox : StackLayout
    {
        private readonly string _ImgCheckBoxOff;
        private readonly string _ImgCheckBoxOn;
        private bool _IsChecked;
        public TapGestureRecognizer ClickEvent;

        public HealthCareCheckBox(string labelText, int? imgSize, string srcImgOn = "checkbox_on"
            , string srcImgOff = "checkbox_off", bool isCheck = false, Color? labelColor = null)
        {
            InitializeComponent();

            _ImgCheckBoxOff = srcImgOff;
            _ImgCheckBoxOn = srcImgOn;
            idText.Text = labelText;
            idText.TextColor = labelColor ?? Color.White;
            idImg.HeightRequest = imgSize ?? 16;
            idImg.HeightRequest = imgSize ?? 16;
            IsCheck = isCheck;
            ClickEvent = new TapGestureRecognizer();
            ClickEvent.Tapped += (s, e) => { IsCheck = !IsCheck; };
            GestureRecognizers.Add(ClickEvent);
        }

        public bool IsCheck
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                if (_IsChecked)
                    idImg.Source = _ImgCheckBoxOn;
                else
                    idImg.Source = _ImgCheckBoxOff;
            }
        }
    }
}