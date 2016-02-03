namespace Probel.Geho.Services.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Activity : Entity
    {
        #region Properties

        [Required]
        public DayOfWeek DayOfWeek
        {
            get; set;
        }

        [Required]
        [DefaultValue(true)]
        public bool IsActive
        {
            get; set;
        }

        [Required]
        public MomentDay MomentDay
        {
            get; set;
        }

        public string Name
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