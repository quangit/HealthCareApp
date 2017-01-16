using System;
using System.Runtime.CompilerServices;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Controls
{
    /// <summary>
    ///     Keep in mind add following line in your page xamlnamespace.
    ///     If NOT, it'll cause you a black screen on ui!
    ///     PageType="{x:Type pages:YourPageName}"
    ///     xmlns:pages="clr-namespace:HealthCare.Pages;assembly=HealthCare"
    /// </summary>
    public class TopBarContentPage : ContentPage
    {
        private readonly Grid _grid;
        private bool _isLoaded;
        private TopBarControl _topBar;

        public TopBarContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, !(Common.OS == TargetPlatform.WinPhone));
            _grid = new Grid {RowSpacing = 0, ColumnSpacing = 0, Padding = 0, BackgroundColor = Color.Transparent};

            BindingContextChanged += (sender, args) =>
            {
                if (!_isLoaded && Loaded != null)
                {
                    _isLoaded = true;
                    Loaded.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public Type PageType { get; set; }
        public event EventHandler Loaded;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (Common.OS == TargetPlatform.WinPhone && !string.IsNullOrWhiteSpace(propertyName) &&
                propertyName.Equals("Title"))
            {
                if (_topBar != null)
                    _topBar.Title = Title;
            }
        }

        /// <param name="child">The element that was added.</param>
        /// <summary>
        ///     Raises the child added event.
        ///     IMPORTANT: Keep in mind, we always set Content before do anything with _grid
        /// </summary>
        protected override void OnChildAdded(Element child)
        {
            try
            {
                if (child != _grid)
                {
                    Content = _grid;
                    var topBarControl = CreateTopBarControl();
                    _grid.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition
                        {
                            Height = new GridLength(topBarControl.TopBarControlHeight,
                                GridUnitType.Absolute)
                        },
                        new RowDefinition {Height = HcStyles.HcGridLength}
                    };
                    _grid.Children.Add(topBarControl, 0, 0);
                    _grid.Children.Add((View) child, 0, 1);
                }
                else
                    base.OnChildAdded(child);
            }
            catch (Exception)
            {
                Content = (View) child;
            }
        }

        private TopBarControl CreateTopBarControl()
        {
            _topBar = new TopBarControl {Title = Title};

            switch (PageType.Name)
            {
                case "RegisterPage":
                case "PromotionPage":
                case "PaymentPage":
                case "SettingPage":
                case "ProfilePage":
                case "ForgotPasswordPage":
                case "OTPPage":
                case "BaseAddCheckupPage":
                case "AddCardPage":
                case "MoreSupportPage":
                case "DoctorLocationPage":
                case "DoctorDetailPage":
                case "HospitalDetailPage":
                case "ConfirmCreditCardPage":
                case "VnPayPaymentPage":
                case "CreditCardPage":
                case "ConfigurationPage":
                case "AskDoctorDetailPage":
                case "AddMedicationPage":
                case "AddQuestionPage":
                case "AddProcedurePage":
                case "ShareViaMailPage":
                case "CHBaseDetailPage":

                case "AddHeightPage":
                case "AddWeightPage":
                case "AddBloodPressure":

                case "WeightPage":
                case "HeightPage":



                    _topBar.TopBarStyle = Common.OnPlatform(
                        TopBarStyle.Style2,
                        TopBarStyle.Style2,
                        TopBarStyle.Style1);
                    break;
                case "CheckupPage":
                case "LoginChBasePage":
                case "SearchPage":
                case "AskDoctorPage":
                case "HealthRecordInformationPage":
                case "CHBaseMedicalHistoryPage":
                case "CHBaseMeicalReadingsPage":
                case "CHBaseShareHealthInfomationPage":
                    _topBar.TopBarStyle = Common.OnPlatform(
                        TopBarStyle.Style4,
                        TopBarStyle.Style4,
                        TopBarStyle.Style3);
                    break;
                case "CheckupDetailPage":
                case "MedicalRecordPage":
                case "PromotionDetailPage":
                case "CardDetailPage":
                case "PasswordPage":
                    _topBar.TopBarStyle = Common.OnPlatform(
                        TopBarStyle.Style4,
                        TopBarStyle.Style4,
                        TopBarStyle.Style1);
                    break;
                
                default:
                    _topBar.TopBarStyle = TopBarStyle.Style1;
                    break;
            }
            return _topBar;
        }
    }
}