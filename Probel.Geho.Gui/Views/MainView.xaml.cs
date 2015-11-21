namespace Probel.Geho.Gui
{
    using System;
    using System.Windows;

    using Microsoft.Practices.Unity;

    using Probel.Geho.Data.BusinessLogic.Hr;
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

        private readonly IUnityContainer Ioc = new UnityContainer();

        #endregion Fields

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void Click_GroupManagement(object sender, RoutedEventArgs e)
        {
            try
            {
                var model = Ioc.Resolve<HrService>();
                var vm = new GroupHrViewModel(model);

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
                var model = Ioc.Resolve<HrService>();
                var vm = new HrViewModel(model);

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
        }

        #endregion Methods
    }
}