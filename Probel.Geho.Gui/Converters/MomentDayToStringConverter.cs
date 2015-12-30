namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Services.Entities;

    public class MomentDayToStringConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MomentDay)
            {
                var dayOfWeek = (MomentDay)value;

                switch (dayOfWeek)
                {
                    case MomentDay.Morning: return Messages.Day_Morning;
                    case MomentDay.Afternoon: return Messages.Day_Afternoon;
                    case MomentDay.AllDay: return Messages.Day_AllDay;
                    default: throw new NotSupportedException("This moment of day is not supported");
                }
            }
            else { return "N.A."; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}