namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

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

        #region Methods

        public static IEnumerable<BusyEducatorViewModel> ToViewModels(IEnumerable<PersonDto> people, IScheduleService srv)
        {
            var list = new List<BusyEducatorViewModel>();
            foreach (var person in people)
            {
                list.Add(new BusyEducatorViewModel(person, srv));
            }
            return list;
        }

        #endregion Methods
    }
}