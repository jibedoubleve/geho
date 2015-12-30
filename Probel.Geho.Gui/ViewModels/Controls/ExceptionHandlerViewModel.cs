namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;

    using Probel.Geho.Gui.Models;
    using Probel.Mvvm.Toolkit.Events;

    public class ExceptionHandlerViewModel : BaseViewModel, IEventHandler<UiMessage>
    {
        #region Fields

        private Exception exception;

        #endregion Fields

        #region Constructors

        public ExceptionHandlerViewModel()
        {
            this.Messenger.Subscribe(this);
        }

        #endregion Constructors

        #region Properties

        public Exception Exception
        {
            get { return this.exception; }
            set
            {
                this.exception = value;
                this.OnPropertyChanged(() => Exception);
            }
        }

        #endregion Properties

        #region Methods

        public void HandleEvent(UiMessage context)
        {
            if (context != null && context.Exception != null)
            {
                this.Exception = context.Exception;
            }
        }

        #endregion Methods
    }
}