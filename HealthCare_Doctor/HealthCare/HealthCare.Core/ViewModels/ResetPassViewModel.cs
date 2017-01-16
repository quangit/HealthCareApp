using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
#endif


using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Utils;


namespace HealthCare.Core.ViewModels
{
    public class ResetPassViewModel : MyMvxViewModel
    {

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public MvxCommand ResetPassCommand { get; set; }
        public MvxCommand BackCommand{ get; set; }

        private readonly IMessageService _messageService;
        public ResetPassViewModel(IMessageService messageService)
        {
            _messageService = messageService;
            ResetPassCommand = new MvxCommand(Reset);
            BackCommand = new MvxCommand(() => Close(this));
        }

#if !MVVMCROSS
        public ResetPassViewModel() : this(IoC.Resolve<IMessageService>())
        {
            
        }
#endif
        private async void Reset()
        {
            if (!RegexUtilities.IsEmail(Email))
            {
                await _messageService.ShowMessageAsync(AppResources.FailureInvalidEmail, AppResources.Warning);
            }
            else
            {
               
                if(await HealthCareService.Current.ResetPass(Email))
                    await _messageService.ShowMessageAsync(AppResources.ResetPass_Success +" "+Email, AppResources.Warning);

            }
        }
    }



   
}
