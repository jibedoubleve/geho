﻿namespace Probel.Geho.Services.Dto
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Date} - {Group.Name} - IsMorning: {IsMorning}")]
    public class DayDto : DayBaseDto
    {
        #region Properties

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