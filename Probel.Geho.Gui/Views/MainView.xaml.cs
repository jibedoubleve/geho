namespace Probel.Geho.Gui
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    using Microsoft.Practices.Unity;

    using Models;

    using Mvvm.Gui;
    using Mvvm.Toolkit.Events;

    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels;
    using Probel.Geho.Gui.Views;

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
            AppContext.Messenger.Subscribe(this);
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
                var view = new ScheduleDisplayView(vm);
                this.mainFrame.Navigate(view);
                new StatusWriter().Ready();
            }
            catch (Exception ex) { new ErrorHandler().HandleError(ex); }
        }

        private void Click_ScheduleEditor(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = Ioc.Resolve<ScheduleEditorViewModel>();
                var view = new ScheduleEditorView(vm);
                vm.Load();
                this.mainFrame.Navigate(view);
            }
            catch (Exception ex) { new ErrorHandler().HandleError(ex); }
        }

        private void Click_ShowError(object sender, RoutedEventArgs e)
        {
            this.errorPopup.IsOpen = true;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        #endregion Methods
    }
}