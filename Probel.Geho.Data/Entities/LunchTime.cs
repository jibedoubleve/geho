namespace Probel.Geho.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class LunchTime : Entity
    {
        #region Properties

        public DayOfWeek DayOfWeek
        {
            get; set;
        }

        public IList<Person> People
        {
            get; set;
        }

        #endregion Properties
    }
}