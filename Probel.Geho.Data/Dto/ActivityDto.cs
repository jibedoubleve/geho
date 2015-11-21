namespace Probel.Geho.Data.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ActivityDto : BaseDto
    {
        #region Properties

        public DayOfWeek DayOfWeek
        {
            get;
            set;
        }

        public bool IsMorning
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IEnumerable<PersonDto> People
        {
            get;
            set;
        }

        #endregion Properties
    }
}