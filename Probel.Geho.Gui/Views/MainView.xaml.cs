namespace Probel.Geho.Gui
{
    using System;
    using System.Windows;

    using Microsoft.Practices.Unity;

    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels;
    using Probel.Geho.Gui.Views;
    using Probel.Mvvm.Gui;

    using Runtime;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        #region Fields

        private readonly IUnityContainer Ioc;

        #endregion Fields

        #region Constructors

        public MainView(IUnityContainer ioc)
        {
            Ioc = ioc;
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private async void Click_GroupManagement(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<GroupHrViewModel>();

                using (WaitingCursor.While) { await vm.LoadAsync(); }
                var view = new GroupHrView(vm);
                this.mainFrame.Navigate(view);
            }
            catch (Exception ex) { new ErrorHandler().HandleError(ex); }
        }

        private async void Click_HrManagement(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<HrViewModel>();

                using (WaitingCursor.While) { await vm.LoadAsync(); }
                var view = new HrView(vm);
                this.mainFrame.Navigate(view);
            }
            catch (Exception ex) { new ErrorHandler().HandleError(ex); }
        }

        private void Click_ScheduleDisplay(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<ScheduleDisplayViewModel>();
                var ctx = Ioc.Resolve<IContext>();
                var view = new ScheduleDisplayView(vm, ctx);
                this.mainFrame.Navigate(view);
                new StatusWriter().Ready();
            }
            catch (Exception ex) { new ErrorHandler().HandleError(ex); }
        }

        private void Click_ScheduleManagement(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<ScheduleManagerViewModel>();

                var view = new ScheduleManagerView(vm);
                vm.Load();
                this.mainFrame.Navigate(view);
            }
            catch (Exception ex) { new ErrorHandler().HandleError(ex); }
        }

        #endregion Methods
    }
}