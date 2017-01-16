using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models;
using Pakaze.Core.ViewModels;

namespace HealthCare.Core.ViewModels
{
    public class AskMeDetailViewModel : MyMvxViewModel
    {
        public Question QuestionDatum { get; set;}


        public void Init()
        {
            // Initalize for test purpose
            QuestionDatum = GetParam<Question>();
        }
    }
}
