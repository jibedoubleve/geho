namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToVisibilityHiddenConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                var isVisible = (bool)value;

                return (isVisible)
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
            else { throw new NotSupportedException("Convertion to visibility impossible"); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}