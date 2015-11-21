namespace Probel.Geho.Data.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.Entities;

    public class DayDto : BaseDto
    {
        #region Properties

        public DateTime Date
        {
            get;
            set;
        }

        public GroupDto Group
        {
            get; set;
        }

        public bool IsMorning
        {
            get; set;
        }

        public IEnumerable<Person> Persons
        {
            get; set;
        }

        #endregion Properties
    }
}