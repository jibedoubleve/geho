namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.ObjectModel;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

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
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<LunchTimeDto> Week
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public override void Load()
        {
            var week = this.Service.GetLunchTimes();
            this.Week.Refill(week);
        }

        #endregion Methods
    }
}