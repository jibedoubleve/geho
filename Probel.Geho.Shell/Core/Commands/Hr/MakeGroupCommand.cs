namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Shell.Helpers;

    [Command("make-group", "mkg", "Create a group.", "mkg -n:[name]")]
    public class MakeGroupCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var groupName = this.ArgProcessor.Get("-n");

            Output.InWhite.Write("Creating group '{0}'...", groupName);
            this.HrManager.CreateGroup(groupName);
            Output.InWhite.WriteLine(" Done");
        }

        #endregion Methods
    }
}