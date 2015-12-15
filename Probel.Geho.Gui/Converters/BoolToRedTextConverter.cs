using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Probel.Geho.Gui.Converters
{
   public class BoolToRedTextConverter:IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var boolean = (bool)value;

                return boolean
                    ? (SolidColorBrush)(new BrushConverter().ConvertFrom("Red"))
                    : (SolidColorBrush)(new BrushConverter().ConvertFrom("Black"));
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
