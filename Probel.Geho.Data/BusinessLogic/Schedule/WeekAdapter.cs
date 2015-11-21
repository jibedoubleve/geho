namespace Probel.Geho.Data.Business_Logic.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Data.Dto;

    public class WeekAdapter
    {
        #region Fields

        private readonly WeekDto Week;

        #endregion Fields

        #region Constructors

        public WeekAdapter(WeekDto week)
        {
            this.Week = week;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<DayDto> GetAfternoons()
        {
            return (from d in Week.Days
                    where !d.IsMorning
                    select d).OrderBy(e => e.Date)
                             .ToList();
        }

        public IEnumerable<DayDto> GetDay(DayOfWeek day)
        {
            return (from d in Week.Days
                    where d.Date.DayOfWeek == day
                    select d).OrderBy(e => e.IsMorning)
                             .ToList();
        }

        public IEnumerable<DayDto> GetMornings()
        {
            return (from d in Week.Days
                    where d.IsMorning
                    select d).OrderBy(e => e.Date)
                             .ToList();
        }

        #endregion Methods
    }
}