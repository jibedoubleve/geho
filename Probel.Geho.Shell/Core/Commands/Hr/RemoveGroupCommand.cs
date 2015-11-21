namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Shell.Helpers;

    [Command("remove-group", "rmg", "Remove a group", "rmg -id:[id]")]
    public class RemoveGroupCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var strId = this.ArgProcessor.Get("id");
            var id = this.ArgProcessor.ParseId(strId);

            Output.InWhite.Write("Removing group...");
            this.HrManager.RemoveGroup(id);
            Output.InWhite.WriteLine(" Done");
        }

        #endregion Methods
    }
}