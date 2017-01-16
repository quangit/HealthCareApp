using HealthCare.Models;
using HealthCare.Resx;

namespace HealthCare.Validators
{
    public class CheckupValidator : AbstractValidator<CheckupModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(CheckupModel data)
        {
            Result.Reset();
            //Check(data.Symptom, ValidNotEmpty, "Triệu chứng không được rỗng");
            Check(data.Schedule, ValidNotNull, AppResources.empty_checkup_schedule);
            //Check(data.Status, x => x != null, "Status checkup không được rỗng");
            Check(data.UserCode, ValidNotEmpty, "Không tìm thấy mã bệnh nhân");
            Check(data.WhenCreated, x => x != null, "WhenCreated is null");
            Check(data.WhenUpdated, x => x != null, "WhenUpdated is null");
            if (data.Schedule != null)
                Result += new ScheduleValidator().Validate(data.Schedule);
            return Result;
        }

        public ValidationResult ValidateAddCheckup(CheckupModel data)
        {
            Result.Reset();
            Check(data.Symptom, ValidNotEmpty, AppResources.rs_failure_symptom_empty);
            Check(data.Symptom, x=>!AppResources.if_more_support_vi.Equals(x), AppResources.rs_failure_symptom_empty);
            return Result;
        }

        #endregion
    }
}