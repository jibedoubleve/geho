namespace Probel.Geho.Gui.Models
{
    using System;

    using Properties;

    #region Enumerations

    public enum Status
    {
        Warn,
        Error,
        Info,
        Debug,
        Empty,
    }

    #endregion Enumerations

    public class UiMessage
    {
        #region Constructors

        public UiMessage()
        {
            this.Message = Messages.Msg_Ready;
            this.Status = Status.Info;
        }

        #endregion Constructors

        #region Properties

        public Exception Exception
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public Status Status
        {
            get; set;
        }

        #endregion Properties
    }
}