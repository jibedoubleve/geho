namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Helpers;

    [Command("list-group", "lsg", "List group", "lg -n:[name]")]
    public class ListGroupCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            if (this.ArgProcessor.HasArgs)
            {
                var name = this.ArgProcessor.Get("n");
                DisplayOneGroup(name);
            }
            else { this.DisplayAllGroups(); }
        }

        private static void DisplayGroup(GroupDto group)
        {
            Output.InWhite.WriteLine();
            Output.InYellow.WriteLine("[{1,3}] {0}", group.Name, group.Id);
            Output.InWhite.WriteLine("----------------------------");
            Output.InDarkYellow.WriteLine("Beneficiaries:");
            foreach (var b in group.People)
            {
                Output.InWhite.WriteLine("\t[{0}] {1} {2}", b.IsEducator ? "EDUCATOR" : "BENEFICIARY", b.Name, b.Surname);
            }
        }

        private void DisplayAllGroups()
        {
            var groups = this.HrManager.GetGroups();
            if (groups.Count() == 0) { Display.NoResultFor<GroupDto>(); }
            else
            {
                foreach (var group in groups)
                {
                    DisplayGroup(group);
                }
            }
        }

        private void DisplayOneGroup(string name)
        {
            var group = this.HrManager.GetGroup(name);

            if (group == null)
            {
                Output.InYellow.WriteLine("No group with this name");
                return;
            }

            DisplayGroup(group);
        }

        #endregion Methods
    }
}