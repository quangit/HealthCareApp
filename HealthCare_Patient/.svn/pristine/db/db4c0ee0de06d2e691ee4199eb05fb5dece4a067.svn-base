using HealthCare.Models.ChBaseModel;
using HealthCare.Resx;

namespace HealthCare.Validators.CHBasesValidator
{
    public class ImmunizationValidator : AbstractValidator<ImmunizationModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(ImmunizationModel data)
        {
            Result.Reset();
            Check(data.Name, ValidNotNull, AppResources.empty_immunization_name);
            Check(data.Administrator, ValidNotNull, AppResources.empty_immunization_who_gave_it);
            Check(data.Manufacturer, ValidNotNull, AppResources.empty_immunization_manufacturer);
            Check(data.Lot, ValidNotEmpty, AppResources.empty_immunization_lot);
            Check(data.Route, ValidNotEmpty, AppResources.empty_immunization_where_administered);
            Check(data.AnatomicSurface, ValidNotEmpty, AppResources.empty_immunization_how_administered);
            Check(data.Sequence, ValidNotEmpty, AppResources.empty_immunization_sequence);
            return Result;
        }

        public ValidationResult ValidateAddImmunization(ImmunizationModel data)
        {
            Result.Reset();
            Check(data.Name, ValidNotNull, AppResources.empty_immunization_name);
            Check(data.Administrator, ValidNotNull, AppResources.empty_immunization_who_gave_it);
            Check(data.Manufacturer, ValidNotNull, AppResources.empty_immunization_manufacturer);
            Check(data.Lot, ValidNotEmpty, AppResources.empty_immunization_lot);
            Check(data.Route, ValidNotEmpty, AppResources.empty_immunization_where_administered);
            Check(data.AnatomicSurface, ValidNotEmpty, AppResources.empty_immunization_how_administered);
            Check(data.Sequence, ValidNotEmpty, AppResources.empty_immunization_sequence);
            return Result;
        }

        #endregion
    }
}