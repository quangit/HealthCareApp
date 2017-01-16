using System;
using Cirrious.CrossCore.Converters;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace HealthCare.Core.Converters
{
	public class ListVisValueConverter : MvxValueConverter<IEnumerable<object>, bool>
	{
		protected override bool Convert(IEnumerable<object> value, Type targetType, object parameter, CultureInfo culture)
		{ 
			var listempty = (value == null || value.Count() == 0); // list empty ? 
			return listempty;
		}
	}

	public class ListVisInvValueConverter : MvxValueConverter<IEnumerable<object>, bool>
	{
		protected override bool Convert(IEnumerable<object> value, Type targetType, object parameter, CultureInfo culture)
		{
			var ret = (value != null && value.ToList<object>().Count != 0); // list not empty ? 
			return ret;
		}
	}
}

