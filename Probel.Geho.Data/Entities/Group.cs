namespace Probel.Geho.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

        public IList<Person> People
        {
            get;
            set;
        }

        #endregion Properties
    }
}