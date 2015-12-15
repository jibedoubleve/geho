namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Data.BusinessLogic;
    using Data.Dto;

    using Probel.Mvvm.DataBinding;

    public class DisplayOneDayViewModel : LoadeableViewModel
    {
        #region Fields

        private DayOfWeek dayOfWeek;

        #endregion Fields

        #region Constructors

        public DisplayOneDayViewModel(IScheduleService srv, DateTime day, List<DayDto> days)
        {
            var groups = from d in days
                         group d by d.Group into g
                         select new DisplayOneDayGroupViewModel(srv
                             , day
                             , g.Key
                             , g.Where(e => e.IsMorning).Select(f => f.People).FirstOrDefault()
                             , g.Where(e => !e.IsMorning).Select(f => f.People).FirstOrDefault());

            this.Groups = new ObservableCollection<DisplayOneDayGroupViewModel>(groups);
            this.DayOfWeek = day.DayOfWeek;
        }

        #endregion Constructors

        #region Properties

        public DayOfWeek DayOfWeek
        {
            get { return this.dayOfWeek; }
            set
            {
                this.dayOfWeek = value;
                this.OnPropertyChanged(() => DayOfWeek);
            }
        }

        public ObservableCollection<DisplayOneDayGroupViewModel> Groups
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public override void Load()
        {
            foreach (var group in Groups)
            {
                group.Load();
            }
        }

        #endregion Methods
    }
}