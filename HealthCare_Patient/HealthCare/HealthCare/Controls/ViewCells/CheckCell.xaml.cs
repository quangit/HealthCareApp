using Xamarin.Forms;

namespace HealthCare.Controls.ViewCells
{
    public partial class CheckCell : ViewCell
    {
        private static CheckCell _currentCheckCell;

        public CheckCell()
        {
            //_currentCheckCell = null;
            InitializeComponent();
        }

        public View Content
        {
            get { return idContent.Content; }
            set { idContent.Content = value; }
        }

        public void InverseCheck()
        {
            idCheckIcon.IsVisible = !idCheckIcon.IsVisible;
        }

        protected override void OnTapped()
        {
            base.OnTapped();
            InverseCheck();
            _currentCheckCell?.InverseCheck();
            _currentCheckCell = this;
        }
    }
}