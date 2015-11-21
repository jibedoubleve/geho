namespace Probel.Geho.Shell.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommand
    {
        #region Methods

        void Execute(IEnumerable<Argument> args);

        #endregion Methods
    }
}