using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using HealthCare.Models.ChBaseModel;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;

namespace HealthCare.ViewModels.CHBases
{
    public class CHBaseHomePageViewModel : BaseViewModel<CHBaseHomePageViewModel>
    {
        private ObservableCollection<CHBaseHomePageFeatureModel> _listFeatureMedicalHistory;
        private ObservableCollection<CHBaseHomePageFeatureModel> _listFeatureMedicalReadings;
        private ObservableCollection<CHBaseHomePageFeatureModel> _listFeatureShareHealthInfomation;

        public CHBaseHomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ClearViewModel();
        }

        #region Commands

        public RelayCommand<CHBaseHomePageFeatureModel> ItemClickCommand => new RelayCommand<CHBaseHomePageFeatureModel>
            (i =>
            {
                if (i?.PageType != null)
                {
                    NavigationService.NavigateTo(i.PageType);
                }
            });


        public RelayCommand GoToChBaseHomePageCommand
            => new RelayCommand(() => { NavigationService.NavigateTo(typeof(ChBaseHomePage)); });

        #endregion

        #region Properties

        public ObservableCollection<CHBaseHomePageFeatureModel> ListFeatureMedicalHistory
        {
            get { return _listFeatureMedicalHistory; }
            set { _listFeatureMedicalHistory = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<CHBaseHomePageFeatureModel> ListFeatureMedicalReadings
        {
            get { return _listFeatureMedicalReadings; }
            set { _listFeatureMedicalReadings = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<CHBaseHomePageFeatureModel> ListFeatureShareHealthInfomation
        {
            get { return _listFeatureShareHealthInfomation; }
            set { _listFeatureShareHealthInfomation = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Methods

        private void ClearViewModel()
        {
            ListFeatureMedicalHistory = new ObservableCollection<CHBaseHomePageFeatureModel>();
            ListFeatureMedicalReadings = new ObservableCollection<CHBaseHomePageFeatureModel>();
            ListFeatureShareHealthInfomation = new ObservableCollection<CHBaseHomePageFeatureModel>();

            InitListFeatureMedicalHistory();
            InitListFeatureMedicalReadings();
            InitListFeatureShareHealthInfomation();
        }

        private void InitListFeatureMedicalHistory()
        {
            ListFeatureMedicalHistory.Clear();
            ListFeatureMedicalHistory.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.procedure,
                PageType = typeof(ProcedurePage)
            });
            ListFeatureMedicalHistory.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.medication,
                PageType = typeof(MedicationPage)
            });
            ListFeatureMedicalHistory.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.immunization,
                PageType = typeof(ImmunizationPage)
            });
        }

        private void InitListFeatureMedicalReadings()
        {
            ListFeatureMedicalReadings.Clear();
            ListFeatureMedicalReadings.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.height,
                PageType = typeof(HeightPage)
            });
            ListFeatureMedicalReadings.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.weight,
                PageType = typeof(WeightPage)
            });
            ListFeatureMedicalReadings.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.blood_glucose,
                PageType = typeof(BloodGlucosePage)
            });
            ListFeatureMedicalReadings.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.blood_pressure,
                PageType = typeof(BloodPressurePage)
            });
        }

        private void InitListFeatureShareHealthInfomation()
        {
            ListFeatureShareHealthInfomation.Clear();
            ListFeatureShareHealthInfomation.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.share_via_chbase_account,
                PageType = typeof(ShareViaCHBaseAccountPage)
            });
            ListFeatureShareHealthInfomation.Add(new CHBaseHomePageFeatureModel
            {
                Name = AppResources.share_via_email,
                PageType = typeof(ShareViaMailPage)
            });
        }

        #endregion
    }
}