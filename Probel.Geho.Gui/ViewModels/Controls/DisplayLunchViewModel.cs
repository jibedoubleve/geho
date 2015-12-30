namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Data;

    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    public class DisplayLunchViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly IScheduleService Service;

        #endregion Fields

        #region Constructors

        public DisplayLunchViewModel(IScheduleService service)
        {
            this.Service = service;
            this.Week = new ObservableCollection<LunchTimeDto>();
            this.WeekView = CollectionViewSource.GetDefaultView(Week);
            this.WeekView.GroupDescriptions.Add(new PropertyGroupDescription("DayOfWeek"));
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<LunchTimeDto> Week
        {
            get;
            private set;
        }

        public ICollectionView WeekView
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public override async void Load()
        {
            var week = await Task.Run(() => this.Service.GetLunchTimes());
            this.Week.Refill(week);
        }

        #endregion Methods
    }
}