using System;
using System.Globalization;
using System.Linq;
using HealthCare.Resx;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class PaymentDesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            if (value is string)
            {
                var strValue = (string)value;
                var creditCard = CreditCardViewModel.Instance.ListCreditCard.FirstOrDefault(model => model.CardId == strValue);
                if (creditCard.CardId == AppConstant.VnPayCreaditCardId)
                    return AppResources.VNPaypayment_card;
                else if (creditCard.CardId == AppConstant.VtcPayCreaditCardId)
                    return AppResources.vtcpay_des;

                return AppResources.sacombank_des;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}