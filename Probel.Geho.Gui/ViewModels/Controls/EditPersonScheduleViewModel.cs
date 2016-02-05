namespace Probel.Geho.Gui.ViewModels.Controls
{
    using Services.Dto;

    using Tools;

    public class EditPersonScheduleViewModel : BaseViewModel
    {
        #region Fields

        private ColourStatus colourStatus = ColourStatus.Transparent;
        private bool isSelected;

        #endregion Fields

        #region Constructors

        public EditPersonScheduleViewModel(PersonDto person)
        {
            this.Person = person;
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
            get { return this.Person.GroupNames; }
            set
            {
                this.Person.GroupNames = value;
                this.OnPropertyChanged(() => GroupNames);
            }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged(() => IsSelected);
            }
        }

        public bool IsTrainee
        {
            get { return this.Person.IsTrainee; }
            set
            {
                this.Person.IsTrainee = value;
                this.OnPropertyChanged(() => IsTrainee);
            }
        }

        public string Name
        {
            get { return this.Person.Name; }
            set
            {
                this.Person.Name = value;
                this.OnPropertyChanged(() => Name);
            }
        }

        public PersonDto Person
        {
            get; private set;
        }

        public string Surname
        {
            get { return this.Person.Surname; }
            set
            {
                this.Person.Surname = value;
                this.OnPropertyChanged(() => Surname);
            }
        }

        #endregion Properties
    }
}