namespace Probel.Geho.Shell.Core
{
    using System.Collections.Generic;

    public class Argument
    {
        #region Constructors

        public Argument(string command, string argument)
        {
            this.Command = command;
            this.Args = argument;
        }

        #endregion Constructors

        #region Properties

        public string Args
        {
            get; private set;
        }

        public string Command
        {
            get; private set;
        }

        #endregion Properties
    }
}