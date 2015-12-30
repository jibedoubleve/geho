namespace Probel.Geho.Services.Entities
{
    using System;
    using System.ComponentModel;
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

        [DefaultValue(false)]
        public bool IsPresent
        {
            get; set;
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