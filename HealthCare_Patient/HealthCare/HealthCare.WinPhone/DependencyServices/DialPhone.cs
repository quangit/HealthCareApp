using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.DependencyServices;
using HealthCare.WinPhone.DependencyServices;
using Microsoft.Phone.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DialPhone))]
namespace HealthCare.WinPhone.DependencyServices
{
    public class DialPhone : IDialPhone
    {
        public bool MakePhoneCall(string number)
        {
            try
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.PhoneNumber = number;
                phoneCallTask.Show();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
