namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Controls;

    using Mvvm.Gui;

    using Probel.Geho.Services.BusinessLogic;
    using Probel.Mvvm.Toolkit.DataBinding;

    using Properties;

    using Runtime;

    using Services.Helpers;

    public class ScheduleEditorViewModel : BaseViewModel
    {
        #region Fields

        private readonly IScheduleService Service;

        private EditDayViewModel editDayViewModel;
        private DateTime selectedDay;
        private DateTime selectedWeek;
        private DateTime weekDateToAdd;

        #endregion Fields

        #region Constructors

        public ScheduleEditorViewModel(IScheduleService service)
        {
            this.RefreshDatesCommand = new RelayCommand(RefreshDates);

            this.AddWeekCommand = new RelayCommand(AddWeek, CanAddWeek);
            this.LoadFreeWeeksCommand = new RelayCommand(LoadFreeWeeks);

            this.editDayViewModel = new EditDayViewModel(service);
            this.NewWeekDates = new ObservableCollection<DateTime>();
            this.WeekDates = new ObservableCollection<DateTime>();
            this.Days = new ObservableCollection<DateTime>();
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        public ICommand AddWeekCommand
        {
            get;
            private set;
        }

        public ObservableCollection<DateTime> Days
        {
            get;
            private set;
        }

        public EditDayViewModel EditDayViewModel
        {
            get { return this.editDayViewModel; }
            set
            {
                this.editDayViewModel = value;
                this.OnPropertyChanged(() => EditDayViewModel);
            }
        }

        public bool ExcludePastWeeks
        {
            get { return Settings.Default.IsPastWeekExcluded; }
            set
            {
                Settings.Default.IsPastWeekExcluded = value;
                Settings.Default.Save();
                this.OnPropertyChanged(() => ExcludePastWeeks);
                this.Load();
            }
        }

        public ICommand LoadFreeWeeksCommand
        {
            get;
            private set;
        }

        public ObservableCollection<DateTime> NewWeekDates
        {
            get;
            private set;
        }

        public ICommand RefreshDatesCommand
        {
            get;
            private set;
        }

        public DateTime SelectedDay
        {
            get { return this.selectedDay; }
            set
            {
                this.selectedDay = value;

                NavigationContext.WeekToManageSelectedDay = value.Date.Date;
                this.EditDayViewModel.Load(value);
                this.OnPropertyChanged(() => SelectedDay);
            }
        }

        public DateTime SelectedWeek
        {
            get { return this.selectedWeek; }
            set
            {
                this.selectedWeek = value;
                NavigationContext.WeekToDisplay = value.Date.Date;
                this.RefillDays();
                this.OnPropertyChanged(() => SelectedWeek);
            }
        }

        public ObservableCollection<DateTime> WeekDates
        {
            get;
            private set;
        }

        public DateTime WeekDateToAdd
        {
            get { return this.weekDateToAdd; }
            set
            {
                this.weekDateToAdd = value;
                this.OnPropertyChanged(() => WeekDateToAdd);
            }
        }

        #endregion Properties

        #region Methods

        public async Task Load()
        {
            Status.Loading();
            using (WaitingCursor.While)
            {
                var weekdates = await Task.Run(() => this.Service.GetWeekDates(isPastExcluded: this.ExcludePastWeeks));
                this.WeekDates.Refill(weekdates);

                SelectSavedWeek();
                SelectSavedDay();
            }
        }

        private async void AddWeek()
        {
            this.Service.CreateWeek(this.WeekDateToAdd);
            this.Status.InfoFormat(Messages.Msg_WeekAdded, WeekDateToAdd.ToString("D"));
            await Load();
        }

        private bool CanAddWeek()
        {
            return this.WeekDateToAdd != null;
        }

        private async void LoadFreeWeeks()
        {
            var weeks = await Task.Run(() => Service.GetNotConfiguredMondays(DateTime.Today));
            this.NewWeekDates.Refill(weeks);
            this.WeekDateToAdd = this.NewWeekDates.ElementAt(0);
        }

        private void RefillDays()
        {
            var days = new List<DateTime>();
            for (int i = 0; i < 5; i++)
            {
                days.Add(SelectedWeek + new TimeSpan(i, 0, 0, 0));
            }
            this.Days.Refill(days);
        }

        private async void RefreshDates()
        {
            await Load();
        }

        private void SelectSavedDay()
        {
            var sd = (from d in this.Days
                      where d.Date == NavigationContext.WeekToManageSelectedDay.Date
                      select d).FirstOrDefault();
            if (sd != null) { this.SelectedDay = sd; }
        }

        private void SelectSavedWeek()
        {
            var sw = (from w in this.WeekDates
                      where w.Date == NavigationContext.WeekToDisplay.Date
                      select w).FirstOrDefault();
            if (sw != null) { this.SelectedWeek = sw; }
        }

        #endregion Methods
    }
}