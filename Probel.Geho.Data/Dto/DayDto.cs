namespace Probel.Geho.Data.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Date} - {Group.Name} - IsMorning: {IsMorning}")]
    public class DayDto : BaseDto
    {
        #region Properties

        public DateTime Date
        {
            get;
            set;
        }

        public GroupDto Group
        {
            get; set;
        }

        public bool IsMorning
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