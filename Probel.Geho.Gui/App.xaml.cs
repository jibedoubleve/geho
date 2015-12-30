namespace Probel.Geho.Gui
{
    using System.Windows;

    using Probel.Geho.Services;

    using Tools;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            DataBootstrap.Initialise();
            UnityBootstrap.Initialise();

            var view = new MainView(UnityBootstrap.Container);
            view.Show();

            base.OnStartup(e);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            new ErrorHandler().HandleError(e.Exception);
        }

        #endregion Methods
    }
}