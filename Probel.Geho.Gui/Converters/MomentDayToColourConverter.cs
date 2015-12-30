namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media;

    using Probel.Geho.Services.Entities;

    public class MomentDayToColourConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is MomentDay)
            {
                var momentDay = (MomentDay)value;

                switch (momentDay)
                {
                    case MomentDay.Morning:
                        return null;
                    case MomentDay.Afternoon:
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFF29D"));
                    case MomentDay.AllDay:
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDACF88"));
                    default:
                        throw new NotSupportedException("This MomentDay is not supported");
                }
            }
            else { throw new ArgumentException("value"); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}