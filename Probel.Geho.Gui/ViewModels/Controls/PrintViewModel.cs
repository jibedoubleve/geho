namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Properties;

    public class PrintViewModel : BaseViewModel
    {
        #region Fields

        private readonly Settings Settings = Settings.Default;

        #endregion Fields

        #region Constructors

        public PrintViewModel()
        {
        }

        #endregion Constructors

        #region Properties

        public bool IsActivitiesSelected
        {
            get { return Settings.IsActivitiesSelected; }
            set
            {
                Settings.IsActivitiesSelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsActivitiesSelected);
            }
        }

        public bool IsFridaySelected
        {
            get { return Settings.IsFridaySelected; }
            set
            {
                Settings.IsFridaySelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsFridaySelected);
            }
        }

        public bool IsLunchSelected
        {
            get { return Settings.IsLunchSelected; }
            set
            {
                Settings.IsLunchSelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsLunchSelected);
            }
        }

        public bool IsMondaySelected
        {
            get { return Settings.IsMondaySelected; }
            set
            {
                Settings.IsMondaySelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsMondaySelected);
            }
        }

        public bool IsThursdaySelected
        {
            get { return Settings.IsThursdaySelected; }
            set
            {
                Settings.IsThursdaySelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsThursdaySelected);
            }
        }

        public bool IsTuesdaySelected
        {
            get { return Settings.IsTuesdaySelected; }
            set
            {

                Settings.IsTuesdaySelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsTuesdaySelected);
            }
        }

        public bool IsWednesdaySelected
        {
            get { return Settings.IsWednesdaySelected; }
            set
            {
                Settings.IsWednesdaySelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsWednesdaySelected);
            }
        }

        public bool IsWeekSelected
        {
            get { return Settings.IsWeekSelected; }
            set
            {
                Settings.IsWeekSelected = value;
                Settings.Save();
                this.OnPropertyChanged(() => IsWeekSelected);
            }
        }

        #endregion Properties
    }
}