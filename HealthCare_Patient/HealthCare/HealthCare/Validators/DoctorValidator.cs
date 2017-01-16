using HealthCare.Objects;

namespace HealthCare.Validators
{
    public class DoctorValidator : AbstractValidator<ProxyDoctorModel>
    {
        public override ValidationResult Validate(ProxyDoctorModel data)
        {
            Result.Reset();
            Result += new PersonValidator().Validate(data);
            Check(data.Id, x => x != null, "Is Doctor is null");
            Check(data.IsFamilyDoctor, x => x != null, "IsFamilyDoctor is null");
            Check(data.OwnerHospitalId, ValidNotNull, "OwnerHospitalIds is null");
            Check(data.Languages, ValidNotNull, "Doctor Languages is null");
            return Result;
        }
    }
}