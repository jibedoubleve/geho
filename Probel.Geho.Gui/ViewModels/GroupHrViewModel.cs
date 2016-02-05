namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Input;

    using Converters;

    using Mvvm.Gui;
    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Gui.Models;
    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    using Properties;

    using Services.Entities;

    public class GroupHrViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly ICommand addActivityCommand;
        private readonly ICommand addGroupCommand;
        private readonly ICommand updateActivityCommand;
        private readonly ICommand updateGroupCommand;

        private ListCollectionView activities;
        private ActivityDto activityToAdd = new ActivityDto();
        private ListCollectionView administrativeActivities;
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
            this.Activities = new ListCollectionView(new List<ActivityViewModel>());
            this.AdministrativeActivities = new ListCollectionView(new List<ActivityViewModel>());
            this.BeneficiariesInGroup = new ObservableCollection<PersonModel>();
            this.BeneficiariesInActivity = new ObservableCollection<PersonModel>();
            this.EducatorsInActivity = new ObservableCollection<PersonModel>();

            this.addGroupCommand = new RelayCommand(AddGroup, CanAddGroup);
            this.updateGroupCommand = new RelayCommand(UpdateGroup, CanUpdateGroup);
            this.addActivityCommand = new RelayCommand(AddActivity, CanAddActivity);
            this.updateActivityCommand = new RelayCommand(UpdateActivity, CanUpdateActivity);
            this.ShowUpdateGroupCommand = new RelayCommand(ShowUpdateGroup, CanShowUpdateGroup);
            this.ShowUpdateActivityCommand = new RelayCommand(ShowUpdateActivity, CanShowUpdateActivity);
        }

        #endregion Constructors

        #region Properties

        public ListCollectionView Activities
        {
            get { return this.activities; }
            set
            {
                this.activities = value;
                this.OnPropertyChanged(() => Activities);
            }
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

        public ListCollectionView AdministrativeActivities
        {
            get { return this.administrativeActivities; }
            set
            {
                this.administrativeActivities = value;
                this.OnPropertyChanged(() => AdministrativeActivities);
            }
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

        public ICommand ShowUpdateActivityCommand
        {
            get;
            private set;
        }

        public ICommand ShowUpdateGroupCommand
        {
            get;
            private set;
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

        public override void Load()
        {
            this.Status.Loading();
            var g = this.Service.GetGroups();
            this.Groups.Refill(g.ToViewModels(Service, this));

            var a = this.Service.GetActivities();
            this.Activities = new ListCollectionView((a.ToViewModels(Service, this).ToList()));
            this.Activities.GroupDescriptions.Add(new PropertyGroupDescription("DayOfWeek"));
            this.Activities.GroupDescriptions.Add(new PropertyGroupDescription("MomentDay"));

            var aa = this.Service.GetAdministrativeActivities();
            this.AdministrativeActivities = new ListCollectionView((aa.ToViewModels(Service, this).ToList()));
            this.AdministrativeActivities.GroupDescriptions.Add(new PropertyGroupDescription("DayOfWeek"));
            this.AdministrativeActivities.GroupDescriptions.Add(new PropertyGroupDescription("MomentDay"));

            this.PeopleWithoutGroup = PersonModel.ToModel(this.Service.GetBeneficiariesWithoutGroup());

            this.SelectedActivity = null;
            this.SelectedGroup = null;
            this.BeneficiariesInActivity.Clear();
            this.EducatorsInActivity.Clear();
            this.BeneficiariesInGroup.Clear();
            this.Status.Ready();
        }

        private void AddActivity()
        {
            using (WaitingCursor.While)
            {
                this.Service.CreateActivity(this.ActivityToAdd);
                var name = this.ActivityToAdd.Name;
                this.ActivityToAdd = new ActivityDto()
                {
                    DayOfWeek = ActivityToAdd.DayOfWeek,
                    MomentDay = ActivityToAdd.MomentDay
                };
                this.Load();
                this.Status.InfoFormat(Messages.Msg_ActivityCreated, name);
            }
        }

        private void AddGroup()
        {
            this.Service.CreateGroup(GroupToAdd);
            var name = GroupToAdd.Name;
            this.GroupToAdd = new GroupDto();
            this.Load();
            this.Status.InfoFormat(Messages.Msg_GroupCreated, name);
        }

        private bool CanAddActivity()
        {
            return this.ActivityToAdd != null
                && !string.IsNullOrWhiteSpace(this.ActivityToAdd.Name)
                && this.ActivityToAdd.DayOfWeek != DayOfWeek.Sunday
                && this.ActivityToAdd.DayOfWeek != DayOfWeek.Saturday;
        }

        private bool CanAddGroup()
        {
            return this.GroupToAdd != null
               && !string.IsNullOrWhiteSpace(this.GroupToAdd.Name);
        }

        private bool CanShowUpdateActivity()
        {
            return this.SelectedActivity != null; ;
        }

        private bool CanShowUpdateGroup()
        {
            return this.SelectedGroup != null; ;
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
            

            var fe = this.Service.GetEducatorWithoutActivities(this.SelectedActivity.DayOfWeek, (this.SelectedActivity.MomentDay & MomentDay.Morning) != 0);
            this.EducatorsInActivity.Refill(PersonModel.ToModel(SelectedActivity.Educators));
            this.EducatorsInActivity.SetSelected();
            this.EducatorsInActivity.AddRange(PersonModel.ToModel(fe));

            var fb = this.Service.GetBeneficiariesWithoutActivities(this.SelectedActivity.DayOfWeek, (this.SelectedActivity.MomentDay & MomentDay.Morning) != 0);
            this.BeneficiariesInActivity.Refill(PersonModel.ToModel(SelectedActivity.Beneficiaries));
            this.BeneficiariesInActivity.SetSelected();
            this.BeneficiariesInActivity.AddRange(PersonModel.ToModel(fb));
        }

        private void SelectPesonInGroup()
        {
            if (this.SelectedGroup == null
            || this.SelectedGroup.Group == null
            || this.SelectedGroup.Group.People == null) { return; }

            foreach (var b in BeneficiariesInGroup) { b.IsSelected = false; }

            this.BeneficiariesInGroup.Refill(this.PeopleWithoutGroup.Concat(PersonModel.ToModel(SelectedGroup.Group.People)));
            foreach (var p in SelectedGroup.Group.People)
            {
                var sp = (from b in this.BeneficiariesInGroup
                          where b.Id == p.Id
                          select b).SingleOrDefault();
                sp.IsSelected = true;
            }
        }

        private void ShowUpdateActivity()
        {
            /* Nothing to do */
        }

        private void ShowUpdateGroup()
        {
            /* Nothing to do */
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
                this.Status.Info(Messages.Msg_UpdateDone);
            }
        }

        private void UpdateGroup()
        {
            using (WaitingCursor.While)
            {
                var people = (from p in this.BeneficiariesInGroup
                              where p.IsSelected
                              select p.Person).ToList();
                this.SelectedGroup.Group.People = people;
                this.Service.UpdateGroup(this.SelectedGroup.Group);
                this.Load();
                this.Status.Info(Messages.Msg_UpdateDone);
            }
        }

        #endregion Methods
    }
}