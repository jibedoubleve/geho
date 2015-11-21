namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Helpers;

    [Command("list-absence", "lsa", "List all the absences")]
    public class ListAbsenceCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var absences = this.HrManager.GetAbsences();
            if (absences.Count() == 0) { Display.NoResultFor<AbsenceDto>(); }
            else
            {
                foreach (var absence in absences)
                {
                    Output.InYellow.WriteLine("Absence {0,3}", absence.Id);
                    Output.InDarkYellow.WriteLine("\t {0} {1}", absence.Person.Name, absence.Person.Surname);
                    Output.InWhite.WriteLine("\t Start: {0}", absence.Start.ToString("dd/MM/yyyy"));
                    Output.InWhite.WriteLine("\t End  : {0}", absence.End.ToString("dd/MM/yyyy"));
                }
            }
        }

        #endregion Methods
    }
}