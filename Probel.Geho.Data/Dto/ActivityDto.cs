namespace Probel.Geho.Services.Dto
{
    using System;
    using System.Collections.Generic;

    using Entities;

    public class ActivityDto : BaseDto
    {
        #region Properties

        public DayOfWeek DayOfWeek
        {
            get;
            set;
        }

        public bool IsActive
        {
            get; set;
        }

        public MomentDay MomentDay
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IEnumerable<PersonDto> People
        {
            get;
            set;
        }

        #endregion Properties
    }
}