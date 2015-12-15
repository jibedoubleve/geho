namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Data.BusinessLogic;

    using Microsoft.Practices.ObjectBuilder2;

    using Models;

    using Mvvm.Gui;

    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

    public class LunchManagementViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly IHrService Service;
        private readonly ICommand updateCommand;

        private IList<LunchTimeDto> Week;

        #endregion Fields

        #region Constructors

        public LunchManagementViewModel(IHrService service)
        {
            this.Service = service;
            this.Monday = new ObservableCollection<PersonModel>();
            this.Tuesday = new ObservableCollection<PersonModel>();
            this.Wednesday = new ObservableCollection<PersonModel>();
            this.Thursday = new ObservableCollection<PersonModel>();
            this.Friday = new ObservableCollection<PersonModel>();
            this.updateCommand = new RelayCommand(Update, CanUpdate);
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<PersonModel> Friday
        {
            get;
            private set;
        }

        public ObservableCollection<PersonModel> Monday
        {
            get;
            private set;
        }

        public ObservableCollection<PersonModel> Thursday
        {
            get;
            private set;
        }

        public ObservableCollection<PersonModel> Tuesday
        {
            get;
            private set;
        }

        public ICommand UpdateCommand
        {
            get { return this.updateCommand; }
        }

        public ObservableCollection<PersonModel> Wednesday
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public override void Load()
        {
            var trainees = this.Service.GetTrainees();
            Week = this.Service.GetLunchTimes().ToList();

            this.Monday.Refill(trainees.ToModel());
            this.Tuesday.Refill(trainees.ToModel());
            this.Wednesday.Refill(trainees.ToModel());
            this.Thursday.Refill(trainees.ToModel());
            this.Friday.Refill(trainees.ToModel());

            foreach (var day in Week)
            {
                switch (day.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        SetSelected(day.People.ToModel(), this.Monday);
                        break;
                    case DayOfWeek.Tuesday:
                        SetSelected(day.People.ToModel(), this.Tuesday);
                        break;
                    case DayOfWeek.Wednesday:
                        SetSelected(day.People.ToModel(), this.Wednesday);
                        break;
                    case DayOfWeek.Thursday:
                        SetSelected(day.People.ToModel(), this.Thursday);
                        break;
                    case DayOfWeek.Friday:
                        SetSelected(day.People.ToModel(), this.Friday);
                        break;
                    default: throw new NotSupportedException("Add lunch in the weekend is not supported.");
                }
            }
        }

        private bool CanUpdate()
        {
            return this.Monday != null
                && this.Tuesday != null
                && this.Wednesday != null
                && this.Thursday != null
                && this.Friday != null;
        }

        private void SetSelected(IEnumerable<PersonModel> people, IEnumerable<PersonModel> @in)
        {
            foreach (var item in @in)
            {
                foreach (var p in people)
                {
                    if (item.Id == p.Id) { item.IsSelected = true; }
                }
            }
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

        private void UpdateLunch(DayOfWeek day, IEnumerable<PersonModel> people)
        {
            var result = (from l in Week
                          where l.DayOfWeek == day
                          select l).SingleOrDefault();

            var toAdd = (from p in people
                         where p.IsSelected
                         select p).ToDto();

            if (result != null) { result.People = toAdd; }
            else
            {
                Week.Add(new LunchTimeDto()
                {
                    DayOfWeek = day,
                    People = toAdd,
                });
            }
        }

        #endregion Methods
    }
}