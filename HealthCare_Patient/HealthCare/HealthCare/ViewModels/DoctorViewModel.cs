using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.ViewModels
{
    public class DoctorViewModel : BaseViewModel<DoctorViewModel>
    {
        private readonly IDoctorWS _doctorWS;
        private int _doctorCount;
        private ObservableCollection<CheckupTypeModel> _listCheckupTypeByDoctor;
        private ObservableCollection<CheckupTypeModel> _listCheckupTypeByHospital;
        private ObservableCollection<CheckupTypeModel> _listCheckupTypeDisplay;
        private ObservableCollection<ProxyDoctorModel> _listDoctor;
        private ObservableCollection<ProxyDoctorModel> _listDoctorFilterByCheckupType;
        private RelayCommand<DoctorModel> _listViewClickCommand;
        private RelayCommand _nextCommand;
        private ICommand _resetSelectedDoctorCommand;
        //private RelayCommand _loadCheckupTypeOfDoctorDataCommand;
        private CheckupTypeModel _selectedCheckupType;
        private DoctorModel _selectedDoctor;

        public DoctorViewModel(INavigationService ns, IDoctorWS ws) : base(ns)
        {
            _doctorWS = ws;
        }

        #region Commands

        //public RelayCommand LoadCheckupTypeOfDoctorDataCommand =>
        // _loadCheckupTypeOfDoctorDataCommand ?? (_loadCheckupTypeOfDoctorDataCommand = new RelayCommand(LoadCheckupTypeOfDoctorData));

        public RelayCommand<DoctorModel> ListViewClickCommand =>
            _listViewClickCommand ?? (_listViewClickCommand = new RelayCommand<DoctorModel>(ClickListView));

        public ICommand ResetSelectedDoctorCommand
            => _resetSelectedDoctorCommand ?? (_resetSelectedDoctorCommand = new RelayCommand(ResetSelectedDoctor));

        #endregion

        #region Properties

        public ObservableCollection<ProxyDoctorModel> ListDoctor
        {
            get { return _listDoctor; }
            set
            {
                _listDoctor = value;
                RaisePropertyChanged();
                DoctorCount = value?.Count ?? -1;
            }
        }


        public int DoctorCount
        {
            get { return _doctorCount; }
            set
            {
                _doctorCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     checkuptype of doctor in hospital, this is for (+) flow
        /// </summary>
        public ObservableCollection<CheckupTypeModel> ListCheckupTypeByHospital
        {
            get { return _listCheckupTypeByHospital; }
            set
            {
                var showAll = new CheckupTypeModel { Id = AppConstant.IdAllItems, Name = AppResources.pick_checkuptype };
                if (!(value.Where(x => x.Id == AppConstant.IdAllItems)).Any())
                    value.Insert(0, showAll);
                _listCheckupTypeByHospital = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     checkuptype of doctor, this is for Search flow
        /// </summary>
        public ObservableCollection<CheckupTypeModel> ListCheckupTypeByDoctor
        {
            get { return _listCheckupTypeByDoctor; }
            set
            {
                var showAll = new CheckupTypeModel { Id = AppConstant.IdAllItems, Name = AppResources.pick_checkuptype };
                if (!(value.Where(x => x.Id == AppConstant.IdAllItems)).Any())
                    value.Insert(0, showAll);
                _listCheckupTypeByDoctor = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<CheckupTypeModel> ListCheckupTypeDisplay
        {
            get { return _listCheckupTypeDisplay; }
            set
            {
                _listCheckupTypeDisplay = value;
                //AddCheckupViewModel.Instance.CheckupTypeFilter = value?[0];
                ResetSelectedDoctor();
            }
        }

        public CheckupTypeModel SelectedCheckupType
        {
            get { return _selectedCheckupType; }
            set
            {
                _selectedCheckupType = value;
                RaisePropertyChanged("SelectedCheckupType");
            }
        }

        public DoctorModel SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                _selectedDoctor = value;
                RaisePropertyChanged("SelectedDoctor");
            }
        }

        #endregion

        #region Method

        public async Task<bool> LoadData(string hospitalId, bool isAddButtonFlow)
        {
            Common.ShowLoading();
            try
            {
                await GetDoctorByHospitalId(hospitalId);
                GetCheckupTypeOfDoctorByHospital(hospitalId);
                GetCheckupTypeOfDoctorByDoctor();

                ListCheckupTypeDisplay = isAddButtonFlow ? ListCheckupTypeByHospital : ListCheckupTypeByDoctor;

                UserDialogs.Instance.HideLoading();
                return true;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
                return false;
            }
        }

        private void GetCheckupTypeOfDoctorByHospital(string hospitalId)
        {
            var listCheckupType = new ObservableCollection<CheckupTypeModel>();
            foreach (var doctor in ListDoctor)
            {
                foreach (var hospital in doctor.Hospitals)
                {
                    if (hospital.Id.Equals(hospitalId) && hospital.CheckupType != null)
                        listCheckupType.Add(hospital.CheckupType);
                }
            }
            ListCheckupTypeByHospital =
                new ObservableCollection<CheckupTypeModel>(listCheckupType.GroupBy(x => x.Id).Select(y => y.First()));
            //Bug: Check if reset selected here, have any issues
            SelectedCheckupType = null;
        }

        private void GetCheckupTypeOfDoctorByDoctor()
        {
            var listCheckupType = new ObservableCollection<CheckupTypeModel>();
            foreach (var doctor in ListDoctor)
            {
                if (doctor.CheckupType != null)
                    listCheckupType.Add(doctor.CheckupType);
            }
            ListCheckupTypeByDoctor =
                new ObservableCollection<CheckupTypeModel>(listCheckupType.GroupBy(x => x.Id).Select(y => y.First()));
            //Bug: Check if reset selected here, have any issues
            SelectedCheckupType = null;
        }

        private async Task GetDoctorByHospitalId(string hospitalId)
        {
            try
            {
                //TODO: Deactive
                ListDoctor =
                    new ObservableCollection<ProxyDoctorModel>((await _doctorWS.GetDoctorByHospitalId(hospitalId))
                        .ToBaseModel<ProxyDoctorModel, DoctorApiModel>()
                        .Where(x => x.Status != null && x.Status == PersonStatus.Activated));

                //fetch current checkuptype of doctor in selected hospital
                foreach (var doctorModel in ListDoctor)
                {
                    doctorModel.CurrenCheckupType =
                        doctorModel.Hospitals.FirstOrDefault(x => x.Id.Equals(hospitalId))?.CheckupType;
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        public void ClickListView(DoctorModel data)
        {
            SelectedDoctor = data;
        }

        public void ResetSelectedDoctor()
        {
            SelectedDoctor = null;
        }

        #endregion
    }
}