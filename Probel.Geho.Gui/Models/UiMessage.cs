namespace Probel.Geho.Gui.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #region Enumerations

    public enum Status
    {
        Warn,
        Error,
        Info,
    }

    #endregion Enumerations

    public class UiMessage
    {
        #region Properties

        public Status Status
        {
            get; set;
        }

        #endregion Properties
    }
}