namespace Probel.Geho.Shell.Core.Commands.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Command("clear", "cls", "Clear the screen.")]
    public class ClearCommand : ICommand
    {
        #region Methods

        public void Execute(IEnumerable<Argument> args)
        {
            Console.Clear();

            Console.WriteLine("Schelule manager");
            Console.WriteLine("----------------");
        }

        #endregion Methods
    }
}