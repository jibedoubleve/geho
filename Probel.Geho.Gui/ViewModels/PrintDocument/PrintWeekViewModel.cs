namespace Probel.Geho.Gui.ViewModels.PrintDocument
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Probel.Geho.Gui.ViewModels.Controls;

    public class PrintWeekViewModel
    {
        #region Constructors

        public PrintWeekViewModel(DisplayWeekViewModel week)
        {
            this.Days = new ObservableCollection<DisplayDayViewModel>(week.Days);
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<DisplayDayViewModel> Days
        {
            get;
            private set;
        }

        #endregion Properties
    }
}