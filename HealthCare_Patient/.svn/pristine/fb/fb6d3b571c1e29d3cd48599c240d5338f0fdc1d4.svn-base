using System;

namespace HealthCare.Exceptions
{
    public class DataIntegrityException : Exception
    {
        public DataIntegrityException(string msg) : base("Data Integrate: " + msg)
        {
        }

        public DataIntegrityException(Type type) : base("Data Integrate: " + type +
                                                        " is null from api backend")
        {
        }
    }
}