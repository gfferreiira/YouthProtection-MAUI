using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;
using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;


namespace YouthProtectionAplication.ViewModels.Diario
{
    public class IntToBoolConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int selectedValue && parameter is string parameterString && int.TryParse(parameterString, out int parameterValue))
            {
                return selectedValue == parameterValue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? int.Parse((string)parameter) : 0;
        }
    }
}
