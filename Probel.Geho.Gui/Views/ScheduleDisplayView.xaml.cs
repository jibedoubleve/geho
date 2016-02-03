namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    using Controls;

    using Mvvm.Toolkit.DataBinding;

    using PrintDocument;

    using Properties;

    using Runtime;

    using ViewModels;
    using ViewModels.Controls;
    using ViewModels.Helpers;
    using ViewModels.PrintDocument;

    /// <summary>
    /// Interaction logic for ScheduleDisplayView.xaml
    /// </summary>
    public partial class ScheduleDisplayView : Page
    {
        #region Fields

        private readonly IContext AppContext;

        #endregion Fields

        #region Constructors

        public ScheduleDisplayView(ScheduleDisplayViewModel vm, IContext ctx)
        {
            InitializeComponent();
            this.DataContext = vm;
            this.AppContext = ctx;
            this.SelectTab();
        }

        #endregion Constructors

        #region Methods

        private void Click_Print(object sender, RoutedEventArgs e)
        {
            printPopup.IsOpen = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ILoadeableViewModel)
            {
                var vm = this.GetViewModel<ILoadeableViewModel>();
                vm.Load();
            }
        }

        private void PrintView_Cancelled(object sender, EventArgs e)
        {
            this.printPopup.IsOpen = false;
        }

        private void PrintView_Printing(object sender, EventArgs e)
        {
            printPopup.IsOpen = false;
            if (e is PrintEventArgs)
            {
                var args = (PrintEventArgs)e;
                var list = new List<UserControl>();
                if (args.IsWeekPrinted)
                {
                    var vm = this.DataContext as ScheduleDisplayViewModel;
                    var builder = new PrintWeekViewModelBuilder(this.uc_week.DataContext as DisplayWeekViewModel, vm.SelectedDate);
                    list.AddRange(builder.Build());

                }
                if (args.IsLunchPrinted)
                {
                    var vm = uc_LunchTime.DataContext as DisplayLunchViewModel;
                    list.Add(new PrintLunchesView(vm));
                }
                if (args.IsActivitiesPrinted)
                {
                    var vm = uc_Activities.DataContext as ActivityGridViewModel;
                    list.Add(new PrintActivitiesView(vm));
                }

                if (args.IsMondayPrinted)
                {
                    var builder = new PrintDayViewModelBuilder(this.uc_Monday.DataContext as DisplayOneDayViewModel);
                    list.AddRange(builder.Build());
                }
                if (args.IsTuesdayPrinted)
                {
                    var builder = new PrintDayViewModelBuilder(this.uc_Tuesday.DataContext as DisplayOneDayViewModel);
                    list.AddRange(builder.Build());
                }
                if (args.IsWednesdayPrinted)
                {
                    var builder = new PrintDayViewModelBuilder(this.uc_Wednesday.DataContext as DisplayOneDayViewModel);
                    list.AddRange(builder.Build());
                }
                if (args.IsThursdayPrinted)
                {
                    var builder = new PrintDayViewModelBuilder(this.uc_Thursday.DataContext as DisplayOneDayViewModel);
                    list.AddRange(builder.Build());
                }
                if (args.IsFridayPrinted)
                {
                    var builder = new PrintDayViewModelBuilder(this.uc_Friday.DataContext as DisplayOneDayViewModel);
                    list.AddRange(builder.Build());
                }

                var printer = new PrintDialog();
                if (printer.ShowDialog() == true)
                {
                    foreach (var item in list)
                    {
                        item.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        item.Arrange(new Rect(item.DesiredSize));
                        printer.PrintVisual(item, Messages.Msg_Print);
                    }
                }
            }
            else { throw new NotSupportedException("This event args are not in the supported type"); }
        }

        private void SelectTab()
        {
            if (AppContext.WeekToDisplayTab == "tab_week") { this.tab_week.IsSelected = true; }
            else if (AppContext.WeekToDisplayTab == "tab_monday") { this.tab_monday.IsSelected = true; }
            else if (AppContext.WeekToDisplayTab == "tab_tuesday") { this.tab_tuesday.IsSelected = true; }
            else if (AppContext.WeekToDisplayTab == "tab_wednesday") { this.tab_wednesday.IsSelected = true; }
            else if (AppContext.WeekToDisplayTab == "tab_thursday") { this.tab_thursday.IsSelected = true; }
            else if (AppContext.WeekToDisplayTab == "tab_friday") { this.tab_friday.IsSelected = true; }
            else if (AppContext.WeekToDisplayTab == "tab_activities") { this.tab_activities.IsSelected = true; }
            else if (AppContext.WeekToDisplayTab == "tab_lunches") { this.tab_lunches.IsSelected = true; }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tab_week.IsSelected) { AppContext.WeekToDisplayTab = "tab_week"; }
            else if (tab_monday.IsSelected) { AppContext.WeekToDisplayTab = "tab_monday"; }
            else if (tab_tuesday.IsSelected) { AppContext.WeekToDisplayTab = "tab_tuesday"; }
            else if (tab_wednesday.IsSelected) { AppContext.WeekToDisplayTab = "tab_wednesday"; }
            else if (tab_thursday.IsSelected) { AppContext.WeekToDisplayTab = "tab_thursday"; }
            else if (tab_friday.IsSelected) { AppContext.WeekToDisplayTab = "tab_friday"; }
            else if (tab_activities.IsSelected) { AppContext.WeekToDisplayTab = "tab_activities"; }
            else if (tab_lunches.IsSelected) { AppContext.WeekToDisplayTab = "tab_lunches"; }
        }

        #endregion Methods
    }
}