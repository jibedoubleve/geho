namespace Probel.Geho.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Person : Entity
    {
        #region Constructors

        public Person()
        {
            this.Absences = new List<Absence>();
        }

        #endregion Constructors

        #region Properties

        public IList<Absence> Absences
        {
            get;
            set;
        }

        public IList<Activity> Activities
        {
            get; set;
        }

        public IList<Day> Days
        {
            get; set;
        }

        public Group Group
        {
            get; set;
        }

        public bool IsEducator
        {
            get; set;
        }

        [Required]
        public string Name
        {
            get; set;
        }

        [Required]
        public string Surname
        {
            get; set;
        }

        #endregion Properties
    }
}