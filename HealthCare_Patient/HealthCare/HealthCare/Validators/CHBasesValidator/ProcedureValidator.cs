using HealthCare.Models.ChBaseModel;
using HealthCare.Resx;

namespace HealthCare.Validators.CHBasesValidator
{
    public class ProcedureValidator : AbstractValidator<ProcedureModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(ProcedureModel data)
        {
            Result.Reset();
            Check(data.Name, ValidNotNull, AppResources.empty_procedure_name);
            Check(data.Location, ValidNotNull, AppResources.empty_procedure_location);
            //Check(data.PrimaryProvider, ValidNotNull, AppResources.empty_procedure_provider);
            return Result;
        }

        public ValidationResult ValidateAddMedication(ProcedureModel data)
        {
            Result.Reset();
            Check(data.Name, ValidNotNull, AppResources.empty_procedure_name);
            Check(data.Location, ValidNotNull, AppResources.empty_procedure_location);
            //Check(data.PrimaryProvider, ValidNotNull, AppResources.empty_procedure_provider);
            return Result;
        }

        #endregion
    }


}