using System;
using System.Text.RegularExpressions;

namespace HealthCare.Validators
{
    public abstract class AbstractValidator<T>
    {
        public delegate bool ValidData<K>(K data);

        protected ValidationResult Result = new ValidationResult();
        public abstract ValidationResult Validate(T data);

        public void Check<K>(K data, ValidData<K> method, string errorMessage)
        {
            if (!method(data))
            {
                Result.IsValid = false;
                Result.Errors.Add(errorMessage);
            }
        }

        public static bool ValidNotEmpty(string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static bool ValidNotNull(object data)
        {
            return (data != null);
        }

        public static bool ValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            email = email.Trim();

            var regex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }

        /// <summary>
        ///     Mode 10 algorithm to valid credit card number
        /// </summary>
        /// <param name="ccValue"></param>
        /// <returns></returns>
        public static bool ValidCardNumber(string ccValue)
        {
            if (ccValue == null)
                return false;
            ccValue = ccValue.Replace("-", "");
            ccValue = ccValue.Replace(" ", "");

            var checksum = 0;
            var evenDigit = false;
            var charArray = ccValue.ToCharArray();
            Array.Reverse(charArray);
            foreach (var digit in charArray)
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }

                var digitValue = (digit - '0')*(evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue%10;
                    digitValue /= 10;
                }
            }
            return (checksum%10) == 0;
        }

        /// <summary>
        ///     Password MUST contain Uppercase letter, Lowercase letter, number and at least 8 character
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            //var patternPass = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,30}$";

            var patternPass = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,30}$";

            var myRegexDienThoai = new Regex(patternPass);

            return myRegexDienThoai.IsMatch(password);
        }

        public static bool ValidPhoneNumber(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return false;
            }
            mobile = mobile.Trim();
            const string patternDienThoai = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";

            var myRegexDienThoai = new Regex(patternDienThoai);

            return myRegexDienThoai.IsMatch(mobile);
        }

        public static bool ValidateIsNumber(string ccValue)
        {
            double num;
            if (double.TryParse(ccValue, out num))
            {
                return true;
            }
                return false;
        }
    }
}