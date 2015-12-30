namespace Probel.Geho.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Mvvm.Gui;
    using Probel.Mvvm.Gui.Messages;
    using Probel.Mvvm.Toolkit.Events;

    public static class AppContext
    {
        #region Fields

        public static readonly EventAggregator Messenger = new EventAggregator();
        public static readonly INotifyUser Notifyer = new WindowsMessageBox();

        #endregion Fields
    }
}