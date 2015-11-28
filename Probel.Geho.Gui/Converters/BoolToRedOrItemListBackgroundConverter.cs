using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Probel.Geho.Gui.Converters
{
    class BoolToRedOrItemListBackgroundConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var boolean = (bool)value;

                return boolean
                    ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC6E2FD"))
                    : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC8F3B8"));
            }
            else { throw new NotSupportedException(string.Format("The value to convert should be a bool. But it is a {0}", value.GetType().Name)); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
