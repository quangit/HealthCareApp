using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HealthCare.Core.Resources;
using Newtonsoft.Json;

#if MVVMCROSS
using MyNotifyPropertyChanged = HealthCare.Core.Models.MyNotifyPropertyChanged;
#else
using MyNotifyPropertyChanged = HealthCare.Core.MyNotifyPropertyChanged;
#endif
namespace HealthCare.Core.Models
{
    public class UserAccount : MyNotifyPropertyChanged
    {

        private string _username;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _sidnumber;

        public string SidNumber
        {
            get { return _sidnumber; }
            set { SetProperty(ref _sidnumber, value); }    
        }


        private string _address;

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }   
        }

        private Hospital _hospital;

        public Hospital Hospital
        {
            get { return _hospital; }
            set { SetProperty(ref _hospital, value); }   
        }


        private List<Hospital> _hospitals;

        public List<Hospital> Hospitals
        {
            get { return _hospitals; }
            set { SetProperty(ref _hospitals, value); }
        }



        private string _name;


        [JsonProperty("firstName")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }



        private string _surname;

        [JsonProperty("lastName")]
        public string Surname
        {
            get { return _surname; }
            set { SetProperty(ref _surname, value); }
        }

        private string _email;

        [JsonProperty("email")]
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;

        [JsonProperty("password")]
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _rePass;

        [JsonIgnore]
        public string RePass
        {
            get { return _rePass; }
            set { SetProperty(ref _rePass, value); }
        }

        private string _sex;

        [JsonIgnore]
        public string Gender
        {
            get { return _sex; }
            set
            {
                SetProperty(ref _sex, value);
                GenderValue = (_sex == GenderSource[0]) ? "M" : "F";
            }
        }

        [JsonIgnore]
        public List<string> GenderSource
        {
            get { return new List<string>() { AppResources.SignUp_Male, AppResources.SignUp_Female }; }
        }

        private DateTime _birthday;

        [JsonIgnore]
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                SetProperty(ref _birthday, value);
                if (value != null)
                {
                    DateString = _birthday.ToString("yyyy MMMMM dd");
                    BirthDayValue = Utils.Util.DateTimeToLong(value);
                }
            }
        }

        private string _DateString;

        [JsonIgnore]
        public string DateString
        {
            get { return _DateString; }
            set { SetProperty(ref _DateString, value); }
        }

        [JsonProperty("birthDate")]
        public long BirthDayValue { get; set; }

        [JsonProperty("gender")]
        public string GenderValue { get; set; }

        private string _photo;

        public string Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        public UserAccount()
        {
            DateString = AppResources.SignUp_Birthday;
            Hospitals = new List<Hospital>();
            Hospital = new Hospital();
        }

        public bool Verify(out string message)
        {
            var e = string.IsNullOrEmpty(Email);
            var f = string.IsNullOrEmpty(Name);
            var s = string.IsNullOrEmpty(Surname);
            var p = string.IsNullOrEmpty(Password);
            var r = string.IsNullOrEmpty(RePass);
            var b = Birthday == null;
            var g = string.IsNullOrEmpty(Gender);
            if (e || f || s || p || r | b || g)
            {
                message = AppResources.SignUp_FillForm;
            }
            else if (Password != RePass)
            {
                message = AppResources.SignUp_PassMatch;
            }
            else if (!IsValidEmail(Email))
            {
                message = AppResources.SignUp_WrongEmail;
            }
            else
            {
                message = "";
                return true;
            }
            return false;
        }

        public static bool IsValidEmail(string strIn)
        {
            var invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            //try
            //{
            //    strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
            //                          RegexOptions.None, TimeSpan.FromMilliseconds(200));
            //}
            //catch (RegexMatchTimeoutException)
            //{
            //    return false;
            //}

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
