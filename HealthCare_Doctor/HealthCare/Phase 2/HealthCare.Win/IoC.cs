using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace HealthCare.Core
{
    public class IoC
    {
        public static UnityContainer Container { get; set; }
        public static string LastParams { get; set; }

        static IoC()
        {
            Container = new UnityContainer();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
