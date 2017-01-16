using HealthCare.Models;

namespace HealthCare.Validators
{
    public class CheckupTypeValidator : AbstractValidator<CheckupTypeModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(CheckupTypeModel data)
        {
            Result.Reset();
            Check(data.Id, ValidNotEmpty, "Id chuyên khoa rỗng");
            Check(data.Name, ValidNotEmpty, "Tên chuyên khoa rỗng");
            return Result;
        }

        #endregion
    }
}