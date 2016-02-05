namespace Probel.Geho.Gui.Runtime
{
    using System;

    using Probel.Geho.Services.Helpers;

    public static class NavigationContext
    {
        #region Constructors

        static NavigationContext()
        {
            WeekToDisplay = DateTime.Today.GetMonday();
            WeekToManage = DateTime.Today.GetMonday();
            WeekToDisplayTab = string.Empty;
        }

        #endregion Constructors

        #region Properties

        public static DateTime WeekToDisplay
        {
            get; set;
        }

        public static string WeekToDisplayTab
        {
            get; set;
        }

        public static DateTime WeekToManage
        {
            get; set;
        }

        public static DateTime WeekToManageSelectedDay
        {
            get; set;
        }

        public static int WeekToManageSelectedGroup
        {
            get; set;
        }

        #endregion Properties
    }
}