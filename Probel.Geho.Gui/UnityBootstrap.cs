namespace Probel.Geho.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Practices.Unity;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Data.BusinessLogic.Schedule;

    class UnityBootstrap
    {
        #region Methods

        internal static void Initialise()
        {
            var container = new UnityContainer();
            container.RegisterType<IHrService, HrService>();
            container.RegisterType<IScheduleService, ScheduleService>();
        }

        #endregion Methods
    }
}