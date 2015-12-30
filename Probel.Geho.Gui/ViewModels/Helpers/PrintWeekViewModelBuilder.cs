namespace Probel.Geho.Gui.ViewModels.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Geho.Gui.ViewModels.Helpers;

    using Views.Controls;
    using Views.PrintDocument;

    public class PrintWeekViewModelBuilder
    {
        #region Fields

        private const int GROUP_SIZE = 7;

        private readonly DisplayWeekViewModel Week;

        #endregion Fields

        #region Constructors

        public PrintWeekViewModelBuilder(DisplayWeekViewModel week)
        {
            this.Week = week;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<PrintWeekView> Build()
        {
            var weekList = new List<List<DisplayDayViewModel>>();
            foreach (var day in Week.Days)
            {
                var groupLists = day.Groups.Chunk(GROUP_SIZE).ToList();

                for (int i = 0; i < groupLists.Count(); i++)
                {
                    if (weekList.Count < i + 1) { weekList.Add(new List<DisplayDayViewModel>()); }

                    weekList[i].Add(new DisplayDayViewModel(day.DayOfWeek, groupLists[i]));
                }

            }
            var viewmodels = BuildViewModels(weekList);
            return BuildViews(viewmodels);
        }

        private IList<DisplayWeekViewModel> BuildViewModels(List<List<DisplayDayViewModel>> weekList)
        {
            List<DisplayWeekViewModel> vm = new List<DisplayWeekViewModel>();
            foreach (var week in weekList)
            {
                vm.Add(new DisplayWeekViewModel(week));
            }
            return vm.ToList();
        }

        private IEnumerable<PrintWeekView> BuildViews(IList<DisplayWeekViewModel> viewmodels)
        {
            var views = new List<PrintWeekView>();
            foreach (var vm in viewmodels)
            {
                views.Add(new PrintWeekView(vm));
            }
            return views;
        }

        #endregion Methods
    }
}