namespace Probel.Geho.Data.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GroupDto : BaseDto
    {
        #region Properties

        public string Name
        {
            get;
            set;
        }

        public IList<PersonDto> Persons
        {
            get;
            set;
        }

        #endregion Properties
    }
}