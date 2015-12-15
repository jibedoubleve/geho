namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Data.InMemoryQuery;

    using Mvvm.DataBinding;
    using Mvvm.Gui;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;

    public class MedicalExamViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly ICommand addAbsenceCommand;
        private readonly IHrService Service;

        private AbsenceDto absenceToAdd;
        private IEnumerable<PersonDto> bufferPersons = new List<PersonDto>();
        private int endOffset = 18;
        private bool filterEducator;
        private int startOffset = 8;

        #endregion Fields

        #region Constructors

        public MedicalExamViewModel(IHrService service)
        {
            this.addAbsenceCommand = new RelayCommand(AddAbsence, CanAddPerson);
            this.MedicalExams = new ObservableCollection<AbsenceViewModel>();
            this.FilteredPersons = new ObservableCollection<PersonDto>();
            this.AbsenceToAdd = new AbsenceDto();
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        public AbsenceDto AbsenceToAdd
        {
            get { return this.absenceToAdd; }
            set
            {
                this.absenceToAdd = value;
                this.OnPropertyChanged(() => AbsenceToAdd);
            }
        }

        public ICommand AddAbsenceCommand
        {
            get { return this.addAbsenceCommand; }
        }

        public int EndOffset
        {
            get { return this.endOffset; }
            set
            {
                this.endOffset = value;
                this.OnPropertyChanged(() => EndOffset);
            }
        }

        public ObservableCollection<PersonDto> FilteredPersons
        {
            get;
            private set;
        }

        public bool FilterEducator
        {
            get { return this.filterEducator; }
            set
            {
                this.filterEducator = value;

                if (FilterEducator) { this.FilteredPersons.Refill(this.bufferPersons.GetEducators()); }
                else { this.FilteredPersons.Refill(this.bufferPersons.GetBeneficiaries()); }

                this.OnPropertyChanged(() => FilterEducator);
            }
        }

        public ObservableCollection<AbsenceViewModel> MedicalExams
        {
            get;
            private set;
        }

        public int StartOffset
        {
            get { return this.startOffset; }
            set
            {
                this.startOffset = value;
                this.OnPropertyChanged(() => StartOffset);
            }
        }

        #endregion Properties

        #region Methods

        public override void Load()
        {
            this.bufferPersons = this.Service.GetPersons();
            this.FilterEducator = false;
            var me = this.Service.GetAbsences(isPresent: true);
            this.MedicalExams.Refill(AbsenceViewModel.ToViewModels(me, Service, this));
        }

        private void AddAbsence()
        {
            this.AbsenceToAdd.Start = this.AbsenceToAdd.Start.Date.AddHours(this.StartOffset);
            this.AbsenceToAdd.End = this.AbsenceToAdd.End.Date.AddHours(this.EndOffset);
            this.AbsenceToAdd.IsPresent = true;

            var status = this.Service.IsAbsenceValid(this.AbsenceToAdd);
            if (status.IsValid)
            {
                this.Service.CreateAbsence(this.AbsenceToAdd);
                this.AbsenceToAdd = new AbsenceDto();
                this.Load();
            }
            else { ViewService.MessageBox.Warning(status.Error); }
        }

        private bool CanAddPerson()
        {
            return this.AbsenceToAdd.Person != null
                && !string.IsNullOrWhiteSpace(this.AbsenceToAdd.Person.Name);
        }

        #endregion Methods
    }
}