namespace Probel.Geho.Services.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseDto
    {
        #region Properties

        [Key]
        public int Id
        {
            get; set;
        }

        #endregion Properties
    }
}