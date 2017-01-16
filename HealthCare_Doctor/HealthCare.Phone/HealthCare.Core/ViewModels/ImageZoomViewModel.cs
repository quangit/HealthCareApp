using System;
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

	public class ImageZoomViewModel : MyMvxViewModel
	{

		private ConsultRequest _request;
		public ConsultRequest Request
		{
			get { return _request; }
			set { SetProperty(ref _request, value); }
		}


		public override void Init()
		{
			Request = GetParam<ConsultRequest>();
		} 
	}
}