namespace Probel.Geho.Data.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AbsenceDto : BaseDto
    {
        #region Properties

        public DateTime End
        {
            get; set;
        }

        public PersonDto Person
        {
            get; set;
        }

        public DateTime Start
        {
            get; set;
        }

        #endregion Properties
    }
}