namespace Probel.Geho.Gui.ViewModels
{
    using Mvvm.Gui.PostOffice;

    using Probel.Geho.Gui.Models;
    using Probel.Mvvm.DataBinding;

    public class MainViewModel : ObservableObject
    {
        #region Fields

        private readonly Messenger Messenger = new Messenger();

        private UiMessage uiMessage;

        #endregion Fields

        #region Constructors

        public MainViewModel()
        {
            this.Messenger.Subscribe<UiMessage>(e => this.UiMessage = e);
        }

        #endregion Constructors

        #region Properties

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
    }
}