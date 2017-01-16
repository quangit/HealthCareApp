namespace HealthCare.Enums
{
    public enum KeywordType
    {
        [Description("Triệu chứng")] Symptom = 1,
        [Description("Bác sĩ")] Doctor = 2,
        [Description("Chuyên khoa")] CheckupType = 3,
        [Description("Bệnh viện - Phòng khám")] Hospital = 4

    }
}