namespace Probel.Geho.Services.BusinessLogic.BusinessRules
{
    using System;
    using System.Linq;
    using System.Text;

    using Data.Properties;

    using Probel.Geho.Services.Database;
    using Probel.Geho.Services.Entities;

    internal class AbsenceValidator : IValidator
    {
        #region Fields

        private readonly Absence Absence;
        private readonly StringBuilder ErrorMessage = new StringBuilder();

        #endregion Fields

        #region Constructors

        public AbsenceValidator(Absence absence)
        {
            this.Absence = absence;
        }

        #endregion Constructors

        #region Properties

        public string Error
        {
            get { return this.ErrorMessage.ToString(); }
        }

        #endregion Properties

        #region Methods

        public bool Validate()
        {
            var isValid = true;
            isValid &= CheckFreePeriod();
            isValid &= CheckIfInPast();
            return isValid;
        }

        private bool CheckFreePeriod()
        {
            using (var db = new DataContext())
            {
                var start = this.Absence.Start;
                var end = this.Absence.End;
                if (Absence.Person == null)
                {
                    this.ErrorMessage.AppendLine(Messages.Validation_NobodyToAbsence);
                    return false;
                }

                var isValid = (from a in db.Absences
                               where a.Person.Id == Absence.Person.Id
                               && (
                                   (start >= a.Start && start <= a.End)
                                || (end >= a.Start && end <= a.End)
                                  )
                               select a).Count() == 0;
                if (!isValid) { this.ErrorMessage.AppendLine(Messages.Validation_AbsenceAtSameSpan); }
                return isValid;
            }
        }

        private bool CheckIfInPast()
        {
            var isValid = true; ;
            if (this.Absence.Start > this.Absence.End)
            {
                this.ErrorMessage.AppendLine(Messages.Validation_AbsenceStartBeforeEnd);
                isValid &= false;
            }
            if (this.Absence.End < DateTime.Today)
            {
                this.ErrorMessage.AppendLine(Messages.Validation_AbsenceEndInThePast);
                isValid &= false;
            }

            return isValid;
        }

        #endregion Methods
    }
}