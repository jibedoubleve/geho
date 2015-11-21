namespace Probel.Geho.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;

    [DebuggerDisplay("Absence: {Id}")]
    public class Absence : Entity
    {
        #region Properties

        [Required]
        public DateTime End
        {
            get;
            set;
        }

        [Required]
        public Person Person
        {
            get;
            set;
        }

        [Required]
        public DateTime Start
        {
            get;
            set;
        }

        #endregion Properties
    }
}