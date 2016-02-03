namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Reflection;

    using Controls;

    using Mvvm.Toolkit.DataBinding;
    using Mvvm.Toolkit.Events;

    using Probel.Geho.Gui.Models;

    public class MainViewModel : BaseViewModel, IEventHandler<UiMessage>
    {
        #region Fields

        private string appVersion;
        private bool isError;
        private UiMessage uiMessage;

        #endregion Fields

        #region Constructors

        public MainViewModel()
        {
            this.Messenger.Subscribe(this);
            var v = Assembly.GetExecutingAssembly().GetName().Version;
            this.AppVersion = string.Format("V.{0}.{1}.{2}", v.Major, v.Minor, v.Build);
        }

        #endregion Constructors

        #region Properties

        public string AppVersion
        {
            get { return this.appVersion; }
            set
            {
                this.appVersion = value;
                this.OnPropertyChanged(() => AppVersion);
            }
        }

        public bool IsError
        {
            get { return this.isError; }
            set
            {
                this.isError = value;
                this.OnPropertyChanged(() => IsError);
            }
        }

        public UiMessage UiMessage
        {
            get { return this.uiMessage; }
            set
            {
                this.uiMessage = value;
                this.OnPropertyChanged(() => UiMessage);
            }
        }

        #endregion Properties

        #region Methods

        public void HandleEvent(UiMessage context)
        {
            this.UiMessage = context;
            this.IsError = (context.Exception != null);
        }

        #endregion Methods
    }
}