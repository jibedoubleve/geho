namespace Probel.Geho.Gui
{
    using System;
    using System.ComponentModel;
    using System.Deployment.Application;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    using Microsoft.Practices.Unity;

    using Models;

    using Mvvm.Gui;
    using Mvvm.Toolkit.Events;

    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels;
    using Probel.Geho.Gui.Views;

    using Properties;

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

        private void CheckUpdate()
        {
            this.progressBar.Visibility
                = this.progressText.Visibility
                = Visibility.Collapsed;

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Task.Run(() => ApplicationDeployment.CurrentDeployment.CheckForUpdate())
                    .ContinueWith(t =>
                    {
                        if (t.Result)
                        {
                            var deployment = ApplicationDeployment.CurrentDeployment;
                            this.progressText.Visibility
                                = this.progressBar.Visibility
                                = Visibility.Visible;

                            deployment.UpdateProgressChanged += Deployment_UpdateProgressChanged;
                            deployment.UpdateCompleted += Deployment_UpdateCompleted;
                            deployment.UpdateAsync();
                        }
                    }, CancellationToken.None, TaskContinuationOptions.NotOnFaulted, scheduler)
                    .ContinueWith(t => new ErrorHandler().HandleError(t.Exception), CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, scheduler);
            }
            else
            {
                this.progressText.Visibility = Visibility.Visible;
                this.progressText.Text = Messages.Status_Offline;
            }
        }

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

        private void Deployment_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.progressBar.Visibility = Visibility.Collapsed;
            this.progressText.Text = Messages.Status_UpdateCompleted;
        }

        private void Deployment_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.CheckUpdate();
        }

        #endregion Methods
    }
}