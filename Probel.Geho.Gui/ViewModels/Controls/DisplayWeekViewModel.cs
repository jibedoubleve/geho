namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Probel.Mvvm.DataBinding;

    public class DisplayWeekViewModel : ObservableObject
    {
        #region Constructors

        public DisplayWeekViewModel(List<DisplayDayViewModel> days)
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

        #endregion Properties
    }
}