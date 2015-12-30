namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Data;

    using Models;

    using Mvvm.Toolkit.DataBinding;

    using Services.BusinessLogic;

    public class ActivityGridViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly IScheduleService Service;

        #endregion Fields

        #region Constructors

        public ActivityGridViewModel(IScheduleService service)
        {
            this.Service = service;
            this.Activities = new ObservableCollection<ActivityModel>();
            this.ActivitiesView = CollectionViewSource.GetDefaultView(Activities);
            this.ActivitiesView.GroupDescriptions.Add(new PropertyGroupDescription("DayOfWeek"));
        }

        #endregion Constructors

        #region Properties

        public ICollectionView ActivitiesView
        {
            get; private set;
        }

        private ObservableCollection<ActivityModel> Activities
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public override void Load()
        {
            var activities = Service.GetActivities().ToModels();
            this.Activities.Refill(activities);
        }

        #endregion Methods
    }
}