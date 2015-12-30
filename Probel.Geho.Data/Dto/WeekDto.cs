namespace Probel.Geho.Services.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WeekDto : BaseDto
    {
        #region Properties

        public IEnumerable<DayDto> Days
        {
            get; set;
        }

        public DateTime MondayDate
        {
            get;
            set;
        }

        #endregion Properties
    }
}