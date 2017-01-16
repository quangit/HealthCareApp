using HealthCare.Models;

namespace HealthCare.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public override ValidationResult Validate(UserModel data)
        {
            Result.Reset();
            Result += new PersonValidator().Validate(data);
            return Result;
        }
    }
}