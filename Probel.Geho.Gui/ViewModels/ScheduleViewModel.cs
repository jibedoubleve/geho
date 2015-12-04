namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Helpers;
    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Mvvm.DataBinding;
    using Probel.Mvvm.Gui;

    public class ScheduleViewModel : ObservableObject
    {
        #region Fields

        private readonly ICommand loadWeekCommand;
        private readonly IScheduleService Service;

        private WeekDto currentWeek;
        private DateTime weekDate = DateTime.Today;

        #endregion Fields

        #region Constructors

        public ScheduleViewModel(IScheduleService srv)
        {
            this.Service = srv;

            this.Days = new ObservableCollection<DayViewModel>();

            this.loadWeekCommand = new RelayCommand(LoadWeek, CanLoadWeek);
        }

        #endregion Constructors

        #region Properties

        public WeekDto CurrentWeek
        {
            get { return this.currentWeek; }
            set
            {
                this.currentWeek = value;
                this.OnPropertyChanged(() => CurrentWeek);
            }
        }

        public ObservableCollection<DayViewModel> Days
        {
            get;
            private set;
        }

        public ICommand LoadWeekCommand
        {
            get { return this.loadWeekCommand; }
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

        #region Methods

        public void NextWeek()
        {
            WeekDate = WeekDate.AddDays(7).GetMonday();
        }

        internal void PreviousWeek()
        {
            WeekDate = WeekDate.AddDays(-7).GetMonday();
        }

        private bool CanLoadWeek()
        {
            return true;
        }

        private void LoadWeek()
        {
            try
            {
                using (WaitingCursor.While)
                {
                    if (!this.Service.WeekExists(this.WeekDate))
                    {
                        var yes = ViewService.MessageBox.Question(Messages.Msg_AskCreateNewWeek);
                        if (yes) { this.Service.CreateWeek(this.WeekDate); }
                        else { return; }
                    }
                    this.CurrentWeek = Service.GetWeek(this.weekDate);
                    var groups = Service.GetGroups();
                    var currentDay = this.WeekDate.GetMonday();
                    var days = new List<DayViewModel>();

                    for (int i = 0; i < 5; i++)
                    {
                        var d = currentDay.AddDays(i);
                        var dvm = new DayViewModel(d, groups, Service, this);
                        days.Add(dvm);
                    }
                    this.Days.Refill(days);
                }
            }
            catch (Exception ex)
            {
                ViewService.MessageBox.Error(ex.ToString());
            }
        }

        #endregion Methods
    }
}