namespace Probel.Geho.Gui.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Gui.Models;
    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Mvvm.DataBinding;

    public class GroupHrViewModel : ObservableObject, ILoadeableViewModel
    {
        #region Fields

        private readonly ICommand addActivityCommand;
        private readonly ICommand addGroupCommand;
        private readonly ICommand updateActivityCommand;
        private readonly ICommand updateGroupCommand;

        private ActivityDto activityToAdd = new ActivityDto();
        private GroupDto groupToAdd = new GroupDto();
        private ActivityViewModel selectedActivity;
        private GroupViewModel selectedGroup;
        private IHrService Service;

        #endregion Fields

        #region Constructors

        public GroupHrViewModel(IHrService service)
        {
            this.Service = service;
            this.Groups = new ObservableCollection<GroupViewModel>();
            this.Activities = new ObservableCollection<ActivityViewModel>();
            this.BeneficiariesInGroup = new ObservableCollection<PersonModel>();
            this.BeneficiariesInActivity = new ObservableCollection<PersonModel>();
            this.EducatorsInActivity = new ObservableCollection<PersonModel>();

            this.addGroupCommand = new RelayCommand(AddGroup, CanAddGroup);
            this.updateGroupCommand = new RelayCommand(UpdateGroup, CanUpdateGroup);
            this.addActivityCommand = new RelayCommand(AddActivity, CanAddActivity);
            this.updateActivityCommand = new RelayCommand(UpdateActivity, CanUpdateActivity);
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<ActivityViewModel> Activities
        {
            get;
            private set;
        }

        public ActivityDto ActivityToAdd
        {
            get { return this.activityToAdd; }
            set
            {
                this.activityToAdd = value;
                this.OnPropertyChanged(() => ActivityToAdd);
            }
        }

        public ICommand AddActivityCommand
        {
            get { return this.addActivityCommand; }
        }

        public ICommand AddGroupCommand
        {
            get { return this.addGroupCommand; }
        }

        public ObservableCollection<PersonModel> BeneficiariesInActivity
        {
            get;
            private set;
        }

        public ObservableCollection<PersonModel> BeneficiariesInGroup
        {
            get;
            private set;
        }

        public ObservableCollection<PersonModel> EducatorsInActivity
        {
            get;
            private set;
        }

        public ObservableCollection<GroupViewModel> Groups
        {
            get;
            private set;
        }

        public GroupDto GroupToAdd
        {
            get { return this.groupToAdd; }
            set
            {
                this.groupToAdd = value;
                this.OnPropertyChanged(() => GroupToAdd);
            }
        }

        public ActivityViewModel SelectedActivity
        {
            get { return this.selectedActivity; }
            set
            {
                this.selectedActivity = value;
                this.SelectPersonInActivity();
                this.OnPropertyChanged(() => SelectedActivity);
            }
        }

        public GroupViewModel SelectedGroup
        {
            get { return this.selectedGroup; }
            set
            {
                this.selectedGroup = value;
                this.SelectPesonInGroup();
                this.OnPropertyChanged(() => SelectedGroup);
            }
        }

        public ICommand UpdateActivityCommand
        {
            get { return this.updateActivityCommand; }
        }

        public ICommand UpdateGroupCommand
        {
            get { return this.updateGroupCommand; }
        }

        private IEnumerable<PersonModel> PeopleWithoutGroup
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public void Load()
        {
            var g = this.Service.GetGroups();
            this.Groups.Refill(GroupViewModel.ToViewModels(g, Service, this));

            var a = this.Service.GetActivities();
            this.Activities.Refill(ActivityViewModel.ToActivityViewModel(a, Service, this));

            this.PeopleWithoutGroup = PersonModel.ToModel(this.Service.GetBeneficiariesWithoutGroup());
        }

        private void AddActivity()
        {
            using (WaitingCursor.While)
            {
                this.Service.CreateActivity(this.ActivityToAdd);
                this.Load();
            }
        }

        private void AddGroup()
        {
            this.Service.CreateGroup(GroupToAdd);
            this.GroupToAdd = new GroupDto();
            this.Load();
        }

        private bool CanAddActivity()
        {
            return this.ActivityToAdd != null
                && !string.IsNullOrWhiteSpace(this.ActivityToAdd.Name);
        }

        private bool CanAddGroup()
        {
            return !string.IsNullOrWhiteSpace(this.GroupToAdd.Name);
        }

        private bool CanUpdateActivity()
        {
            return this.SelectedActivity != null;
        }

        private bool CanUpdateGroup()
        {
            return this.SelectedGroup != null
                && !string.IsNullOrWhiteSpace(this.SelectedGroup.Group.Name);
        }

        private void SelectPersonInActivity()
        {
            if (this.SelectedActivity == null
            || this.SelectedActivity.Beneficiaries == null
            || this.SelectedActivity.Educators == null) { return; }

            var fe = this.Service.GetFreeEducators(this.SelectedActivity.DayOfWeek, this.SelectedActivity.IsMorning);
            this.EducatorsInActivity.Refill(PersonModel.ToModel(SelectedActivity.Educators));
            this.EducatorsInActivity.SetSelected();
            this.EducatorsInActivity.AddRange(PersonModel.ToModel(fe));

            var fb = this.Service.GetFreeBeneficiaries(this.SelectedActivity.DayOfWeek, this.SelectedActivity.IsMorning);
            this.BeneficiariesInActivity.Refill(PersonModel.ToModel(SelectedActivity.Beneficiaries));
            this.BeneficiariesInActivity.SetSelected();
            this.BeneficiariesInActivity.AddRange(PersonModel.ToModel(fb));
        }

        private void SelectPesonInGroup()
        {
            if (this.SelectedGroup == null
            || this.SelectedGroup.Group == null
            || this.SelectedGroup.Group.Persons == null) { return; }

            foreach (var b in BeneficiariesInGroup) { b.IsSelected = false; }

            this.BeneficiariesInGroup.Refill(this.PeopleWithoutGroup.Concat(PersonModel.ToModel(SelectedGroup.Group.Persons)));
            foreach (var p in SelectedGroup.Group.Persons)
            {
                var sp = (from b in this.BeneficiariesInGroup
                          where b.Id == p.Id
                          select b).SingleOrDefault();
                sp.IsSelected = true;
            }
        }

        private void UpdateActivity()
        {
            var ed = this.EducatorsInActivity.Where(e => e.IsSelected);
            var be = this.BeneficiariesInActivity.Where(e => e.IsSelected);

            this.SelectedActivity.Activity.People = PersonModel.ToDto(ed.Concat(be));

            using (WaitingCursor.While)
            {
                this.Service.UpdateActivity(this.SelectedActivity.Activity);
                this.EducatorsInActivity.Clear();
                this.BeneficiariesInActivity.Clear();
                this.Load();
            }
        }

        private void UpdateGroup()
        {
            using (WaitingCursor.While)
            {
                var persons = (from p in this.BeneficiariesInGroup
                               where p.IsSelected
                               select p.Person).ToList();
                this.SelectedGroup.Group.Persons = persons;
                this.Service.UpdateGroup(this.SelectedGroup.Group);
                this.Load();
            }
        }

        #endregion Methods
    }
}