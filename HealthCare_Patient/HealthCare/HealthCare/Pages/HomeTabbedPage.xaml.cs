using System.Collections.ObjectModel;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class HomeTabbedPage : TabbedPageCustom
    {
       
        public HomeTabbedPage()
        {
            Common.ShowLoading();
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, Common.OS != TargetPlatform.WinPhone);
            NavigationPage.SetHasBackButton(this, false);

            if (CommonViewModel.Instance.SystemConfig.PatientAppTabs != null)
            {
                foreach (var index in CommonViewModel.Instance.SystemConfig.PatientAppTabs)
                {
                    switch (index)
                    {
                        case 1:
                            Children.Add(new SearchPage());
                            break;
                        case 2:
                            Children.Add(new AskDoctorPage());
                            break;
                        case 3:
                            Children.Add(new CheckupPage());
                            break;
                        case 4:

                            Children.Add(new HealthRecordInformationPage());

                            //Children.Add(new CreditCardPage());

                            //Children.Add(new LoginChBasePage());

                            break;
                    }
                }
            }
            else
            {
                Children.Add(new SearchPage());
                Children.Add(new AskDoctorPage());
                Children.Add(new CheckupPage());

                Children.Add(new HealthRecordInformationPage());

                //Children.Add(new CreditCardPage());

                //Children.Add(new LoginChBasePage());

            }

        }

    }
}