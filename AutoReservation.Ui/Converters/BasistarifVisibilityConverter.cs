using CarReservation.Common.DataTransferObjects;
using System;
using System.Windows;
using System.Windows.Data;

namespace CarReservation.Ui.Converters
{
    public class BasistarifVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((CarClass)value == CarClass.Luxury)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
