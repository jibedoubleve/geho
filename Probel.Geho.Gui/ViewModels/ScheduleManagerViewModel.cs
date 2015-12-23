namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Helpers;
    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Mvvm.DataBinding;
    using Probel.Mvvm.Gui;

    using Runtime;

    public class ScheduleManagerViewModel : BaseViewModel, ILoadeableViewModel
    {
        #region Fields

        public readonly IContext AppContext;

        private readonly IScheduleService Service;

        private WeekDto currentWeek;
        private ManageDayViewModel selectedDay;
        private DateTime weekDate = DateTime.Today;

        #endregion Fields

        #region Constructors

        public ScheduleManagerViewModel(IScheduleService srv, IContext context)
        {
            this.AppContext = context;
            this.Service = srv;

            this.Days = new ObservableCollection<ManageDayViewModel>();
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

        public ObservableCollection<ManageDayViewModel> Days
        {
            get;
            private set;
        }

        public ManageDayViewModel SelectedDay
        {
            get { return this.selectedDay; }
            set
            {
                this.selectedDay = value;
                this.AppContext.WeekToManageSelectedDay = value.CurrentDay.Date;
                this.OnPropertyChanged(() => SelectedDay);
            }
        }

        public DateTime WeekDate
        {
            get { return this.weekDate; }
            set
            {
                this.weekDate
                    = AppContext.WeekToManage
                    = value;
                this.LoadWeek();
                this.OnPropertyChanged(() => WeekDate);
            }
        }

        #endregion Properties

        #region Methods

        public void Load()
        {
            this.WeekDate = AppContext.WeekToManage;
        }

        public Task LoadAsync()
        {
            throw new NotImplementedException();
        }

        private async void LoadWeek()
        {
            try
            {
                this.StatusBar.Loading();
                using (WaitingCursor.While)
                {
                    if (!this.Service.WeekExists(this.WeekDate))
                    {
                        var yes = ViewService.MessageBox.Question(Messages.Msg_AskCreateNewWeek);
                        if (yes) { this.Service.CreateWeek(this.WeekDate); }
                        else { return; }
                    }

                    var r = await Task.Run(() =>
                    {
                        return new
                        {
                            CurrentWeek = Service.GetWeek(this.weekDate),
                            Groups = Service.GetGroups(),
                            CurrentDay = this.WeekDate.GetMonday(),
                            Days = new List<ManageDayViewModel>(),
                        };
                    });

                    this.CurrentWeek = r.CurrentWeek;
                    for (int i = 0; i < 5; i++)
                    {
                        var d = r.CurrentDay.AddDays(i);
                        var dvm = new ManageDayViewModel(d, r.Groups, Service, this);
                        dvm.Load();
                        r.Days.Add(dvm);
                    }
                    this.Days.Refill(r.Days);

                    var sd = (from d in this.Days
                              where d.CurrentDay.Date == AppContext.WeekToManageSelectedDay
                              select d).FirstOrDefault();
                    if (sd != null) { this.SelectedDay = sd; }

                    this.StatusBar.Ready();
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