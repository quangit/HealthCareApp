using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using HealthCare.Core.Models;
using Pakaze.Core.ViewModels;

namespace HealthCare.Core.ViewModels
{
    public class ExaminationDetailViewModel : MyMvxViewModel
    {

        public Examination _ExaminationDatum { get; set; }


        public void Init()
        {
            // Initalize for test purpose
            _ExaminationDatum = GetParam<Examination>();
        }
    }
}
