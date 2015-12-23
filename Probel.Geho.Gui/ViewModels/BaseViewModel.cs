namespace Probel.Geho.Gui.ViewModels
{
    using Probel.Geho.Gui.Tools;
    using Probel.Mvvm.DataBinding;

    public class BaseViewModel : ObservableObject
    {
        #region Fields

        protected readonly ErrorHandler ErrorHandler = new ErrorHandler();
        protected readonly StatusWriter StatusBar = new StatusWriter();

        #endregion Fields
    }
}