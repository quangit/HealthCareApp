using System;
using System.Windows.Data;

namespace HealthCare.Phone.Converters
{
	public class TimeHeaderConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			DateTime date = (DateTime)value;
			return date.ToShortTimeString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
