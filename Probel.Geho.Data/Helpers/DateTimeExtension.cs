namespace Probel.Geho.Services.Helpers
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

        public static IEnumerable<DateTime> GetMondays(this DateTime from, int weeks = 26)
        {
            var fromMonday = from.GetMonday();
            var results = new List<DateTime>();
            for (int i = 0; i <= weeks; i++)
            {
                results.Add(fromMonday.AddDays(i * 7));
            }
            return results;
        }

        #endregion Methods
    }
}