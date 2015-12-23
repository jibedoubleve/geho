namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Mvvm.Gui;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Gui.Tools;
    using Probel.Mvvm.DataBinding;

    public class ManageGroupDayViewModel : LoadeableViewModel
    {
        #region Fields

        public readonly GroupDto Group;
        public readonly ManageDayViewModel ParentVm;

        private readonly IScheduleService Service;

        private string errorMessageAfternoon;
        private string errorMessageMorning;
        private bool hasAfternoonError;
        private bool hasMorningError;

        #endregion Fields

        #region Constructors

        public ManageGroupDayViewModel(GroupDto group, IScheduleService service, ManageDayViewModel parentVm)
        {
            this.Service = service;
            this.ParentVm = parentVm;
            this.Group = group;
            this.EducatorsMorning = new ObservableCollection<PersonFlatBusyViewModel>();
            this.EducatorsAfternoon = new ObservableCollection<PersonFlatBusyViewModel>();

            this.BusyEducators = new ObservableCollection<PersonDto>();
            this.BeneficiariesMorning = new ObservableCollection<PersonDto>();
            this.BenificiariesAfternoon = new ObservableCollection<PersonDto>();
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<PersonDto> BeneficiariesMorning
        {
            get;
            private set;
        }

        public ObservableCollection<PersonDto> BenificiariesAfternoon
        {
            get;
            private set;
        }

        public ObservableCollection<PersonDto> BusyEducators
        {
            get;
            private set;
        }

        public ObservableCollection<PersonFlatBusyViewModel> EducatorsAfternoon
        {
            get;
            private set;
        }

        public ObservableCollection<PersonFlatBusyViewModel> EducatorsMorning
        {
            get;
            private set;
        }

        public string ErrorMessageAfternoon
        {
            get { return this.errorMessageAfternoon; }
            set
            {
                this.errorMessageAfternoon = value;
                this.OnPropertyChanged(() => ErrorMessageAfternoon);
            }
        }

        public string ErrorMessageMorning
        {
            get { return this.errorMessageMorning; }
            set
            {
                this.errorMessageMorning = value;
                this.OnPropertyChanged(() => ErrorMessageMorning);
            }
        }

        public string GroupName
        {
            get { return this.Group.Name; }
            set
            {
                this.Group.Name = value;
                this.OnPropertyChanged(() => GroupName);
            }
        }

        public bool HasAfternoonError
        {
            get { return this.hasAfternoonError; }
            set
            {
                this.hasAfternoonError = value;
                this.OnPropertyChanged(() => HasAfternoonError);
            }
        }

        public bool HasMorningError
        {
            get { return this.hasMorningError; }
            set
            {
                this.hasMorningError = value;
                this.OnPropertyChanged(() => HasMorningError);
            }
        }

        #endregion Properties

        #region Methods

        public static IEnumerable<ManageGroupDayViewModel> ToViewModel(IEnumerable<GroupDto> groups, IScheduleService srv, ManageDayViewModel parentVm)
        {
            if (srv == null) { throw new ArgumentNullException("srv"); }
            var list = new List<ManageGroupDayViewModel>();
            foreach (var item in groups)
            {
                var vm = new ManageGroupDayViewModel(item, srv, parentVm);
                //vm.Load();
                list.Add(vm);
            }
            return list;
        }

        public override void Load()
        {
            using (WaitingCursor.While)
            {
                var freeInMorning = this.Service.GetFreeEducators(this.ParentVm.CurrentDay, isMorning: true);
                var freeInAfternoon = this.Service.GetFreeEducators(this.ParentVm.CurrentDay, isMorning: false);

                this.EducatorsMorning.Refill(PersonFlatBusyViewModel.ToViewModels(freeInMorning, Service, this));
                this.EducatorsAfternoon.Refill(PersonFlatBusyViewModel.ToViewModels(freeInAfternoon, Service, this));

                //TODO: send it to the service layer
                var days = (from d in ParentVm.ParentVm.CurrentWeek.Days
                            where d.Date == ParentVm.CurrentDay
                               && d.Group.Id == this.Group.Id
                            select d).ToList();

                this.BeneficiariesMorning.Refill(this.Service.GetFreeBeneficiaries(this.Group, this.ParentVm.CurrentDay, isMorning: true));
                this.BenificiariesAfternoon.Refill(this.Service.GetFreeBeneficiaries(this.Group, this.ParentVm.CurrentDay, isMorning: false));

                MarkEducatorSelectedInThisGroup(days);
                MarkEducatorsBusyInOtherGroups();
            }
        }

        //TODO: send it to the Service Layer
        public void MarkEducatorsBusyInOtherGroups()
        {
            var morningbusy = Service.GetEducatorsBusyInDay(this.ParentVm.CurrentDay, isMorning: true);

            foreach (var b in EducatorsMorning) { b.IsBusy = false; b.GroupNames = string.Empty; }
            foreach (var b in morningbusy)
            {
                this.EducatorsMorning.Where(e => e.Person.Id == b.Id)
                                     .ToList()
                                     .ForEach(e =>
                                     {
                                         e.IsBusy = true;
                                         e.GroupNames = b.GroupNames;
                                     });
            }

            var afternoonbusy = Service.GetEducatorsBusyInDay(this.ParentVm.CurrentDay, isMorning: false);

            foreach (var b in EducatorsAfternoon) { b.IsBusy = false; b.GroupNames = string.Empty; }
            foreach (var b in afternoonbusy)
            {
                this.EducatorsAfternoon.Where(e => e.Person.Id == b.Id)
                                       .ToList()
                                       .ForEach(e =>
                                       {
                                           e.IsBusy = true;
                                           e.GroupNames = b.GroupNames;
                                       });
            }
        }

        public void Save()
        {
            using (WaitingCursor.While)
            {
                var morningEducs = (from e in this.EducatorsMorning
                                    where e.IsSelected
                                    select e.Person).ToList();

                this.Service.FeedDay(this.ParentVm.CurrentDay, morningEducs, Group, true);

                var afternoonEducs = (from e in this.EducatorsAfternoon
                                      where e.IsSelected
                                      select e.Person).ToList();

                this.Service.FeedDay(this.ParentVm.CurrentDay, afternoonEducs, Group, false);
            }
        }

        //TODO: send it to the service layer
        private void MarkEducatorSelectedInThisGroup(List<DayDto> days)
        {
            if (days.Count() > 0)
            {
                var mornings = days.Where(e => e.IsMorning).FirstOrDefault();
                if (mornings != null)
                {
                    foreach (var p in mornings.People)
                    {
                        var r = this.EducatorsMorning.Where(e => e.Person.Id == p.Id)
                                                 .ToList();
                        r.ForEach(e => e.SetSelected());
                    }
                }

                var afternoons = days.Where(e => !e.IsMorning).FirstOrDefault();
                if (afternoons != null)
                {
                    foreach (var p in afternoons.People)
                    {
                        this.EducatorsAfternoon.Where(e => e.Person.Id == p.Id)
                                               .ToList()
                                               .ForEach(e => e.SetSelected());
                    }
                }
            }
        }

        #endregion Methods
    }
}