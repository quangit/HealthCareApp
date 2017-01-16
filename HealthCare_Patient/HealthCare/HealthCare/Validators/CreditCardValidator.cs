using HealthCare.Models;
using HealthCare.Resx;

namespace HealthCare.Validators
{
    public class CreditCardValidator : AbstractValidator<CreditCardModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(CreditCardModel data)
        {
            Result.Reset();
            Check(data.CardId, ValidNotEmpty, AppResources.empty_cc_num);
            // Check(data.CardId, ValidCardNumber, "Mã số thẻ không đúng");
            Check(data.FirstName, ValidNotEmpty, AppResources.empty_first_name);
            Check(data.LastName, ValidNotEmpty, AppResources.empty_last_name);
            Check(data.Address, ValidNotEmpty, AppResources.empty_address);
            Check(data.IdNo, ValidNotEmpty, AppResources.rs_failure_idno_empty);
            Check(data.Email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            Check(data.Email, ValidEmail, AppResources.rs_failure_invalid_email);
            Check(data.PhoneNo, ValidNotEmpty, AppResources.rs_failure_phone_empty);
            Check(data.BirthDay, x => x > 0, AppResources.rs_failure_birthday_empty);
            return Result;
        }

        public ValidationResult ValidatePinOtp(string pinOrOTP, CreditCardModel newCard)
        {
            Result.Reset();
            Check(newCard, ValidNotNull, AppResources.cc_add_failure);
            Check(pinOrOTP, ValidNotEmpty, AppResources.empty_cc_pin_otp);
            return Result;
        }

        public ValidationResult ValidateSetPaymentPassword(string password, string confirmPassword)
        {
            Result.Reset();
            Check(password, ValidNotEmpty, AppResources.rs_failure_password_empty);
            Check(confirmPassword, ValidNotEmpty, AppResources.empty_confirm_password);
            Check(password, data => data != null && data.Equals(confirmPassword), AppResources.password_confirm_not_match);
            return Result;
        }

        public ValidationResult ValidateEnterPaymentPassword(string password)
        {
            Result.Reset();
            Check(password, ValidNotEmpty, AppResources.rs_failure_password_empty);
            return Result;
        }

        #endregion
    }
}