namespace Probel.Geho.Gui.Converters
{
    using System;
    using System.Windows.Data;

    using Probel.Geho.Gui.Models;

    public class StatusToImageConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var uri = @"/Probel.Geho.Gui;component/Images/Status\{0}.bmp";
            if (value is UiMessage)
            {
                var status = value as UiMessage;

                switch (status.Status)
                {
                    case Status.Info:
                        return new Uri(string.Format(uri, "Info"), UriKind.Relative);
                    case Status.Warn:
                        return new Uri(string.Format(uri, "Warn"), UriKind.Relative);
                    case Status.Error:
                        return new Uri(string.Format(uri, "Error"), UriKind.Relative);
                    case Status.Debug:
                        return new Uri(string.Format(uri, "Debug"), UriKind.Relative);
                    default:
                        return new Uri(string.Format(uri, "Empty"), UriKind.Relative);
                }
            }
            else { return new Uri(string.Format(uri, "Empty"), UriKind.Relative); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}