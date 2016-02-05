namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Mvvm.Toolkit.DataBinding;

    using Runtime;

    using Services.BusinessLogic;
    using Services.Dto;

    public class EditDayViewModel : BaseViewModel
    {
        #region Fields

        private readonly IScheduleService Service;

        private DateTime currentDay;
        private EditGroupScheduleViewModel editGroupScheduleViewModel;
        private GroupBaseDto selectedGroup;

        #endregion Fields

        #region Constructors

        public EditDayViewModel(IScheduleService service)
        {
            this.Groups = new ObservableCollection<GroupBaseDto>();
            this.EditGroupScheduleViewModel = new EditGroupScheduleViewModel(service);
            this.Service = service;
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

        public EditGroupScheduleViewModel EditGroupScheduleViewModel
        {
            get { return this.editGroupScheduleViewModel; }
            set
            {
                this.editGroupScheduleViewModel = value;
                this.OnPropertyChanged(() => EditGroupScheduleViewModel);
            }
        }

        public ObservableCollection<GroupBaseDto> Groups
        {
            get;
            private set;
        }

        public GroupBaseDto SelectedGroup
        {
            get { return this.selectedGroup; }
            set
            {
                this.selectedGroup = value;
                //Todo: fix this
                if (value != null) { NavigationContext.WeekToManageSelectedGroup = value.Id; }
                this.LoadGroup();
                this.OnPropertyChanged(() => SelectedGroup);
            }
        }

        #endregion Properties

        #region Methods

        public void Load(DateTime dayDate)
        {
            this.CurrentDay = dayDate;
            var groups = this.Service.GetBaseGroups();
            this.Groups.Refill(groups);
            this.SelectGroup();
        }

        private async Task LoadGroup()
        {
            this.EditGroupScheduleViewModel.CurrentDay = this.CurrentDay;
            this.EditGroupScheduleViewModel.Group = this.SelectedGroup;
            await this.EditGroupScheduleViewModel.Load();
        }

        private void SelectGroup()
        {
            var gid = NavigationContext.WeekToManageSelectedGroup;
            if(this.Groups.Count == 0) { return; }
            else
            {
                this.SelectedGroup = (from g in this.Groups
                                      where g.Id == gid
                                      select g).FirstOrDefault();
                if(this.SelectedGroup== null) { this.SelectedGroup = this.Groups.ElementAt(0); }
            }
        }

        #endregion Methods
    }
}