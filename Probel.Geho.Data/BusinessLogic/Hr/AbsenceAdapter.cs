﻿namespace Probel.Geho.Data.BusinessLogic.Hr
{
    using System.Data.Entity;
    using System.Linq;

    using Probel.Geho.Data.BusinessLogic.BusinessRules;
    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Entities;

    internal class AbsenceAdapter
    {
        #region Fields

        private readonly Absence Absence;

        #endregion Fields

        #region Constructors

        public AbsenceAdapter(Absence absence)
        {
            this.Absence = absence;
        }

        #endregion Constructors

        #region Methods

        public AbsenceAdapter ClearOccupations(DataContext db)
        {
            var days = (from day in db.Days
                                   .Include(e => e.People)
                        where day.People.Where(p => p.Id == this.Absence.Person.Id).Count() != 0
                           && Absence.Start <= day.Date && day.Date <= Absence.End
                        select day).ToList();

            foreach (var day in days) { day.People.Remove(Absence.Person); }
            db.SaveChanges();
            return this;
        }

        public AbsenceAdapter Validate()
        {
            var validator = new AbsenceValidator(Absence);
            if (!validator.Validate()) { throw new BusinessRuleException(validator.Error); }
            return this;
        }

        #endregion Methods
    }
}