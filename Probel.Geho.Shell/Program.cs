namespace Probel.Geho.Shell
{
    using System;
    using System.Linq;

    using Probel.Geho.Data;
    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Shell.Core;

    class Program
    {
        #region Methods

        private static void Initialise()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Cfg.DataDirectory);
            DataBootstrap.Initialise();
        }

        static void Main(string[] args)
        {
            Initialise();

            var cmd = new CommandShell();
            cmd.Start("quit", "exit", "bye");
        }

        #endregion Methods
    }
}