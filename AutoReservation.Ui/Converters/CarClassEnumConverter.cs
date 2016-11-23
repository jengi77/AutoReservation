using CarReservation.Common.DataTransferObjects;
using System;
using System.Windows.Data;

namespace CarReservation.Ui.Converters
{
    public class CarClassEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (CarClass)value;
        }
    }
}
