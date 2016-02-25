namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Mvvm.Gui;
    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Services.BusinessLogic;

    using Properties;

    using Services.Dto;

    using Tools;

    public class EditGroupScheduleViewModel : BaseViewModel
    {
        #region Fields

        private readonly IScheduleService Service;

        private DateTime currentDay;
        private GroupBaseDto group;

        #endregion Fields

        #region Constructors

        public EditGroupScheduleViewModel(IScheduleService service)
        {
            this.SaveCommand = new RelayCommand(Save, CanSave);
            this.EducatorsAfternoon = new ObservableCollection<EditPersonScheduleViewModel>();
            this.EducatorsMorning = new ObservableCollection<EditPersonScheduleViewModel>();
            this.BeneficiariesAfternoon = new ObservableCollection<PersonDto>();
            this.BeneficiariesMorning = new ObservableCollection<PersonDto>();
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<PersonDto> BeneficiariesAfternoon
        {
            get;
            private set;
        }

        public ObservableCollection<PersonDto> BeneficiariesMorning
        {
            get;
            private set;
        }

        public DateTime CurrentDay
        {
            get { return this.currentDay; }
            set
            {
                this.currentDay = value;
                this.OnPropertyChanged(() => CurrentDay);
            }
        }

        public ObservableCollection<EditPersonScheduleViewModel> EducatorsAfternoon
        {
            get;
            private set;
        }

        public ObservableCollection<EditPersonScheduleViewModel> EducatorsMorning
        {
            get;
            private set;
        }

        public GroupBaseDto Group
        {
            get { return this.group; }
            set
            {
                this.group = value;
                this.OnPropertyChanged(() => Group);
            }
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public async Task Load()
        {
            Status.Loading();

            if (Group == null) { this.Status.Warn(Messages.Msg_NoGroupSelected); return; }
            if (CurrentDay == null) { this.Status.Warn(Messages.Msg_NoDateSelected); return; }

            using (WaitingCursor.While)
            {
                var freeInMorning = await Task.Run(() => this.Service.GetFreeEducators(this.CurrentDay, isMorning: true));
                var freeInAfternoon = await Task.Run(() => this.Service.GetFreeEducators(this.CurrentDay, isMorning: false));
                var freeBenefInMorning = await Task.Run(() => this.Service.GetFreeBeneficiaries(Group, CurrentDay, isMorning: true));
                var freeBenefInAfternoon = await Task.Run(() => this.Service.GetFreeBeneficiaries(Group, CurrentDay, isMorning: false));

                this.EducatorsMorning.Refill(freeInMorning.ToViewModels());
                this.EducatorsAfternoon.Refill(freeInAfternoon.ToViewModels());

                this.BeneficiariesMorning.Refill(freeBenefInMorning);
                this.BeneficiariesAfternoon.Refill(freeBenefInAfternoon);

                await Task.Run(() => this.MarkEducatorSelectedInThisGroup());
            }
            this.Status.Ready();
        }

        private bool CanSave()
        {
            return Group != null;
        }

        //TODO: send it to the service layer
        private void MarkEducatorSelectedInThisGroup()
        {
            var busyMorning = this.Service.GetEducatorsBusyInDay(this.CurrentDay, isMorning: true);
            var busyAfternoon = this.Service.GetEducatorsBusyInDay(this.CurrentDay, isMorning: false);

            foreach (var educator in busyMorning)
            {
                this.EducatorsMorning.Where(e => e.Person.Id == educator.Id)
                                     .ToList()
                                     .ForEach(e =>
                                     {
                                         e.ColourStatus = ColourStatus.Red;
                                         e.GroupNames = string.Format("({0})", educator.GroupNames);
                                     });
                this.EducatorsMorning.Where(e => e.Person.Id == educator.Id
                                                && !string.IsNullOrEmpty(e.Person.GroupNames)
                                                && e.Person.GroupNames.Contains(this.Group.Name))
                                     .ToList()
                                     .ForEach(e =>
                                     {
                                         e.IsSelected = true;
                                         e.ColourStatus = ColourStatus.Green;
                                     });
            }
            foreach (var educator in busyAfternoon)
            {
                this.EducatorsAfternoon.Where(e => e.Person.Id == educator.Id)
                                       .ToList()
                                       .ForEach(e =>
                                       {
                                           e.ColourStatus = ColourStatus.Red;
                                           e.GroupNames = string.Format("({0})", educator.GroupNames);
                                       });
                this.EducatorsAfternoon.Where(e => e.Person.Id == educator.Id
                                                && !string.IsNullOrEmpty(e.Person.GroupNames)
                                                && e.Person.GroupNames.Contains(this.Group.Name))
                                     .ToList()
                                     .ForEach(e =>
                                     {
                                         e.IsSelected = true;
                                         e.ColourStatus = ColourStatus.Green;
                                     });
            }
        }

        private async void Save()
        {
            var morningEducs = (from e in this.EducatorsMorning
                                where e.IsSelected
                                select e.Person).ToList();
            await Task.Run(() => this.Service.FeedDay(CurrentDay, morningEducs, Group, isMorning: true));

            var afternoonEducs = (from e in this.EducatorsAfternoon
                                  where e.IsSelected
                                  select e.Person).ToList();
            await Task.Run(() => this.Service.FeedDay(CurrentDay, afternoonEducs, Group, isMorning: false));

            await this.Load();

            this.Status.Info(Messages.Msg_SavedSchedule);
        }

        #endregion Methods
    }
}