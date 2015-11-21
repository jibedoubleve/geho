namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    using Probel.Geho.Gui.Properties;

    public class BoolToNoonConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                var isMorning = (bool)value;
                return isMorning
                    ? Messages.Day_Morning
                    : Messages.Day_Afternoon;
            }
            else { throw new ArgumentException("Not supported convertion", "value"); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}