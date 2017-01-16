using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using HealthCare.Core.Models;
using Pakaze.Core.ViewModels;

namespace HealthCare.Core.ViewModels
{

    public class DoctorHandbookDetailViewModel : MyMvxViewModel
    {
        private PatientExamination _patientExamination;

        public PatientExamination PatientExamination
        {
            get { return _patientExamination; }
            set
            {
                _patientExamination = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<PatientExamination> _patientExaminations;

        public ObservableCollection<PatientExamination> PatientExaminations
        {
            get { return _patientExaminations; }
            set
            {
                _patientExaminations = value;
                RaisePropertyChanged();
            }
        }


        private PatientExamination _selectedDoctorHandbook;

        public PatientExamination SelectedDoctorHandbook
        {
            get{ return _selectedDoctorHandbook; }
            set
            {
                _selectedDoctorHandbook = value;
                RaisePropertyChanged();
            }
        }

        private MvxCommand _showDoctocHandbookDetailCommand;
        public MvxCommand ShowPatientExaminationCommand
        {
            get
            {
                _showDoctocHandbookDetailCommand = _showDoctocHandbookDetailCommand ?? new MvxCommand(() =>
                {
                    ShowViewModel<PatientDocumentViewModel>(SelectedDoctorHandbook);
                });
                return _showDoctocHandbookDetailCommand;
            }
        }




        public void Init()
        {
            // Initalize for test purpose
            PatientExaminations = new ObservableCollection<PatientExamination>(); 
            PatientExamination = GetParam<PatientExamination>();

            for (int i = 0; i < 10; i++)
            {
                PatientExamination _patientExamination = new PatientExamination();
                _patientExamination.ExaminationDate = DateTime.Now.AddDays(i);
                _patientExamination.ExaminationTime = i + "g00";
                _patientExamination.ExaminationVenue = "FV Hospital";
                _patientExamination.ExamDateString = _patientExamination.ExaminationDate.ToString("dd-mm-yy");
                _patientExamination.Patient = new Patient();
                _patientExamination.Patient = PatientExamination.Patient;
                PatientExaminations.Add(_patientExamination);
            }
        }
    }
}
