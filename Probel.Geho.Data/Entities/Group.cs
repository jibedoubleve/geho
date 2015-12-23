namespace Probel.Geho.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Group : Entity
    {
        #region Constructors

        public Group()
        {
            this.People = new List<Person>();
        }

        #endregion Constructors

        #region Properties

        [Required]
        public string Name
        {
            get;
            set;
        }

        public int Order
        {
            get; set;
        }

        public IList<Person> People
        {
            get;
            set;
        }

        #endregion Properties
    }
}