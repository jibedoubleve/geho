namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    class BoolToWhiteOrRedConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var boolean = (bool)value;

                return boolean
                    ? (SolidColorBrush)(new BrushConverter().ConvertFrom("White"))
                    : (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFDBAB7"));
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