namespace Probel.Geho.Gui.Models
{
    using Properties;

    #region Enumerations

    public enum Status
    {
        Warn,
        Error,
        Info,
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