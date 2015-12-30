namespace Probel.Geho.Services.BusinessLogic.Hr
{
    using System.Data.Entity;
    using System.Linq;

    using Probel.Geho.Services.BusinessLogic.BusinessRules;
    using Probel.Geho.Services.Database;
    using Probel.Geho.Services.Entities;

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

        /// <summary>
        /// Remove persons from groups in day during the absence. In other words,
        /// in the time table, the absent person won't appear.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
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