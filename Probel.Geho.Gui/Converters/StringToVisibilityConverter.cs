namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class StringToVisibilityConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var str = (string)value;
                return string.IsNullOrWhiteSpace(str)
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
            else { throw new NotSupportedException("Convertion of this type is not allowed."); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}