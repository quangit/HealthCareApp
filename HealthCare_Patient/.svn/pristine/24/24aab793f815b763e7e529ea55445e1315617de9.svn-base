using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.Resx;

namespace HealthCare.Validators.CHBasesValidator
{
    public class BloodPressureValidator : AbstractValidator<BloodPressureModel>
    {
        #region implemented abstract members of AbstractValidator


        public override ValidationResult Validate(BloodPressureModel data)
        {
            Result.Reset();
            Check(data.Systolic, ValidNotEmpty, AppResources.systolic + " " + AppResources.not_be_empty);
            Check(data.Diastolic, ValidNotEmpty, AppResources.diastolic + " " + AppResources.not_be_empty);
            Check(data.Pulse, ValidNotEmpty, AppResources.pulse + " " + AppResources.not_be_empty);
            return Result;
        }

        public ValidationResult ValidateNumber(string number)
        {
            Result.Reset();
            Check(number, ValidateIsNumber, "Không phải là chữ số");
            return Result;
        }

        #endregion
    }
}