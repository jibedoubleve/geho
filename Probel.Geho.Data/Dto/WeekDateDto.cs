namespace Probel.Geho.Services.Dto
{
    using System;
    using System.Collections.Generic;

    public class WeekDateDto
    {
        #region Properties

        public DateTime Date
        {
            get; set;
        }

        public IEnumerable<DayBaseDto> Days
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }

        #endregion Properties
    }
}