namespace Probel.Geho.Gui.Tools
{
    using System;

    using Probel.Mvvm.Gui;

    public class ErrorHandler
    {
        #region Methods

        public void HandleError(Exception ex)
        {
            ViewService.MessageBox.Error(ex);
            new StatusWriter().Error(ex.Message);
        }

        public void HandleWarning(string message)
        {
            ViewService.MessageBox.Warning(message);
            new StatusWriter().Warn(message);
        }

        #endregion Methods
    }
}