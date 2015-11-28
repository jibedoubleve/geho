namespace Probel.Geho.Gui
{
    using System;
    using System.Windows;

    using Microsoft.Practices.Unity;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Data.BusinessLogic.Schedule;
    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels;
    using Probel.Geho.Gui.Views;
    using Probel.Mvvm.Gui;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private readonly IUnityContainer Ioc;

        #endregion Fields

        #region Constructors

        public MainWindow(IUnityContainer ioc)
        {
            Ioc = ioc;
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void Click_GroupManagement(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<GroupHrViewModel>();

                using (WaitingCursor.While) { vm.Load(); }
                var view = new GroupHrView(vm);
                this.mainFrame.Navigate(view);
            }
            catch (Exception ex)
            {
                ViewService.MessageBox.Error(ex.ToString());
            }
        }

        private void Click_HrManagement(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<HrViewModel>();

                using (WaitingCursor.While) { vm.Load(); }
                var view = new HrView(vm);
                this.mainFrame.Navigate(view);
            }
            catch (Exception ex)
            {
                ViewService.MessageBox.Error(ex.ToString());
            }
        }

        private void Click_ScheduleManagement(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<ScheduleViewModel>();

                var view = new ScheduleView(vm);
                this.mainFrame.Navigate(view);
            }
            catch (Exception ex)
            {
                ViewService.MessageBox.Error(ex.ToString());
            }
        }

        #endregion Methods
    }
}