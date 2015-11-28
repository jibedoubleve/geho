namespace Probel.Geho.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Navigation;

    using Microsoft.Practices.Unity;

    using Probel.Geho.Data;
    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Gui.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Settings.Default.DataDirectory);
            DataBootstrap.Initialise();
            UnityBootstrap.Initialise();

            var view = new MainWindow(UnityBootstrap.Container);
            view.Show();

            base.OnStartup(e);
        }

        #endregion Methods
    }
}