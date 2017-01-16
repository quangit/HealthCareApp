using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
using HealthCare.Core;
#endif
using HealthCare.Core.Models;

namespace HealthCare.Core.ViewModels
{
    public class CheckupViewModel:MyMvxViewModel
    {


        private Checkup _checkup;
        public Checkup Checkup
        {
            get { return _checkup; }
            set { SetProperty(ref _checkup, value); }
        }

        public CheckupViewModel()
        {
            
        }

        public override void Init()
        {
            base.Init();
            Checkup = GetParam<Checkup>();
        }
    }
}
