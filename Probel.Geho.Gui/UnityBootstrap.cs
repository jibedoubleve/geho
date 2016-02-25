namespace Probel.Geho.Gui
{
    using Microsoft.Practices.Unity;

    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.BusinessLogic.Hr;
    using Probel.Geho.Services.BusinessLogic.Schedule;

    using Runtime;

    class UnityBootstrap
    {
        #region Fields

        internal static IUnityContainer Container;

        #endregion Fields

        #region Constructors

        static UnityBootstrap()
        {
            Container = new UnityContainer();
        }

        #endregion Constructors

        #region Methods

        internal static void Initialise()
        {
            Container.RegisterType<IHrService, HrService>();
            Container.RegisterType<IScheduleService, ScheduleService>();
        }

        #endregion Methods
    }
}