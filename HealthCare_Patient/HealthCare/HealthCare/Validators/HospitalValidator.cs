using HealthCare.Objects;

namespace HealthCare.Validators
{
    public class HospitalValidator : AbstractValidator<ProxyHospitalModel>
    {
        public override ValidationResult Validate(ProxyHospitalModel data)
        {
            Result.Reset();
            Check(data.CheckupType, ValidNotNull, "CheckupType is null");
            return Result;
        }
    }
}