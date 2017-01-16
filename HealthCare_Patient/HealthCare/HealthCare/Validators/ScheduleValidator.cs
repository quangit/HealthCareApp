using HealthCare.Models;

namespace HealthCare.Validators
{
    public class ScheduleValidator : AbstractValidator<ScheduleModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(ScheduleModel data)
        {
            Result.Reset();
            Check(data.Doctor, ValidNotNull, "Không tìm thấy bác sĩ ");
            Check(data.Hospital, ValidNotNull, "Không tìm thấy bệnh viện");
            Check(data.CheckupType, ValidNotNull, "Không tìm thấy chuyên khoa");

            return Result;
        }

        #endregion
    }
}