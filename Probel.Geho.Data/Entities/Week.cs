namespace Probel.Geho.Services.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Week : Entity
    {
        #region Properties

        public IList<Day> Days
        {
            get;
            set;
        }

        public DateTime Monday
        {
            get;
            set;
        }

        #endregion Properties
    }
}