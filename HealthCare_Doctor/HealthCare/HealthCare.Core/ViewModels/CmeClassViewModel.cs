using System;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Models;
#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
using HealthCare.Core;
#endif
namespace HealthCare.Core.ViewModels
{
	public class CmeClassViewModel : MyMvxViewModel
	{

		private readonly IReporterService _reporterService;
		public CmeClassViewModel (IReporterService reporterService)
		{
			_reporterService = reporterService;
		}
#if !MVVMCROSS
	    public CmeClassViewModel():this(IoC.Resolve<IReporterService>())
	    {
            
        }
#endif

        private CmeClass _cmeClass;
		public CmeClass CmeClass
		{
			get { return _cmeClass; }
			set { SetProperty(ref _cmeClass, value); }
		}


		public override void Init(){
			CmeClass = GetParam<CmeClass> ();
		}
	}
}

