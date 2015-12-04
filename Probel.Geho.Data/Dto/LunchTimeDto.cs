namespace Probel.Geho.Data.Dto
{
    using System;

    public class LunchTimeDto : BaseDto
    {
        #region Properties

        public DayOfWeek DayOfWeek
        {
            get; set;
        }

        public PersonDto Person
        {
            get; set;
        }

        #endregion Properties
    }
}