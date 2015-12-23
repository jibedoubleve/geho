namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

    using Properties;

    #region Enumerations

    public enum ColourStatus
    {
        Transparent,
        Red,
        Green,
    }

    #endregion Enumerations

    public class PersonFlatBusyViewModel : BaseViewModel
    {
        #region Fields

        private readonly ManageGroupDayViewModel ParentVm;
        private readonly IScheduleService Service;
        private readonly PersonDto _person;

        private ColourStatus colourStatus;
        private bool isBusy;
        private bool isSelected;

        #endregion Fields

        #region Constructors

        public PersonFlatBusyViewModel(PersonDto person, IScheduleService service, ManageGroupDayViewModel parentVm)
        {
            this.Service = service;
            this._person = person;
            this.ParentVm = parentVm;
        }

        #endregion Constructors

        #region Properties

        public ColourStatus ColourStatus
        {
            get { return this.colourStatus; }
            set
            {
                this.colourStatus = value;
                this.OnPropertyChanged(() => ColourStatus);
            }
        }

        public string GroupNames
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Person.GroupNames)
                  ? string.Empty
                  : "(" + this.Person.GroupNames + ")";
            }
            set
            {
                this.Person.GroupNames = value;
                this.OnPropertyChanged(() => GroupNames);
            }
        }

        /// <summary>
        /// Indicates whether this user is busy in other group or not
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                this.SetColourStatus();
                this.OnPropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        /// Indicates whether this user is working in this group or not
        /// </summary>
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                this.SetColourStatus();
                this.OnPropertyChanged(() => IsSelected);
                this.Save();
            }
        }

        public bool IsTrainee
        {
            get { return this._person.IsTrainee; }
            set
            {
                this._person.IsTrainee = value;
                this.OnPropertyChanged(() => IsTrainee);
            }
        }

        public string Name
        {
            get { return this._person.Name; }
            set
            {
                this._person.Name = value;
                this.OnPropertyChanged(() => Name);
            }
        }

        public PersonDto Person
        {
            get { return this._person; }
        }

        public string Surname
        {
            get { return this._person.Surname; }
            set
            {
                this._person.Surname = value;
                this.OnPropertyChanged(() => Surname);
            }
        }

        #endregion Properties

        #region Methods

        public static IEnumerable<PersonFlatBusyViewModel> ToViewModels(IEnumerable<PersonDto> people, IScheduleService service, ManageGroupDayViewModel parentVm)
        {
            var list = new List<PersonFlatBusyViewModel>();
            foreach (var person in people.OrderBy(e => e.Name).ThenBy(e => e.Surname))
            {
                list.Add(new PersonFlatBusyViewModel(person, service, parentVm));
            }
            return list;
        }

        public void Save()
        {
            this.ParentVm.Save();
            this.ParentVm.ParentVm.MarkBusyEducators();
            this.StatusBar.InfoFormat(IsSelected ? Messages.Msg_AddPersonInGroup : Messages.Msg_RemovePersonFromGroup
                , this.Name
                , this.Surname);
        }

        public void SetSelected()
        {
            this.isSelected = true;
            this.SetColourStatus();
        }

        private void SetColourStatus()
        {
            if (!this.IsBusy && !this.IsSelected) { ColourStatus = ColourStatus.Transparent; }
            else if (this.IsBusy && !this.IsSelected) { ColourStatus = ColourStatus.Red; }
            else { ColourStatus = ColourStatus.Green; }
        }

        #endregion Methods
    }
}