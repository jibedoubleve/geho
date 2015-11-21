namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Windows.Data;

    using Probel.Geho.Gui.Properties;

    public class DayOfWeekToStringConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DayOfWeek)
            {
                var dayOfWeek = (DayOfWeek)value;

                switch (dayOfWeek)
                {
                    case DayOfWeek.Friday: return Messages.Day_Friday;
                    case DayOfWeek.Monday: return Messages.Day_Monday;
                    case DayOfWeek.Saturday: return Messages.Day_Saturday;
                    case DayOfWeek.Sunday: return Messages.Day_Sunday;
                    case DayOfWeek.Thursday: return Messages.Day_Thursday;
                    case DayOfWeek.Tuesday: return Messages.Day_Tuesday;
                    case DayOfWeek.Wednesday: return Messages.Day_Wednesday;
                    default: throw new NotSupportedException("This day of week is not supported");
                }
            }
            else { return "N.A."; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}