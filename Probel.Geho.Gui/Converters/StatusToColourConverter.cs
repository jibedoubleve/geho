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

    using Probel.Geho.Gui.ViewModels.Controls;

    public class StatusToColourConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ColourStatus)
            {
                var colourStatus = (ColourStatus)value;
                switch (colourStatus)
                {
                    case ColourStatus.Transparent:
                        return null;
                    case ColourStatus.Red:
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFDBAB7"));
                    case ColourStatus.Green:
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC8F3B8"));
                    default: throw new NotSupportedException("This ColourStatus is not supported");
                }
            }
            else { throw new NotSupportedException("Convertion of this value is not supported."); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}