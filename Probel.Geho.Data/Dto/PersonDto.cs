﻿namespace Probel.Geho.Data.Dto
{
    using System.Collections.Generic;

    public class PersonDto : BaseDto
    {
        #region Properties

        public IEnumerable<AbsenceDto> Absences
        {
            get; set;
        }

        public IEnumerable<ActivityDto> Activities
        {
            get; set;
        }

        public IEnumerable<DayDto> Days
        {
            get;
            set;
        }

        public bool IsEducator
        {
            get; set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Surname
        {
            get;
            set;
        }

        #endregion Properties
    }
}