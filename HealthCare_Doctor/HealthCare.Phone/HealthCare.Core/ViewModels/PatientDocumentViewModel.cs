using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models;
using Pakaze.Core.ViewModels;

namespace HealthCare.Core.ViewModels
{
    public class PatientDocumentViewModel : MyMvxViewModel
    {

        public string DateOfExamination { get; set; }

        private ObservableCollection<Document> _documents;

        public ObservableCollection<Document> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                RaisePropertyChanged();
            }
        }

        

        public void Init()
        {
            // Initalize for test purpose
           // _ExaminationDatum = GetParam<Examination>();
            Documents = new ObservableCollection<Document>();
            var examination = GetParam<PatientExamination>();
            DateOfExamination = examination.ExaminationDate.ToString("yy-mm-dd");
            //TODO: for test purpose
            for (int i = 0; i < 10; i++)
            {
                Document _doc = new Document();
                _doc.Name = "Chup X-Quang";
                _doc.Desc = "Chup Phoi ... ";
                Documents.Add(_doc);
            }
        }
    }
}
