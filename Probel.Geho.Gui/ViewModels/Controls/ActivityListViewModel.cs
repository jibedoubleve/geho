﻿namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    using Data.BusinessLogic;
    using Data.Dto;

    using Probel.Mvvm.DataBinding;

    public class ActivityListViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly IScheduleService Service;

        #endregion Fields

        #region Constructors

        public ActivityListViewModel(IScheduleService service)
        {
            this.Service = service;
            this.Activities = new ObservableCollection<ActivityCardViewModel>();
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<ActivityCardViewModel> Activities
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public override async void Load()
        {
            var activities = await Task.Run(() => this.Service.GetActivities());
            this.Activities.Refill(ActivityCardViewModel.ToActivityCardViewModel(activities));
        }

        #endregion Methods
    }
}