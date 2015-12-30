namespace Probel.Geho.Gui.ViewModels.Helpers
{
    using System.Collections.Generic;

    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Geho.Gui.ViewModels.PrintDocument;
    using Probel.Geho.Gui.Views.PrintDocument;

    public class PrintDayViewModelBuilder
    {
        #region Fields

        private const int GROUP_SIZE = 7;

        private readonly DisplayOneDayViewModel ViewModel;

        #endregion Fields

        #region Constructors

        public PrintDayViewModelBuilder(DisplayOneDayViewModel viewmodel)
        {
            this.ViewModel = viewmodel;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<PrintDayView> Build()
        {
            var groupsList = ViewModel.Groups.Chunk(GROUP_SIZE);

            var uc = new List<PrintDayView>();
            foreach (var goups in groupsList)
            {
                var printVm = new PrintDayViewModel(goups, ViewModel.DayOfWeek);
                uc.Add(new PrintDayView(printVm));
            }
            return uc;
        }

        #endregion Methods
    }
}