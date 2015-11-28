namespace Probel.Geho.Shell.Core.Commands.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Shell.Helpers;

    [Command("list-free", "lsf", "List free educators", "lsf -date:[date] -isMorning:[true|false]")]
    public class ListFreeEducatorCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var dateStr = ArgProcessor.Get("date");
            var date = ArgProcessor.ParseDate(dateStr);

            var isMorningStr = ArgProcessor.Get("isMorning");
            var isMorning = ArgProcessor.ParseBool(isMorningStr);

            var educators = HrManager.GetEducatorWithoutActivities(date.DayOfWeek, isMorning);

            foreach (var e in educators)
            {
                Output.InWhite.WriteLine("[{0}] {1} {2}", e.IsEducator ? "EDUCATOR" : "BENEFICIARY", e.Name, e.Surname);
            }
        }

        #endregion Methods
    }
}