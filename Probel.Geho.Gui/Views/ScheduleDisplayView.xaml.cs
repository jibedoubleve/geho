namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    using Controls;

    using Mvvm.DataBinding;

    using Properties;

    using Runtime;

    using ViewModels;

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
                var list = new List<UIElement>();
                if (args.IsWeekPrinted) { list.Add(this.uc_week); }
                if (args.IsMondayPrinted) { list.Add(this.uc_Monday); }
                if (args.IsTuesdayPrinted) { list.Add(this.uc_Tuesday); }
                if (args.IsWednesdayPrinted) { list.Add(this.uc_Wednesday); }
                if (args.IsThursdayPrinted) { list.Add(this.uc_Thursday); }
                if (args.IsFridayPrinted) { list.Add(this.uc_Friday); }
                if (args.IsLunchPrinted) { list.Add(this.uc_LunchTime); }
                if (args.IsActivitiesPrinted) { list.Add(this.uc_Activities); }

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