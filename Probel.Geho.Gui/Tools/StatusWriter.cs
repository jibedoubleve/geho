namespace Probel.Geho.Gui.Tools
{
    using System;

    using Probel.Geho.Gui.Models;
    using Probel.Mvvm.Gui.PostOffice;

    using Properties;

    public class StatusWriter
    {
        #region Fields

        private readonly Messenger Messenger = new Messenger();

        #endregion Fields

        #region Methods

        public void Error(string message)
        {
            this.Write(Status.Error, message);
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

        private void Write(Status status, string msg)
        {
            this.Messenger.Post<UiMessage>(new UiMessage()
            {
                Message = msg,
                Status = status,
            });
        }

        private void Write(Status status, string format, params object[] args)
        {
            this.Messenger.Post<UiMessage>(new UiMessage()
            {
                Message = string.Format(format, args),
                Status = status,
            });
        }

        #endregion Methods
    }
}