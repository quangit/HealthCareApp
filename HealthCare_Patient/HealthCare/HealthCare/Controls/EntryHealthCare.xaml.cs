using Xamarin.Forms;

namespace HealthCare.Controls
{
    public partial class EntryHealthCare : StackLayout
    {
        private string _ImageSrc;

        public EntryHealthCare()
        {
            InitializeComponent();
        }

        public string ImageSrc
        {
            get { return _ImageSrc; }
            set
            {
                _ImageSrc = value;
                nImage.Source = _ImageSrc;
            }
        }

        public string PlaceHolderText
        {
            get { return nEntry.Placeholder; }
            set { nEntry.Placeholder = value; }
        }

        public string Text
        {
            get { return nEntry.Text; }
            set { nEntry.Text = value; }
        }

        public bool IsPassword
        {
            get { return nEntry.IsPassword; }
            set { nEntry.IsPassword = value; }
        }
    }
}