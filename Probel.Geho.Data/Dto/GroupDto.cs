﻿namespace Probel.Geho.Services.Dto
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

        public int Order
        {
            get; set;
        }

        public IList<PersonDto> People
        {
            get;
            set;
        }

        #endregion Properties
    }
}