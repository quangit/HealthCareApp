using System;
using Cirrious.CrossCore.Converters;
using HealthCare.Core.Resources;
using System.Globalization;

namespace HealthCare.Core
{
	public class TextHintValueConverter : MvxValueConverter<string, string>
	{
		protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			var hint = (parameter == null) ? "" : parameter.ToString ();
			if (string.IsNullOrEmpty (value))
				return AppResources.ResourceManager.GetString (hint);
			else
				return value;
		}
	}
}

