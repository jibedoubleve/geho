﻿namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Data.Dto;

    using Probel.Mvvm.DataBinding;

    public class DisplayDayViewModel : ObservableObject
    {
        #region Fields

        private DayOfWeek dayOfWeek;

        #endregion Fields

        #region Constructors

        public DisplayDayViewModel(DayOfWeek dayOfWeek, IEnumerable<DayDto> days)
        {
            var groups = from d in days
                         group d by d.Group into g
                         select new DisplayGroupViewModel(
                             g.Key.Name
                             , g.Where(e => e.IsMorning).Select(f => f.People).FirstOrDefault()
                             , g.Where(e => !e.IsMorning).Select(f => f.People).FirstOrDefault());

            this.Groups = new ObservableCollection<DisplayGroupViewModel>(groups);
            this.DayOfWeek = dayOfWeek;
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

        public ObservableCollection<DisplayGroupViewModel> Groups
        {
            get;
            private set;
        }

        #endregion Properties
    }
}