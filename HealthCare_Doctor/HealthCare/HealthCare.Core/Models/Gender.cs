using HealthCare.Core.Resources;

namespace HealthCare.Core.Models
{
    public class Gender
    {
        public bool IsFemale { get; set; }
        public override string ToString()
        {
            return IsFemale ? AppResources.SignUp_Female : AppResources.SignUp_Male;
        }

        public string Value
        {
            get
            {
                return IsFemale ? "F" : "M";
            }
        }
    }
}