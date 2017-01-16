using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.Resx;

namespace HealthCare.Validators.CHBasesValidator
{
    public class WeightValidator : AbstractValidator<WeightModel>
    {
        #region implemented abstract members of AbstractValidator


        public override ValidationResult Validate(WeightModel data)
        {
            Result.Reset();
            Check(data.Value, ValidNotEmpty, AppResources.weight + " " + AppResources.not_be_empty);
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