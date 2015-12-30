namespace Probel.Geho.Gui.Runtime
{
    using System;

    using Probel.Geho.Services.Helpers;

    public interface IContext
    {
        #region Properties

        DateTime WeekToDisplay
        {
            get; set;
        }

        string WeekToDisplayTab
        {
            get; set;
        }

        DateTime WeekToManage
        {
            get; set;
        }

        DateTime WeekToManageSelectedDay
        {
            get; set;
        }

        int WeekToManageSelectedGroup
        {
            get; set;
        }

        #endregion Properties
    }

    public class Context : IContext
    {
        #region Constructors

        public Context()
        {
            this.WeekToDisplay = DateTime.Today.GetMonday();
            this.WeekToManage = DateTime.Today.GetMonday();
            this.WeekToDisplayTab = string.Empty;
        }

        #endregion Constructors

        #region Properties

        public DateTime WeekToDisplay
        {
            get; set;
        }

        public string WeekToDisplayTab
        {
            get; set;
        }

        public DateTime WeekToManage
        {
            get; set;
        }

        public DateTime WeekToManageSelectedDay
        {
            get; set;
        }

        public int WeekToManageSelectedGroup
        {
            get; set;
        }

        #endregion Properties
    }
}