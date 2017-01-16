using HealthCare.Models.ChBaseModel;
using HealthCare.Resx;

namespace HealthCare.Validators.CHBasesValidator
{
    public class MedicationValidator : AbstractValidator<MedicationModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(MedicationModel data)
        {
            Result.Reset();
            Check(data.Name, ValidNotNull, AppResources.empty_medicaltion_name);
            Check(data.Indication, ValidNotNull, AppResources.empty_medicaltion_indication);
            Check(data.Dose, ValidNotEmpty, AppResources.empty_medicaltion_dose);
            Check(data.Strength, ValidNotNull, AppResources.empty_medicaltion_strenght);
            return Result;
        }

        public ValidationResult ValidateAddMedication(MedicationModel data)
        {
            Result.Reset();
            Check(data.Name, ValidNotNull, AppResources.empty_medicaltion_name);
            Check(data.Indication, ValidNotNull, AppResources.empty_medicaltion_indication);
            Check(data.Dose, ValidNotEmpty, AppResources.empty_medicaltion_dose);
            Check(data.Strength, ValidNotNull, AppResources.empty_medicaltion_strenght);
            return Result;
        }

        #endregion
    }
}