using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.Resx;

namespace HealthCare.Validators.CHBasesValidator
{
    public class BloodGlucoseValidator : AbstractValidator<BloodGlucoseModel>
    {
        #region implemented abstract members of AbstractValidator


        public override ValidationResult Validate(BloodGlucoseModel data)
        {
            Result.Reset();
           
            Check(data.Value, ValidNotEmpty, AppResources.glucose_value + " " + AppResources.not_be_empty);
            Check(data.Type, ValidNotEmpty, AppResources.glucose_type + " " + AppResources.not_be_empty);
            Check(data.MeasurementContext, ValidNotEmpty, AppResources.glucose_context + " " + AppResources.not_be_empty);
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