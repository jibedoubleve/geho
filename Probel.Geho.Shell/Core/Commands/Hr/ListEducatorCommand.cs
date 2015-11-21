namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Helpers;

    [Command("list-educator", "lse", "List educator", "se -sn:[surname] -n:[name]")]
    public class ListEducatorCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var surname = (this.ArgProcessor.HasArgs)
                    ? this.ArgProcessor.Get("sn")
                    : string.Empty;
                var name = (this.ArgProcessor.HasArgs)
                    ? this.ArgProcessor.Get("n")
                    : string.Empty;

                var b = HrManager.GetEducators(name, surname);
                DisplayList(b);
        }

        private static void DisplayList(IEnumerable<PersonDto> educators)
        {
            if (educators.Count() == 0) { Display.NoResultFor<PersonDto>(); }
            else
            {
                foreach (var b in educators.OrderBy(e => e.Id))
                {
                    Output.InWhite.WriteLine("[{0,2}] {1} {2}", b.Id, b.Name, b.Surname);
                }
            }
        }

        #endregion Methods
    }
}