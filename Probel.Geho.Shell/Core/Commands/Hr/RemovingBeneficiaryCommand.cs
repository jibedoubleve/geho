namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Shell.Helpers;

    [Command("remove-beneficiary", "rmb", "Remove a beneficiary", "rmb -id:[id]")]
    public class RemovingBeneficiaryCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var strId = this.ArgProcessor.Get("id");
            var id = this.ArgProcessor.ParseId(strId);

            Output.InWhite.Write("Removing educator...");
            HrManager.RemovePerson(id);
            Output.InWhite.WriteLine(" Done.");
        }

        #endregion Methods
    }
}