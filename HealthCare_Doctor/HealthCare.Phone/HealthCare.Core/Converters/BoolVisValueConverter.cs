
using System.Globalization;
#if MVVMCROSS
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;
using Cirrious.CrossCore.Converters;
#else
using MvxVisibility = Windows.UI.Xaml.Visibility;
#endif

using System;

namespace HealthCare.Core.Converters
{


	public class BoolVisValueConverter : MvxBaseVisibilityValueConverter<bool>
	{
		protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
		{
			return (value) ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}
	}

	public class ReverseBoolVisValueConverter : MvxBaseVisibilityValueConverter<bool>
	{
		protected override MvxVisibility Convert(bool value, object parameter, CultureInfo culture)
		{
			return (value) ? MvxVisibility.Collapsed : MvxVisibility.Visible;
		}
	}

	public class StringFormatValueConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return null;

			if (parameter == null)
				return value;

			var format = "{0:" + parameter.ToString()  + "}";

			return string.Format(format, value);
		}
	}
}

