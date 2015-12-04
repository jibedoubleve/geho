namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Gui.Tools;
    using Probel.Mvvm.DataBinding;
    using Probel.Mvvm.Gui;

    public class PersonViewModel : ObservableObject
    {
        #region Fields

        private readonly ICommand cancelCommand;
        private readonly ICommand deletePersonCommand;
        private readonly ICommand editCommand;
        private readonly ILoadeableViewModel ParentVm;
        private readonly IHrService Service;

        private bool isEdition;
        private PersonDto person;

        #endregion Fields

        #region Constructors

        public PersonViewModel(ILoadeableViewModel parentVm, IHrService service)
        {
            this.ParentVm = parentVm;
            this.Service = service;
            this.deletePersonCommand = new RelayCommand(DeletePerson, CanDeletePerson);
            this.editCommand = new RelayCommand(Edit, CanEdit);
            this.cancelCommand = new RelayCommand(Cancel, CanCancel);
        }

        #endregion Constructors

        #region Properties

        public ICommand CancelCommand
        {
            get { return this.cancelCommand; }
        }

        public ICommand DeletePersonCommand
        {
            get { return this.deletePersonCommand; }
        }

        public ICommand EditCommand
        {
            get { return this.editCommand; }
        }

        public bool IsEdition
        {
            get { return this.isEdition; }
            set
            {
                this.isEdition = value;
                this.OnPropertyChanged(() => IsEdition);
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

        public static IEnumerable<PersonViewModel> ToViewModels(IEnumerable<PersonDto> absences, IHrService service, ILoadeableViewModel vm)
        {
            var result = new List<PersonViewModel>();
            foreach (var person in absences)
            {
                result.Add(new PersonViewModel(vm, service) { Person = person, });
            }
            return result;
        }

        private bool CanCancel()
        {
            return this.Person != null; ;
        }

        private void Cancel()
        {
            this.Person = this.Service.GetPerson(this.Person.Id);
        }

        private bool CanDeletePerson()
        {
            return this.Person != null;
        }

        private bool CanEdit()
        {
            return !string.IsNullOrWhiteSpace(this.Person.Name);
        }

        private void DeletePerson()
        {
            var yes = ViewService.MessageBox.Question(string.Format(Messages.Msg_AskDeletePerson, this.Person.Name, this.Person.Surname));
            if (yes)
            {
                using (WaitingCursor.While)
                {
                    this.Service.RemovePerson(this.Person);
                    this.ParentVm.Load();
                }
            }
        }

        private void Edit()
        {
            this.Service.UpdatePerson(this.Person);
        }

        #endregion Methods
    }
}