using System.Collections.Generic;

namespace HealthCare.Validators
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsValid = true;
            Errors = new List<string>();
        }

        public bool IsValid { get; set; }
        public IList<string> Errors { get; set; }

        public void Reset()
        {
            IsValid = true;
            Errors.Clear();
        }

        public static ValidationResult operator +(ValidationResult a, ValidationResult b)
        {
            var newValidator = new ValidationResult();
            newValidator.IsValid = a.IsValid && b.IsValid;
            newValidator.Errors = new List<string>(a?.Errors);
            if (b.Errors != null)
                foreach (var error in b.Errors)
                    newValidator.Errors.Add(error);
            return newValidator;
        }
    }
}