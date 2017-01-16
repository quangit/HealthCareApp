using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Localization;
using Cirrious.MvvmCross.ViewModels;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;

namespace HealthCare.Core.ViewModels
{
    public class MyMvxViewModel : MvxViewModel
    {

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }


        private readonly IHttpService _httpService;

        //private Queue<Action> BackPendingActions = new Queue<Action>(); // to be done on the Client side When come back from another VM

        public MyMvxViewModel()
        {
            _httpService = Mvx.Resolve<IHttpService>();

        }


        public IMvxLanguageBinder TextSource
        {
            get { return new MvxLanguageBinder("", GetType().Name); }
        }


        public string this[string index]
        {
            get
            {
                //if (index.Equals("currency"))
                //return Data.currency;
                return AppResources.ResourceManager.GetString(index);
            }
        }

        public virtual void Init()
        {
            Data.CurrentVm = this;
        }

        #region ViewModel Navigation

        public MvxCommand<string> ShowViewModelCommand
        {
            get { return new MvxCommand<string>(s =>
            {

                var type = Type.GetType(this.GetType().Namespace + "." + s);
                if (type != null)
                    ShowViewModel(type);

            }); }
        }
        public bool ShowViewModel<T>() where T : IMvxViewModel
        {
            Data.PreviousVm = this;
            return base.ShowViewModel<T>();
        }


        public bool ShowViewModel<T>(object param) where T : IMvxViewModel
        {
            Data.PageParamAdd(typeof(T).Name, param);
            // track a page view
            return ShowViewModel<T>();
        }

        public T GetParam<T>()
        {
            return (T)(Data.PageParamGet((this.GetType()).Name));
        }

        public T GetParam<T>(string viewModelKey)
        {
            return (T)(Data.PageParamGet(viewModelKey));
        }


        protected new bool Close(IMvxViewModel viewModel)
        {
            //Data.CurrentVm = Data.PreviousVm;
            //Data.PreviousVm = this;
            return base.Close(this);
        }
        protected void Close(Action backActionforPrevVm)
        {
            //Data.PreviousVm.BackPendingActions.Enqueue(backActionforPrevVm);
            Close(this);
        }

        #endregion




    }
}
