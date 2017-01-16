using HealthCare.Models;

namespace HealthCare.Validators
{
    public class SuggestionValidator : AbstractValidator<Suggestion>
    {
        public override ValidationResult Validate(Suggestion data)
        {
            Result.Reset();
            Check(data.Name, ValidNotEmpty, "Suggestion is empty");
            return Result;
        }
    }
}