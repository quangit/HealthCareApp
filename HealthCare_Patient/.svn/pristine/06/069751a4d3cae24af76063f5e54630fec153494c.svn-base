using System;
using System.Globalization;
using System.Linq;
using HealthCare.Resx;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class PaymentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is string)
            {
                var strValue = (string)value;
                var creditCard = CreditCardViewModel.Instance.ListCreditCard.FirstOrDefault(model => model.CardId == strValue);
                //if (creditCard.CardId == AppConstant.VnPayCreaditCardId)
                //    return AppResources.VNPaypayment_card;
                //else if (creditCard.CardId == AppConstant.VtcPayCreaditCardId)
                //    return AppResources.VtcPaypayment_card;

                if (creditCard.CardId == AppConstant.VnPayCreaditCardId ||
                    creditCard.CardId == AppConstant.VtcPayCreaditCardId)
                    return AppResources.payment_other_method;

                return AppResources.payment_card;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}