using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfColorFontDialog
{
	public class FontSizeListBoxItemToDoubleConverter : IValueConverter
	{

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            string str = (string)value;
            try
            {
                return double.Parse(value.ToString());
            }
            catch(FormatException ex)
            {
                return 0;
            }

        }

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}