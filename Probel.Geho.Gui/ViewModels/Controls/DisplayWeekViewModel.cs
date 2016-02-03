namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Mvvm.Toolkit.DataBinding;

    public class DisplayWeekViewModel : ObservableObject
    {
        #region Fields

        private DateTime weekDate;

        #endregion Fields

        #region Constructors

        public DisplayWeekViewModel(IEnumerable<DisplayDayViewModel> days)
        {
            this.Days = new ObservableCollection<DisplayDayViewModel>(days);
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<DisplayDayViewModel> Days
        {
            get;
            private set;
        }

        public DateTime WeekDate
        {
            get { return this.weekDate; }
            set
            {
                this.weekDate = value;
                this.OnPropertyChanged(() => WeekDate);
            }
        }

        #endregion Properties
    }
}