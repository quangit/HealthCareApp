using System;
using HealthCare.Resx;

namespace HealthCare.Exceptions
{
    public class NetworkException : Exception
    {
        public NetworkException() : base(AppResources.network_not_available)
        {
        }
    }
}