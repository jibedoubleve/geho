namespace Probel.Geho.Gui.ViewModels.PrintDocument
{
    using Probel.Geho.Gui.ViewModels.Controls;

    public class PrintActivitiesViewModel : BaseViewModel
    {
        #region Fields

        private ActivityGridViewModel activityGridViewModel;

        #endregion Fields

        #region Constructors

        public PrintActivitiesViewModel(ActivityGridViewModel viewmodel)
        {
            this.ActivityGridViewModel = viewmodel;
        }

        #endregion Constructors

        #region Properties

        public ActivityGridViewModel ActivityGridViewModel
        {
            get { return this.activityGridViewModel; }
            set
            {
                this.activityGridViewModel = value;
                this.OnPropertyChanged(() => ActivityGridViewModel);
            }
        }

        #endregion Properties
    }
}