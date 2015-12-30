namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    public class ManageDayViewModel : LoadeableViewModel
    {
        #region Fields

        public readonly ScheduleManagerViewModel ParentVm;

        private readonly IEnumerable<GroupDto> GroupsDto;
        private readonly IScheduleService Service;

        private DateTime currentDay;
        private int numberOfEducToShow;
        private ManageGroupDayViewModel selectedGroup;

        #endregion Fields

        #region Constructors

        public ManageDayViewModel(DateTime d, IEnumerable<GroupDto> groups, IScheduleService service, ScheduleManagerViewModel parentVm)
        {
            if (parentVm == null) { throw new ArgumentNullException("parentVm"); }
            if (service == null) { throw new ArgumentNullException("service"); }

            this.ParentVm = parentVm;
            this.CurrentDay = d;
            this.Service = service;
            this.GroupsDto = groups;
            this.Groups = new ObservableCollection<ManageGroupDayViewModel>();
        }

        #endregion Constructors

        #region Properties

        public DateTime CurrentDay
        {
            get { return this.currentDay; }
            set
            {
                this.currentDay = value;
                this.OnPropertyChanged(() => CurrentDay);
            }
        }

        public ObservableCollection<ManageGroupDayViewModel> Groups
        {
            get;
            private set;
        }

        public int NumberOfEducToShow
        {
            get { return this.numberOfEducToShow; }
            set
            {
                this.numberOfEducToShow = value;
                this.OnPropertyChanged(() => NumberOfEducToShow);
            }
        }

        public ManageGroupDayViewModel SelectedGroup
        {
            get { return this.selectedGroup; }
            set
            {
                this.selectedGroup = value;
                this.ParentVm.AppContext.WeekToManageSelectedGroup = value.Group.Id;
                this.OnPropertyChanged(() => SelectedGroup);
            }
        }

        #endregion Properties

        #region Methods

        public override void Load()
        {
            var groups = GroupsDto.ToViewModels(Service, this);

            foreach (var group in groups) { group.Load(); }

            this.Groups = new ObservableCollection<ManageGroupDayViewModel>(groups);
            this.SelectGroup();
        }

        public void MarkBusyEducators()
        {
            foreach (var item in Groups) { item.MarkEducatorsBusyInOtherGroups(); }
        }

        private void SelectGroup()
        {
            if (this.Groups.Count == 0) { return; }
            var sg = (from g in this.Groups
                      where g.Group.Id == ParentVm.AppContext.WeekToManageSelectedGroup
                      select g).FirstOrDefault();

            if (sg != null) { this.SelectedGroup = sg; }
        }

        #endregion Methods
    }
}