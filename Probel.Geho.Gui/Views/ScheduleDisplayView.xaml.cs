namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using Controls;

    using Mvvm.DataBinding;

    using Properties;

    using ViewModels;

    /// <summary>
    /// Interaction logic for ScheduleDisplayView.xaml
    /// </summary>
    public partial class ScheduleDisplayView : Page
    {
        #region Constructors

        public ScheduleDisplayView(ScheduleDisplayViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
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

        private void SelectionChanged_cb_Dates(object sender, SelectionChangedEventArgs e)
        {
            if (this.cb_Dates.SelectedItem != null
            && this.cb_Dates.SelectedItem is DateTime
            && this.DataContext is ScheduleDisplayViewModel)
            {
                var vm = this.GetViewModel<ScheduleDisplayViewModel>();
                var dt = (DateTime)this.cb_Dates.SelectedItem;
                vm.LoadWeek(dt);
            }
        }

        #endregion Methods
    }
}