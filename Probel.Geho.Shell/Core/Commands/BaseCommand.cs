namespace Probel.Geho.Shell.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Data.BusinessLogic.Schedule;

    public abstract class BaseCommand : ICommand
    {
        #region Fields

        protected readonly IHrService HrManager = new HrService();
        protected readonly IScheduleService ScheduleManager = new ScheduleService();

        #endregion Fields

        #region Properties

        protected ArgumentProcessor ArgProcessor
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public void Execute(IEnumerable<Argument> args)
        {
            this.ArgProcessor = new ArgumentProcessor(args);
            this.ExecuteCommand(args);
        }

        protected abstract void ExecuteCommand(IEnumerable<Argument> args);

        #endregion Methods
    }
}