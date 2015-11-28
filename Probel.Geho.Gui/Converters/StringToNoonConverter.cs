namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    using Data.Entities;

    using Probel.Geho.Gui.Properties;

    public class StringToNoonConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MomentDay)
            {
                var isMorning = (MomentDay)value;
                switch (isMorning)
                {
                    case MomentDay.Morning:
                        return Messages.Day_Morning;
                    case MomentDay.Afternoon:
                        return Messages.Day_Afternoon;
                    case MomentDay.AllDay:
                        return Messages.Day_AllDay;
                    default:
                        throw new NotSupportedException("This element of this enumeration is not supported");
                }
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