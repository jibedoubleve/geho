namespace Probel.Geho.Gui.ViewModels
{
    using Mvvm.Gui;
    using Mvvm.Toolkit.DataBinding;
    using Mvvm.Toolkit.Events;

    using Probel.Geho.Gui.Tools;

    public class BaseViewModel : ObservableObject
    {
        #region Fields

        protected readonly ErrorHandler ErrorHandler = new ErrorHandler();
        protected readonly EventAggregator Messenger = AppContext.Messenger;
        protected readonly INotifyUser Notifyer = AppContext.Notifyer;
        protected readonly StatusWriter Status = new StatusWriter();

        #endregion Fields
    }
}