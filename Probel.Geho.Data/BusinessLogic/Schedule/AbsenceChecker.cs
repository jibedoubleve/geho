namespace Probel.Geho.Services.Business_Logic.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Probel.Geho.Services.Entities;

    internal class AbsenceChecker
    {
        #region Fields

        private readonly Absence Absence;
        private readonly DateTime Date;

        #endregion Fields

        #region Constructors

        public AbsenceChecker(Absence absence, DateTime date)
        {
            this.Absence = absence;
            this.Date = date;
        }

        #endregion Constructors

        #region Properties

        public bool IsAbsent
        {
            get
            {
                var result = (this.Date >= Absence.Start && this.Date <= Absence.End);
                return result;
            }
        }

        #endregion Properties
    }
}