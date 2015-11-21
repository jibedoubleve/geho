namespace Probel.Geho.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DateTimeExtension
    {
        #region Methods

        public static DateTime GetMonday(this DateTime dateInWeek)
        {
            var count = (dateInWeek.Date.DayOfWeek == DayOfWeek.Sunday)
                ? -6
                : -(int)(dateInWeek.Date.DayOfWeek - 1);

            var date = dateInWeek.Date.AddDays(count);
            return date.Date;
        }

        #endregion Methods
    }
}