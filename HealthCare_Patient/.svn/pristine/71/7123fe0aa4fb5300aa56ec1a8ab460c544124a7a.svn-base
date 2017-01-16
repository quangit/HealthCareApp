using HealthCare.Models;

namespace HealthCare.Validators
{
    public class CityValidator : AbstractValidator<CityModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(CityModel data)
        {
            Result.Reset();
            Check(data.Id, ValidNotEmpty, "Id thành phố rỗng");
            //Check(data.Name, ValidNotEmpty, "Tên chuyên khoa rỗng");
            return Result;
        }

        #endregion
    }
}