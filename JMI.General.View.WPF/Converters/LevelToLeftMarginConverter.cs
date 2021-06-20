using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JMI.General.View.WPF.Converters
{
    /// <summary>
    /// Converts intendation to thickness that can be used in margins.
    /// Value is intendation level (type int), and parameter is intendation length (type double).
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class LevelToLeftMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int && parameter is double)
            {
                int level = (int)value;
                double width = (double)parameter;
                double margin = level * width;
                return new Thickness(margin, 0, 0, 0);
            }
            return new Thickness();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
