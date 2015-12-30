namespace Probel.Geho.Gui.Tools
{
    using System;

    using Mvvm.Gui;

    public class ErrorHandler
    {
        #region Fields

        private INotifyUser Notifyer = AppContext.Notifyer;

        #endregion Fields

        #region Methods

        public void HandleError(Exception ex)
        {
            Notifyer.Error(ex.Message);
            new StatusWriter().Error(ex);
        }

        public void HandleWarning(string message)
        {
            Notifyer.Warning(message);
            new StatusWriter().Warn(message);
        }

        #endregion Methods
    }
}