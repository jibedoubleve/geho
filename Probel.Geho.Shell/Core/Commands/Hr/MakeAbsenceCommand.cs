namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Command("make-absence", "mka", "Create an absence.", "mka -id:[id] -start:[dd/mm/yyyy hh:mm] -end:[dd/mm/yyyy hh:mm]")]
    public class MakeAbsenceCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var strId = this.ArgProcessor.Get("-id");
            var strFrom = this.ArgProcessor.Get("-start");
            var strTo = this.ArgProcessor.Get("-end");

            var id = this.ArgProcessor.ParseId(strId);
            var start = this.ArgProcessor.ParseDate(strFrom);
            var end = this.ArgProcessor.ParseDate(strTo);

            this.HrManager.CreateAbsence(id, start, end);
        }

        #endregion Methods
    }
}