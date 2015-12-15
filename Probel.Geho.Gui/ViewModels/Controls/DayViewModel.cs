namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

    public class DayViewModel : LoadeableViewModel
    {
        #region Fields

        public readonly ScheduleViewModel ParentVm;

        private readonly IEnumerable<GroupDto> GroupsDto;
        private readonly IScheduleService Service;

        private DateTime currentDay;
        private int numberOfEducToShow;

        #endregion Fields

        #region Constructors

        public DayViewModel(DateTime d, IEnumerable<GroupDto> groups, IScheduleService service, ScheduleViewModel parentVm)
        {
            if (parentVm == null) { throw new ArgumentNullException("parentVm"); }
            if (service == null) { throw new ArgumentNullException("service"); }

            this.ParentVm = parentVm;
            this.CurrentDay = d;
            this.Service = service;
            this.GroupsDto = groups;
            this.Groups = new ObservableCollection<GroupDayViewModel>();
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

        public ObservableCollection<GroupDayViewModel> Groups
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

        #endregion Properties

        #region Methods

        public override void Load()
        {
            var grps = GroupDayViewModel.ToViewModel(GroupsDto, Service, this);

            foreach (var g in grps) { g.Load(); }

            this.Groups = new ObservableCollection<GroupDayViewModel>(grps);
        }

        public void MarkBusyEducators()
        {
            foreach (var item in Groups) { item.MarkEducatorsBusyInOtherGroups(); }
        }

        #endregion Methods
    }
}