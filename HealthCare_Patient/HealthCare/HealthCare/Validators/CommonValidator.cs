using System;
using HealthCare.Enums;
using HealthCare.Resx;

namespace HealthCare.Validators
{
    public class CommonValidator : AbstractValidator<Type>
    {
        public override ValidationResult Validate(Type data)
        {
            throw new Exception("Do not use this method, please use another one in CommonValidator");
        }

        public ValidationResult ValidateLoginInput(string userName, string pass)
        {
            Result.Reset();
            Check(userName, ValidNotEmpty, AppResources.empty_user_name);
            Check(pass, ValidNotEmpty, AppResources.empty_password);
            //Check<string>(pass, ValidPassword, "Mật khẩu không đúng định dạng");
            return Result;
        }

        public ValidationResult ValidateChBaseLoginInput(string email, string pass)
        {
            Result.Reset();
            Check(email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            Check(email, ValidEmail, AppResources.rs_failure_invalid_email);
            Check(pass, ValidNotEmpty, AppResources.rs_failure_password_empty);
            //Check<string>(pass, ValidPassword, "Mật khẩu không đúng định dạng");
            return Result;
        }


        public ValidationResult ValidateMoreSupport
            (string firstName, string lastName, string age, Gender gender,
                string email, string doctorName, string hospitalName, string symptom)
        {
            Result.Reset();
            Check(firstName, ValidNotEmpty, AppResources.empty_first_name);
            Check(lastName, ValidNotEmpty, AppResources.empty_last_name);
            Check(age, ValidNotEmpty, AppResources.empty_age);
            Check(email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            Check(email, ValidEmail, AppResources.rs_failure_invalid_email);
            //Check(hospitalName, ValidNotEmpty, AppResources.empty_hospial_name);
            Check(symptom, ValidNotEmpty, AppResources.rs_failure_symptom_empty);

            //Check(age, x => (age != null), "Tuổi không được để trống");
            return Result;
        }
        public ValidationResult ValidateMoreSupport
           (string symptom)
        {
            Result.Reset();
            //Check(hospitalName, ValidNotEmpty, AppResources.empty_hospial_name);
            Check(symptom, ValidNotEmpty, AppResources.empty_question);
            //Check(age, x => (age != null), "Tuổi không được để trống");
            return Result;
        }
        public ValidationResult ValidateRelatedUser(string firstName, string address, string email)
        {
            Result.Reset();
            Check(firstName, ValidNotEmpty, AppResources.empty_patient_name);
            Check(address, ValidNotEmpty, AppResources.rs_failure_address_empty);
            if (!string.IsNullOrWhiteSpace(email))
                Check(email, ValidEmail, AppResources.rs_failure_invalid_email);
            return Result;
        }

        public ValidationResult ValidateEmptyPassword(string password)
        {
            Result.Reset();
            Check(password, ValidNotEmpty, AppResources.empty_password);
            return Result;
        }

        public ValidationResult ValidateChangePassword(string oldPassword, string newPassword, string confirmPassword,
            bool isValidateFormatPassword)
        {
            Result.Reset();
            Check(oldPassword, ValidNotEmpty, AppResources.empty_old_password);
            Check(newPassword, ValidNotEmpty, AppResources.empty_new_password);
            Check(confirmPassword, ValidNotEmpty, AppResources.empty_confirm_password);
            if (isValidateFormatPassword)
                Check(newPassword, ValidPassword,
                    AppResources.password_invalid_format);
            Check(confirmPassword, x => !string.IsNullOrWhiteSpace(x) && x.Equals(newPassword),
                AppResources.password_confirm_not_match);
            Check(newPassword, x => !x.Equals(oldPassword), AppResources.password_new_duplication_old);
            return Result;
        }

        public ValidationResult ValidateUserEmail(string email)
        {
            Result.Reset();
            Check(email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            Check(email, ValidEmail, AppResources.rs_failure_invalid_email);
            return Result;
        }

        public ValidationResult ValidateSendOtp(string phone, string otp)
        {
            Result.Reset();
            Check(phone, ValidNotEmpty, AppResources.rs_failure_phone_empty);
            Check(otp, ValidNotEmpty, AppResources.otp_empty);
            return Result;
        }

        public ValidationResult ValidateSendHospitalReigtrationSecurityCode(string code)
        {
            Result.Reset();
            Check(code, ValidNotEmpty, AppResources.pin_code_cannot_empty);
            return Result;
        }
    }
}