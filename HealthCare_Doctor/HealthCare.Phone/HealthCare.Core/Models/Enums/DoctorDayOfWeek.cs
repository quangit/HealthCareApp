namespace HealthCare.Core.Models.Enums
{
    public class DoctorDayOfWeekObject
    {
        public DoctorDayOfWeek Value { get; set; }
        public override string ToString()
        {
            return Value.ToResourceString();
        }
    }
    public enum DoctorDayOfWeek
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6,
        Saturday = 7,
    }

    
}