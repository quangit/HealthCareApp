using System;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Models;
using System.Collections.Generic;
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
	public class CmeCategoryViewModel : MyMvxViewModel
	{

		private readonly ICmeService _cmeService;
		private readonly IReporterService _reporterService;
		public CmeCategoryViewModel (ICmeService cmeService, IReporterService reporterService)
		{
			_cmeService = cmeService;
			_reporterService = reporterService;
		}
#if !MVVMCROSS
	    public CmeCategoryViewModel():this(IoC.Resolve<ICmeService>(), IoC.Resolve<IReporterService>())
	    {
	        
	    }
#endif
        private CmeCategory _cmeCategory;
		public CmeCategory CmeCategory
		{
			get { return _cmeCategory; }
			set { SetProperty(ref _cmeCategory, value); }
		}

		private MvxCommand<CmeClass> _cmeClassCommand;
		public MvxCommand<CmeClass> CmeClassCommand
		{
			get
			{
				_cmeClassCommand = _cmeClassCommand ?? new MvxCommand<CmeClass>(DoCmeClass);
				return _cmeClassCommand;
			}
		}

		private async void DoCmeClass(CmeClass item)
		{
			var str = await _cmeService.GetClassDetail (item);
			item.full_description = str;
			ShowViewModel<CmeClassViewModel> (item);
		}

		public override void Init(){
			CmeCategory = GetParam<CmeCategory> ();
		}
	}
}

