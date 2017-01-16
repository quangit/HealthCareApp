/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:HealthCare"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Linq;
using System.Reflection;
using HealthCare.Helpers;
using HealthCare.Pages.CHBases;
using HealthCare.Pages;
using HealthCare.Services;
using HealthCare.Services.Interfaces;
using HealthCare.ViewModels.CHBases;
using Microsoft.Practices.ServiceLocation;

namespace HealthCare.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIocV2.Default);
            InitHcServices();
        }

        public PromotionViewModel VmPromotion => PromotionViewModel.Instance;
        public LoginViewModel VmLogin => LoginViewModel.Instance;
        public UserViewModel VmUser => UserViewModel.Instance;
        public CheckupViewModel VmCheckup => CheckupViewModel.Instance;
        public CreditCardViewModel VmCreditCard => CreditCardViewModel.Instance;
        public CommonViewModel VmCommon => CommonViewModel.Instance;
        public ToolbarViewModel VmToolbar => ToolbarViewModel.Instance;
        public HospitalViewModel VmHospital => HospitalViewModel.Instance;
        public SettingViewModel VmSetting => SettingViewModel.Instance;
        public DoctorViewModel VmDoctor => DoctorViewModel.Instance;
        public PasswordViewModel VmPassword => PasswordViewModel.Instance;
        public DoctorScheduleViewModel VmDoctorSchedule => DoctorScheduleViewModel.Instance;
        public DoctorLocationViewModel VmDoctorLocation => DoctorLocationViewModel.Instance;
        public AddCheckupViewModel VmAddCheckup => AddCheckupViewModel.Instance;
        public SearchViewModel VmSearch => SearchViewModel.Instance;
        public MoreSupportViewModel VmMoreSupport => MoreSupportViewModel.Instance;
        public MoreSupportViewModel2 VmMoreSupport2 => MoreSupportViewModel2.Instance;
        public AskDoctorViewModel VmAskDoctor => AskDoctorViewModel.Instance;
        public FakeDataViewModel VmFakeData => FakeDataViewModel.Instance;
        public TopBarViewModel VmTopBar => TopBarViewModel.Instance;
        public HealthRecordInformationViewModel VmHealthRecordInformation => HealthRecordInformationViewModel.Instance;
        public CHBaseDetailViewModel VmChBaseDetail => CHBaseDetailViewModel.Instance;
        public ProcedureViewModel VmProcedure => ProcedureViewModel.Instance;
        public ShareViaEmailViewModel VmShareViaEmail => ShareViaEmailViewModel.Instance;
        public BloodPressureViewModel VmBloodPressure => BloodPressureViewModel.Instance;
        public MedicationViewModel VmMedication => MedicationViewModel.Instance;
        public ImmunizationViewModel VmImmunization => ImmunizationViewModel.Instance;
        public HeightViewModel VmHeight => HeightViewModel.Instance;
        public WeightViewModel VmWeight => WeightViewModel.Instance;
        public ChBaseViewModel VmChBase => ChBaseViewModel.Instance;
        public CHBaseHomePageViewModel VmChBaseHomePage => CHBaseHomePageViewModel.Instance;
        public BloodGlucoseViewModel VmBloodGlucose => BloodGlucoseViewModel.Instance;

        public PaymentViewModel VmPayment => PaymentViewModel.Instance;
        

        public void InitHcServices()
        {
            var assembly = typeof(ViewModelLocator).GetTypeInfo().Assembly;
            var listclass = assembly.CreatableTypes();
            var listPage = listclass.EndingWith("Page");

            var navigationService = new NavigationService();

            foreach (var page in listPage)
            {
                navigationService.Configure(page);
            }

            SimpleIocV2.Default.Reset();

            var listservice = listclass.EndingWith("WS").AsInterfaces();
            foreach (var service in listservice)
            {
                SimpleIocV2.Default.Register(service.ServiceTypes.ElementAt(0), service.ImplementationType);
            }
            SimpleIocV2.Default.Register<INavigationService>(() => navigationService);
        }
    }
}