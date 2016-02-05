namespace Probel.Geho.Gui.Tools
{
    using System;
    using System.Diagnostics;

    using Mvvm.Toolkit.Events;

    using Probel.Geho.Gui.Models;

    using Properties;

    public class StatusWriter
    {
        #region Fields

        private readonly EventAggregator Messenger = AppContext.Messenger;

        #endregion Fields

        #region Methods

        [Conditional("DEBUG")]
        public void Debug(string message)
        {
            var dt = string.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss.ff"), message);
            System.Diagnostics.Debug.WriteLine(message);
            this.Write(Status.Debug, message);
        }

        [Conditional("DEBUG")]
        public void Debug(string format, params object[] args)
        {
            var message = string.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss.ff"), string.Format(format, args));
            System.Diagnostics.Debug.WriteLine(message);
            this.Write(Status.Debug, message);
        }

        public void Error(Exception ex)
        {
            this.Write(Status.Error, ex.Message, ex);
        }

        public void Info(string message)
        {
            this.Write(Status.Info, message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.Write(Status.Info, format, args);
        }

        public void Loading()
        {
            this.Write(Status.Info, Messages.Msg_Loading);
        }

        public void Ready()
        {
            this.Write(Status.Empty, Messages.Msg_Ready);
        }

        public void Warn(string message)
        {
            this.Write(Status.Warn, message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            this.Write(Status.Warn, format, args);
        }

        private void Write(Status status, string msg, Exception ex = null)
        {
            this.Messenger.Publish(new UiMessage()
            {
                Message = msg.Replace(Environment.NewLine, " "),
                Status = status,
                Exception = ex,
            });
        }

        private void Write(Status status, string format, params object[] args)
        {
            this.Messenger.Publish(new UiMessage()
            {
                Message = string.Format(format, args),
                Status = status,
            });
        }

        #endregion Methods
    }
}