using System;
using System.Collections.Generic;
#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
using HealthCare.Core;
#endif
using System.Collections.ObjectModel;
using HealthCare.Core.Models;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;

namespace HealthCare.Core.ViewModels
{
    public class CmeSearchViewModel : MyMvxViewModel
    {

        private readonly ICmeService _cmeService;
        private readonly IReporterService _reporterService;

        public CmeSearchViewModel(ICmeService cmeService, IReporterService reporterService)
        {
            _cmeService = cmeService;
            _reporterService = reporterService;
        }
#if !MVVMCROSS
        public CmeSearchViewModel() : this(Core.IoC.Resolve<ICmeService>(), IoC.Resolve<IReporterService>())
        {

        }
#endif
        private string _searchTerm = "";
        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); }
        }

        private List<CmeClass> _cmeClasses;
        public List<CmeClass> CmeClasses
        {
            get { return _cmeClasses; }
            set { SetProperty(ref _cmeClasses, value); }
        }

        private MvxCommand _searchCommand;
        public MvxCommand SearchCommand
        {
            get
            {
                _searchCommand = _searchCommand ?? new MvxCommand(DoSearch);
                return _searchCommand;
            }
        }

        private async void DoSearch()
        {
            CmeClasses = await _cmeService.SearchClasses(SearchTerm.Trim());
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
            var str = await _cmeService.GetClassDetail(item);
            item.full_description = str;
            ShowViewModel<CmeClassViewModel>(item);
        }
    }
}

