using System;
using System.Globalization;
using System.Linq;
using HealthCare.Models;
using HealthCare.Resx;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class HideCardIdConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return AppResources.empty;
            var s = value as string;
            if (s != null)
            {
                var strValue = s;
                var creditCard = CreditCardViewModel.Instance.ListCreditCard.FirstOrDefault(model => model.CardId == strValue);
                if (creditCard.Id == "Other")
                    return strValue;
                return "********" + strValue.Substring(strValue.Length - 2);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}