namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Data.BusinessLogic;

    using Mvvm.Gui;

    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

    public class LunchManagementViewModel : ObservableObject, ILoadeableViewModel
    {
        #region Fields

        private readonly IHrService Service;
        private readonly ICommand updateCommand;

        private PersonDto friday;
        private PersonDto monday;
        private PersonDto thursday;
        private PersonDto tuesday;
        private PersonDto wednesday;

        #endregion Fields

        #region Constructors

        public LunchManagementViewModel(IHrService service)
        {
            this.Service = service;
            this.Trainees = new ObservableCollection<PersonDto>();
            this.Week = new ObservableCollection<LunchTimeDto>();
            this.updateCommand = new RelayCommand(Update, CanUpdate);
        }

        #endregion Constructors

        #region Properties

        public PersonDto Friday
        {
            get { return this.friday; }
            set
            {
                this.friday = value;
                this.OnPropertyChanged(() => Friday);
            }
        }

        public PersonDto Monday
        {
            get { return this.monday; }
            set
            {
                this.monday = value;
                this.OnPropertyChanged(() => Monday);
            }
        }

        public PersonDto Thursday
        {
            get { return this.thursday; }
            set
            {
                this.thursday = value;
                this.OnPropertyChanged(() => Thursday);
            }
        }

        public ObservableCollection<PersonDto> Trainees
        {
            get;
            private set;
        }

        public PersonDto Tuesday
        {
            get { return this.tuesday; }
            set
            {
                this.tuesday = value;
                this.OnPropertyChanged(() => Tuesday);
            }
        }

        public ICommand UpdateCommand
        {
            get { return this.updateCommand; }
        }

        public PersonDto Wednesday
        {
            get { return this.wednesday; }
            set
            {
                this.wednesday = value;
                this.OnPropertyChanged(() => Wednesday);
            }
        }

        public ObservableCollection<LunchTimeDto> Week
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public void Load()
        {
            var trainees = this.Service.GetTrainees();
            this.Trainees.Refill(trainees);
            this.Week.Refill(this.Service.GetLunchTimes());

            this.Monday = this.GetLunch(DayOfWeek.Monday);
            this.Tuesday = this.GetLunch(DayOfWeek.Tuesday);
            this.Wednesday = this.GetLunch(DayOfWeek.Wednesday);
            this.Thursday = this.GetLunch(DayOfWeek.Thursday);
            this.Friday = this.GetLunch(DayOfWeek.Friday);
        }

        private bool CanUpdate()
        {
            return this.Monday != null
                && this.Tuesday != null
                && this.Wednesday != null
                && this.Thursday != null
                && this.Friday != null;
        }

        private PersonDto GetLunch(DayOfWeek day)
        {
            var p = (from l in Week
                     where l.DayOfWeek == day
                     select l.Person).SingleOrDefault();

            return (from t in Trainees
                    where t.Id == p.Id
                    select t).SingleOrDefault();
        }

        private void Update()
        {
            try
            {
                UpdateLunch(DayOfWeek.Monday, Monday);
                UpdateLunch(DayOfWeek.Tuesday, Tuesday);
                UpdateLunch(DayOfWeek.Wednesday, Wednesday);
                UpdateLunch(DayOfWeek.Thursday, Thursday);
                UpdateLunch(DayOfWeek.Friday, Friday);

                this.Service.UpdateLunch(Week);
            }
            catch (Exception ex) { ViewService.MessageBox.Error(ex.ToString()); }
        }

        private void UpdateLunch(DayOfWeek day, PersonDto person)
        {
            //var result = Week.Where(e => e.DayOfWeek == day).Select(f => { f.Person = person; return f; });
            var result = (from l in Week
                          where l.DayOfWeek == day
                          select l).SingleOrDefault();
            result.Person = person;
        }

        #endregion Methods
    }
}