namespace Probel.Geho.Gui.ViewModels.PrintDocument
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Probel.Geho.Gui.ViewModels.Controls;

    public class PrintDayViewModel : BaseViewModel
    {
        #region Fields

        private DayOfWeek day;

        #endregion Fields

        #region Constructors

        public PrintDayViewModel(IEnumerable<DisplayOneDayGroupViewModel> groups, DayOfWeek day)
        {
            this.Day = day;
            this.Groups = new ObservableCollection<DisplayOneDayGroupViewModel>(groups);
        }

        #endregion Constructors

        #region Properties

        public DayOfWeek Day
        {
            get { return this.day; }
            set
            {
                this.day = value;
                this.OnPropertyChanged(() => Day);
            }
        }

        public ObservableCollection<DisplayOneDayGroupViewModel> Groups
        {
            get;
            private set;
        }

        #endregion Properties
    }
}