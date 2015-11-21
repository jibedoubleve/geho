namespace Probel.Geho.Shell.Core.Commands.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Helpers;

    [Command("make-educator", "mke", "Create an educator.", "mke -sn:[surname] -n:[name]")]
    public class MakeEducatorCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            var surname = this.ArgProcessor.Get("-sn");
            var name = this.ArgProcessor.Get("n");

            Output.InWhite.Write("Adding new educator [{0} {1}]...", name, surname);
            this.HrManager.CreatePerson(new PersonDto() { Name = name, Surname = surname });
            Output.InWhite.WriteLine(" Done.");
        }

        #endregion Methods
    }
}