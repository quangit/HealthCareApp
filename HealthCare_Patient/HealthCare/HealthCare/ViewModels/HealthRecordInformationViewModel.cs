using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class HealthRecordInformationViewModel : BaseViewModel<HealthRecordInformationViewModel>
    {

        private string _fullName;
        private string _imageAvatar;
        private string _statusAccount;
        private string _statusButton;
        private DateTime _expirationDate;
        private ImageSource _avatarImageSource;
        private bool _loggedChBase;
        private readonly IChBaseWS _chBaseWs;
        public HealthRecordInformationViewModel(INavigationService navigationService,IChBaseWS chBaseWs) : base(navigationService)
        {
            _chBaseWs = chBaseWs;
            //_chBaseWs.GetPatientInfo();
            FullNamne = "Ngo Duc Quang sadasdas.kjdasdaskjdfhk";
            StatusAccount = AppResources.payment_pending_account;
            StatusButton = AppResources.payment;
            ExpirationDate = DateTime.Now;
        }

        public string FullNamne
        {
            get
            {
                if (_fullName.Length > 20)
                {
                    return _fullName.Substring(0,20);
                }
                return _fullName;
            }
            set { _fullName = value;RaisePropertyChanged(); }
        }

        public string StatusAccount
        {
            get { return _statusAccount; }
            set { _statusAccount = value; RaisePropertyChanged(); }
        }

        public string StatusButton
        {
            get { return _statusButton; }
            set { _statusButton = value; RaisePropertyChanged(); }
        }

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value;RaisePropertyChanged(); }
        }

        public ImageSource AvatarImageSource
        {
            get
            {
                return _avatarImageSource ??
                       (_avatarImageSource = new FileImageSource { File = "ic_avatar_placeholder.png" });
            }
            set
            {
                _avatarImageSource = value;
                RaisePropertyChanged();
            }
        }

        public bool LoggedChBase
        {
            get { return _loggedChBase; }
            set
            {
                _loggedChBase = value;
                RaisePropertyChanged();
            }
        }
    }
}
