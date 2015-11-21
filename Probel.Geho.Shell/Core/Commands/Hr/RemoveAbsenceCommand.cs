namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System.Collections.Generic;

    using Probel.Geho.Shell.Helpers;

    [Command("remove-absence", "rma", "Remove absence.", "rma -id:[id]")]
    public class RemoveAbsenceCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var strId = this.ArgProcessor.Get("id");

            var id = this.ArgProcessor.ParseId(strId);

            Output.InWhite.Write("Removing absence...");
            HrManager.RemoveAbsence(id);
            Output.InWhite.WriteLine("Done.");
        }

        #endregion Methods
    }
}