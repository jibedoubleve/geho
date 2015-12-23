namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Controls;

    using Data.Dto;
    using Data.Helpers;

    using Mvvm.Gui;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Mvvm.DataBinding;

    using Runtime;

    using Tools;

    public class ScheduleDisplayViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly IContext AppContext;
        private readonly IScheduleService Service;

        private ActivityListViewModel activityListViewModel;
        private DisplayLunchViewModel displayLunchViewModel;
        private DisplayWeekViewModel displayWeekViewModel;
        private DisplayOneDayViewModel friday;
        private DisplayOneDayViewModel monday;
        private DateTime selectedDate;
        private DisplayOneDayViewModel thursday;
        private DisplayOneDayViewModel tuesday;
        private DisplayOneDayViewModel wednesday;

        #endregion Fields

        #region Constructors

        public ScheduleDisplayViewModel(IScheduleService service, IContext appContext)
        {
            this.AppContext = appContext;
            this.Mondays = new ObservableCollection<DateTime>();
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        public ActivityListViewModel ActivityListViewModel
        {
            get { return this.activityListViewModel; }
            set
            {
                this.activityListViewModel = value;
                this.OnPropertyChanged(() => ActivityListViewModel);
            }
        }

        public DisplayLunchViewModel DisplayLunchViewModel
        {
            get { return this.displayLunchViewModel; }
            set
            {
                this.displayLunchViewModel = value;
                this.OnPropertyChanged(() => DisplayLunchViewModel);
            }
        }

        public DisplayWeekViewModel DisplayWeekViewModel
        {
            get { return this.displayWeekViewModel; }
            set
            {
                this.displayWeekViewModel = value;
                this.OnPropertyChanged(() => DisplayWeekViewModel);
            }
        }

        public DisplayOneDayViewModel Friday
        {
            get { return this.friday; }
            set
            {
                this.friday = value;
                this.OnPropertyChanged(() => Friday);
            }
        }

        public DisplayOneDayViewModel Monday
        {
            get { return this.monday; }
            set
            {
                this.monday = value;
                this.OnPropertyChanged(() => Monday);
            }
        }

        public ObservableCollection<DateTime> Mondays
        {
            get;
            private set;
        }

        public DateTime SelectedDate
        {
            get { return this.selectedDate; }
            set
            {
                this.selectedDate
                    = AppContext.WeekToDisplay
                    = value;
                this.OnPropertyChanged(() => SelectedDate);
                this.LoadWeek();
            }
        }

        public DisplayOneDayViewModel Thursday
        {
            get { return this.thursday; }
            set
            {
                this.thursday = value;
                this.OnPropertyChanged(() => Thursday);
            }
        }

        public DisplayOneDayViewModel Tuesday
        {
            get { return this.tuesday; }
            set
            {
                this.tuesday = value;
                this.OnPropertyChanged(() => Tuesday);
            }
        }

        public DisplayOneDayViewModel Wednesday
        {
            get { return this.wednesday; }
            set
            {
                this.wednesday = value;
                this.OnPropertyChanged(() => Wednesday);
            }
        }

        #endregion Properties

        #region Methods

        public override async void Load()
        {
            using (WaitingCursor.While)
            {
                this.StatusBar.Loading();

                var mondays = await Task.Run(() => this.Service.GetMondays());
                this.Mondays.Refill(mondays);
                SelectedFirstMonday();
                this.DisplayLunchViewModel = new DisplayLunchViewModel(this.Service);
                this.ActivityListViewModel = new ActivityListViewModel(this.Service);
                this.DisplayLunchViewModel.Load();
                this.ActivityListViewModel.Load();
                this.StatusBar.Ready();
            }
        }

        public async void LoadWeek()
        {
            using (WaitingCursor.While)
            {
                this.StatusBar.Loading();
                var week = await Task.Run(() => this.Service.GetWeek(this.SelectedDate));
                var monday = this.SelectedDate.GetMonday();

                this.DisplayFullWeek(week);

                Monday = this.FillDay(week, monday);
                Tuesday = this.FillDay(week, monday.AddDays(1));
                Wednesday = this.FillDay(week, monday.AddDays(2));
                Thursday = this.FillDay(week, monday.AddDays(3));
                Friday = this.FillDay(week, monday.AddDays(4));
                this.StatusBar.Ready();
            }
        }

        private void DisplayFullWeek(WeekDto week)
        {
            var days = (from d in week.Days
                        group d by d.Date.DayOfWeek into g
                        select new DisplayDayViewModel(g.Key, g.ToList())
                      )
                      .OrderBy(e => e.DayOfWeek)
                      .ToList();
            this.DisplayWeekViewModel = new DisplayWeekViewModel(days);
        }

        private DisplayOneDayViewModel FillDay(WeekDto week, DateTime date)
        {
            var days = (from d in week.Days
                        where d.Date.DayOfWeek == date.DayOfWeek
                        select d).ToList();
            var vm = new DisplayOneDayViewModel(this.Service, date, days);
            vm.Load();
            return vm;
        }

        private void SelectedFirstMonday()
        {
            var date = (from d in Mondays
                        where d == AppContext.WeekToDisplay
                        select d).FirstOrDefault();

            if (date != null) { this.SelectedDate = date; }
            else if (this.Mondays.Count > 0)
            {
                this.SelectedDate = this.Mondays[0];
            }
        }

        #endregion Methods
    }
}