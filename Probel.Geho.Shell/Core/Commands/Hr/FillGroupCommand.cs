namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Helpers;

    [Command("fill-group", "fg", "Fill a group", "fg -grp:[group name] -bid:[id;id;id]")]
    public class FillGroupCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var group = this.ArgProcessor.Get("grp");
            var beneIds = this.ArgProcessor.Get("bid").Split(';');

            Output.InWhite.Write("Updating group {0}...", group);

            var grp = this.HrManager.GetGroup(group);
            if (grp == null)
            {
                Output.InYellow.WriteLine("No group with this name.");
                return;
            }

            var beneficiaries = new List<PersonDto>();
            foreach (var b in beneIds)
            {
                int id = 0;
                if (Int32.TryParse(b, out id))
                {
                    beneficiaries.Add(HrManager.GetBeneficiary(id));
                }
            }

            grp.People = beneficiaries;
            HrManager.UpdateGroup(grp);

            Output.InWhite.WriteLine(" Done");
        }

        #endregion Methods
    }
}