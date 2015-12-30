namespace Probel.Geho.Services.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Id} {DayOfWeek}")]
    public class LunchTimeDto : BaseDto
    {
        #region Properties

        public DayOfWeek DayOfWeek
        {
            get; set;
        }

        public IEnumerable<PersonDto> People
        {
            get; set;
        }

        #endregion Properties
    }
}