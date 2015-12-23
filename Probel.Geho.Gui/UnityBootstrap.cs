namespace Probel.Geho.Gui
{
    using Microsoft.Practices.Unity;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Data.BusinessLogic.Schedule;

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
            Container.RegisterInstance<IContext>(new Context());
        }

        #endregion Methods
    }
}