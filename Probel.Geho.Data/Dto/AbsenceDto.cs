﻿namespace Probel.Geho.Services.Dto
{
    using System;

    public class AbsenceDto : BaseDto
    {
        #region Constructors

        public AbsenceDto()
        {
            this.Start
                = this.End
                = DateTime.Today;
        }

        #endregion Constructors

        #region Properties

        public DateTime End
        {
            get; set;
        }

        public bool IsPresent
        {
            get; set;
        }

        public PersonDto Person
        {
            get; set;
        }

        public DateTime Start
        {
            get; set;
        }

        #endregion Properties
    }
}