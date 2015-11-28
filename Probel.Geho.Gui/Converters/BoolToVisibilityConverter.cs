namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToVisibilityConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                var isVisible = (bool)value;

                return (isVisible)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
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