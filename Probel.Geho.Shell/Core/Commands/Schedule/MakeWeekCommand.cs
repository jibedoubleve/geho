namespace Probel.Geho.Shell.Core.Commands.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Shell.Helpers;

    [Command("make-week", "mkw", "Create a new week.", "mkw -date:[date]")]
    public class MakeWeekCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            Output.InWhite.Write("Creating week...");

            var strDate = this.ArgProcessor.Get("-date");
            var date = this.ArgProcessor.ParseDate(strDate);

            if (!ScheduleManager.WeekExists(date))
            {
                this.ScheduleManager.CreateWeek(date);
            }
            else { throw new BusinessRuleException("This week is already set"); }
            Output.InWhite.WriteLine(" Done");
        }

        #endregion Methods
    }
}