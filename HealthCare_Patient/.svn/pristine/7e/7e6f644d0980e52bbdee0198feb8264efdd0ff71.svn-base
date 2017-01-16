using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models.ChBaseModel;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.ViewModels.CHBases
{
    public class ShareViaEmailViewModel : BaseViewModel<ShareViaEmailViewModel>
    {
        private readonly IChBaseWS _chBaseWs;
        private readonly CommonValidator _validator;

        private string _email, _urlShareHealth;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                 RaisePropertyChanged();
            }
        }

        public string UrlShareHealth
        {
            get
            {
                
                return ChBaseHelper.ShareViaChbaseUrl;
            }
            set
            {
                _urlShareHealth = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand ShareCommand => new RelayCommand(async () =>
        {
            var result = _validator.ValidateUserEmail(Email);
            if (!result.IsValid)
            {
                await Common.AlertAsync(result.Errors[0]);
                return;
            }
            Common.ShowLoading();
            try
            {
                var listHeight = await _chBaseWs.GetHeights();
                var listWeight = await _chBaseWs.GetWeights();
                var listProcedure = await _chBaseWs.GetProcedure();
                var listBloodGlucoze = await _chBaseWs.GetBloodGlucose();
                var listBloodPress = await _chBaseWs.GetBloodPressure();
                var listImmunization = await _chBaseWs.GetImmunization();
                var listMedicaiton = await _chBaseWs.GetMedication();

                ChBaseHealthInfoModel model = new ChBaseHealthInfoModel()
                {
                    ListWeight = listWeight,
                    ListHeight = listHeight,
                    ListBloodGlucose = listBloodGlucoze,
                    ListBloodPressure = listBloodPress,
                    ListImmunization = listImmunization,
                    ListMedication = listMedicaiton,
                    ListProcedure = listProcedure,
                    Person = new PersonModel
                    {
                        Name = UserViewModel.Instance.CurrentUser.FullName,
                        Email = Email
                    },
                    Lang = Common.GetDeviceLanguage()
                };
                await _chBaseWs.ShareHealthInformationViaEmail(model);
                Common.ShowMessage(Resx.AppResources.SharedSuccess);
            }
            catch(Exception ex)
            {
                Common.ShowMessage(ex.Message);
            }
        });

        public ShareViaEmailViewModel(INavigationService navigationService, IChBaseWS chBaseWs, CommonValidator validator) : base(navigationService)
        {
            _chBaseWs = chBaseWs;
            _validator = validator;
        }

    }
}
