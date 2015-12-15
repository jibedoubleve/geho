namespace Probel.Geho.Gui.Views
{
    using System.Windows;
    using System.Windows.Controls;

    using Mvvm.DataBinding;

    using Probel.Geho.Gui.ViewModels;

    /// <summary>
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : Page
    {
        #region Constructors

        public ScheduleView(ScheduleViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void click_After(object sender, RoutedEventArgs e)
        {
            if (DataContext is ScheduleViewModel)
            {
                var vm = this.GetViewModel<ScheduleViewModel>();
                vm.NextWeek();
            }
        }

        private void click_Before(object sender, RoutedEventArgs e)
        {
            if (DataContext is ScheduleViewModel)
            {
                var vm = this.GetViewModel<ScheduleViewModel>();
                vm.PreviousWeek();
            }
        }

        #endregion Methods
    }
}