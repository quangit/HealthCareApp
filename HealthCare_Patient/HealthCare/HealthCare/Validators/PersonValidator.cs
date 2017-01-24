using System;
using HealthCare.Models;
using HealthCare.Resx;

namespace HealthCare.Validators
{
    public class PersonValidator : AbstractValidator<PersonModel>
    {
        #region implemented abstract members of AbstractValidator

        public override ValidationResult Validate(PersonModel data)
        {
            Result.Reset();
            //Check(data.Id, ValidNotNull, "Id is null");
            Check(data.LastName, ValidNotEmpty, AppResources.empty_last_name);
            Check(data.FirstName, ValidNotEmpty, AppResources.empty_first_name);
            Check(data.Password, ValidNotEmpty, AppResources.rs_failure_password_empty);
            Check(data.Password, ValidPassword, AppResources.password_invalid_format);
            Check(data.IdNo, ValidNotEmpty, AppResources.rs_failure_idno_empty);
            Check(data.Email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            Check(data.Email, ValidEmail, AppResources.rs_failure_invalid_email);
            Check(data.PhoneNo, ValidNotEmpty, AppResources.rs_failure_phone_empty);
            Check(data.Address, ValidNotEmpty, AppResources.empty_address);
            Check(data.BirthDay, x => x.Date < DateTime.Now.Date, AppResources.rs_failure_birthday_empty);
            Check(data.City, ValidNotNull, AppResources.empty_city);
            Check(data.District, ValidNotNull, AppResources.empty_district);

            return Result;
        }

        public ValidationResult ValidateWithoutPassword(PersonModel data)
        {
            Result.Reset();
            //Check(data.LastName, ValidNotEmpty, AppResources.empty_last_name);
            //Check(data.FirstName, ValidNotEmpty, AppResources.empty_first_name);
            //Check(data.IdNo, ValidNotEmpty, AppResources.rs_failure_idno_empty);
            //Check(data.Email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            if (!string.IsNullOrWhiteSpace(data.Email))
                Check(data.Email, ValidEmail, AppResources.rs_failure_invalid_email);
            Check(data.Email, ValidEmail, AppResources.rs_failure_invalid_email);
            //Check(data.PhoneNo, ValidNotEmpty, AppResources.rs_failure_phone_empty);
            //Check(data.Address, ValidNotEmpty, AppResources.empty_address);
            //Check(data.BirthDay, x => x.Date < DateTime.Now.Date, AppResources.rs_failure_birthday_empty);
            //Check(data.City, ValidNotNull, AppResources.empty_city);
            //Check(data.District, ValidNotNull, AppResources.empty_district);
            return Result;
        }

        public ValidationResult ValidateWithVerifyPassword(PersonModel data)
        {
            Result.Reset();
            //Check(data.LastName, ValidNotEmpty, AppResources.empty_last_name);
            //Check(data.FirstName, ValidNotEmpty, AppResources.empty_first_name);
            //Check(data.Password, ValidNotEmpty, AppResources.rs_failure_password_empty);
            Check(data.Password, ValidPassword, AppResources.password_invalid_format);
            Check(data.VerifyPassword, x => !string.IsNullOrWhiteSpace(x) && x.Equals(data.Password),
               AppResources.password_confirm_not_match);
            //Check(data.IdNo, ValidNotEmpty, AppResources.rs_failure_idno_empty);
            //Check(data.Email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            if (!string.IsNullOrWhiteSpace(data.Email))
                Check(data.Email, ValidEmail, AppResources.rs_failure_invalid_email);
            //Check(data.PhoneNo, ValidNotEmpty, AppResources.rs_failure_phone_empty);
            //Check(data.Address, ValidNotEmpty, AppResources.empty_address);
            //Check(data.BirthDay, x => x.Date < DateTime.Now.Date, AppResources.rs_failure_birthday_empty);
            //Check(data.City, ValidNotNull, AppResources.empty_city);
            //Check(data.District, ValidNotNull, AppResources.empty_district);
            return Result;
        }

        public ValidationResult ValidateForProfile(PersonModel data)
        {
            Result.Reset();
            Check(data.LastName, ValidNotEmpty, AppResources.empty_last_name);
            Check(data.FirstName, ValidNotEmpty, AppResources.empty_first_name);
            Check(data.BirthDay, x => x.Date < DateTime.Now.Date, AppResources.rs_failure_birthday_empty);
            Check(data.PhoneNo, ValidNotEmpty, AppResources.rs_failure_phone_empty);
            //Check(data.Email, ValidNotEmpty, AppResources.rs_failure_email_empty);
            if (!string.IsNullOrWhiteSpace(data.Email))
                Check(data.Email, ValidEmail, AppResources.rs_failure_invalid_email);
            //Check(data.IdNo, ValidNotEmpty, AppResources.rs_failure_idno_empty);
            //Check(data.Address, ValidNotEmpty, AppResources.empty_address);
            //Check(data.City, ValidNotNull, AppResources.empty_city);
            //Check(data.District, ValidNotNull, AppResources.empty_district);
            return Result;
        }

        #endregion
    }
}