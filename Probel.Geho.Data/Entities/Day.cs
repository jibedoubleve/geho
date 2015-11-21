namespace Probel.Geho.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;

    [DebuggerDisplay("{Date}")]
    public class Day : Entity
    {
        #region Constructors

        public Day()
        {
            this.Persons = new List<Person>();
        }

        #endregion Constructors

        #region Properties

        [Required]
        public DateTime Date
        {
            get;
            set;
        }

        public Group Group
        {
            get;
            set;
        }

        [Required]
        public bool IsMorning
        {
            get; set;
        }

        public IList<Person> Persons
        {
            get;
            set;
        }

        #endregion Properties
    }
}