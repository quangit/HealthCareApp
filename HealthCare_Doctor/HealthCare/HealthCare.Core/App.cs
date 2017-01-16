using Cirrious.CrossCore.IoC;
using Acr.UserDialogs;
using System.Drawing;
using Cirrious.CrossCore;
using HealthCare.Core.Models;
using HealthCare.Core.Services;

namespace HealthCare.Core
{
	public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();
            
            
            RegisterAppStart<ViewModels.LoginViewModel>();

        }
	}
}