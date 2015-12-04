namespace Probel.Geho.Data.Entities
{
    using System;

    public class LunchTime : Entity
    {
        #region Properties

        public DayOfWeek DayOfWeek
        {
            get; set;
        }

        public Person Person
        {
            get; set;
        }

        #endregion Properties
    }
}