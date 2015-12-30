namespace Probel.Geho.Gui.ViewModels.PrintDocument
{
    using Probel.Geho.Gui.ViewModels.Controls;

    public class PrintLunchesViewModel : BaseViewModel
    {
        #region Fields

        private DisplayLunchViewModel displayLunchViewModel;

        #endregion Fields

        #region Constructors

        public PrintLunchesViewModel(DisplayLunchViewModel viewmodel)
        {
            this.DisplayLunchViewModel = viewmodel;
        }

        #endregion Constructors

        #region Properties

        public DisplayLunchViewModel DisplayLunchViewModel
        {
            get { return this.displayLunchViewModel; }
            set
            {
                this.displayLunchViewModel = value;
                this.OnPropertyChanged(() => DisplayLunchViewModel);
            }
        }

        #endregion Properties
    }
}