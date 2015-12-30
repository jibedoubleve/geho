namespace Probel.Geho.Services.InMemoryQuery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Services.Dto;

    public class QueryWeek
    {
        #region Fields

        private readonly WeekDto Week;

        #endregion Fields

        #region Constructors

        public QueryWeek(WeekDto week)
        {
            this.Week = week;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<DayDto> Get(DayOfWeek dayOfWeek)
        {
            var days = (from d in this.Week.Days
                        where d.Date.DayOfWeek == dayOfWeek
                        select d).ToList();
            return days;
        }

        #endregion Methods
    }
}