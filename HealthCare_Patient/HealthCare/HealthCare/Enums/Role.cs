namespace HealthCare.Enums
{
    public enum Role
    {
        [Description("Bệnh Nhân")] Patient = 0,

        [Description("Bác Sĩ")] Doctor = 1,

        [Description("Y Tá")] Nurse = 2,

        [Description("Quản Trị")] HospitalAdmin = 9,

        [Description("Admin")] SystemAdmin = 99
    }
}