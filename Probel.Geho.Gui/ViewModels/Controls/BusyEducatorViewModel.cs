namespace Probel.Geho.Gui.ViewModels.Controls
{
    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    public class BusyEducatorViewModel : ObservableObject
    {
        #region Fields

        private readonly IScheduleService Service;

        private bool isBusy;
        private PersonDto person;

        #endregion Fields

        #region Constructors

        public BusyEducatorViewModel(PersonDto person, IScheduleService service)
        {
            this.Service = service;
            this.Person = person;
        }

        #endregion Constructors

        #region Properties

        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                this.OnPropertyChanged(() => IsBusy);
            }
        }

        public PersonDto Person
        {
            get { return this.person; }
            set
            {
                this.person = value;
                this.OnPropertyChanged(() => Person);
            }
        }

        #endregion Properties
    }
}