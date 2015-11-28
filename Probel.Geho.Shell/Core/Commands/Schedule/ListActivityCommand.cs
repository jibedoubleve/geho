namespace Probel.Geho.Shell.Core.Commands.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Helpers;

    [Command("list-activity", "lsac", "List all activities")]
    public class ListActivityCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var people = HrManager.GetPersons();

            foreach (var p in people)
            {
                Output.InDarkYellow.WriteLine("Activities for [{0}] {1} {2}", p.IsEducator ? "EDUCATOR" : "BENEFICIARY", p.Name, p.Surname);
                if (p.Activities.Count() == 0) { Display.NoResultFor<ActivityDto>(); }
                else
                {
                    foreach (var a in p.Activities)
                    {
                        Output.InWhite.WriteLine("'{0}' {1} {2}", a.Name
                            , a.DayOfWeek
                            , a.MomentDay.ToString());
                    }
                }
            }
        }

        #endregion Methods
    }
}