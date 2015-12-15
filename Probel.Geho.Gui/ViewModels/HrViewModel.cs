namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.BusinessLogic.BusinessRules;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.InMemoryQuery;
    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Gui.Tools;
    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Mvvm.DataBinding;
    using Probel.Mvvm.Gui;

    public class HrViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly ICommand addAbsenceCommand;
        private readonly ICommand addPersonCommand;
        private readonly IHrService Service;

        private AbsenceDto absenceToAdd;
        private IEnumerable<PersonDto> bufferPersons = new List<PersonDto>();
        private int endOffset = 18;
        private bool filterEducator;
        private LunchManagementViewModel lunchManagementViewModel;
        private MedicalExamViewModel medicalExamViewModel;
        private PersonDto personToAdd;
        private int startOffset = 8;

        #endregion Fields

        #region Constructors

        public HrViewModel(IHrService service)
        {
            this.Service = service;
            this.MedicalExamViewModel = new MedicalExamViewModel(service);
            this.Beneficiaries = new ObservableCollection<PersonViewModel>();
            this.Educators = new ObservableCollection<PersonViewModel>();
            this.Absences = new ObservableCollection<AbsenceViewModel>();
            this.FilteredPersons = new ObservableCollection<PersonDto>();
            this.Groups = new ObservableCollection<GroupViewModel>();
            this.Activities = new ObservableCollection<ActivityCardViewModel>();

            this.PersonToAdd = new PersonDto();
            this.AbsenceToAdd = new AbsenceDto();

            this.addPersonCommand = new RelayCommand(AddPerson, CanAddPerson);
            this.addAbsenceCommand = new RelayCommand(AddAbsence, CanAddAbsence);
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<AbsenceViewModel> Absences
        {
            get;
            private set;
        }

        public AbsenceDto AbsenceToAdd
        {
            get { return this.absenceToAdd; }
            set
            {
                this.absenceToAdd = value;
                this.OnPropertyChanged(() => AbsenceToAdd);
            }
        }

        public ObservableCollection<ActivityCardViewModel> Activities
        {
            get;
            private set;
        }

        public ICommand AddAbsenceCommand
        {
            get { return this.addAbsenceCommand; }
        }

        public ICommand AddPersonCommand
        {
            get { return this.addPersonCommand; }
        }

        public ObservableCollection<PersonViewModel> Beneficiaries
        {
            get;
            private set;
        }

        public ObservableCollection<PersonViewModel> Educators
        {
            get;
            private set;
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

        public ObservableCollection<GroupViewModel> Groups
        {
            get;
            private set;
        }

        public LunchManagementViewModel LunchManagementViewModel
        {
            get { return this.lunchManagementViewModel; }
            set
            {
                this.lunchManagementViewModel = value;
                this.OnPropertyChanged(() => LunchManagementViewModel);
            }
        }

        public MedicalExamViewModel MedicalExamViewModel
        {
            get { return this.medicalExamViewModel; }
            set
            {
                this.medicalExamViewModel = value;
                this.OnPropertyChanged(() => MedicalExamViewModel);
            }
        }

        public PersonDto PersonToAdd
        {
            get { return this.personToAdd; }
            set
            {
                this.personToAdd = value;
                this.OnPropertyChanged(() => PersonToAdd);
            }
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
            this.MedicalExamViewModel.Load();
            bufferPersons = this.Service.GetPersons();

            var p = bufferPersons;
            var a = this.Service.GetAbsences();
            var g = this.Service.GetGroups();
            var ac = this.Service.GetActivities();

            if (FilterEducator) { this.FilteredPersons.Refill(this.bufferPersons.GetEducators()); }
            else { this.FilteredPersons.Refill(this.bufferPersons.GetBeneficiaries()); }

            this.Beneficiaries.Refill(PersonViewModel.ToViewModels(p.GetBeneficiaries(), Service, this));
            this.Educators.Refill(PersonViewModel.ToViewModels(p.GetEducators(), Service, this));
            this.Absences.Refill(AbsenceViewModel.ToViewModels(a, Service, this));
            this.Groups.Refill(GroupViewModel.ToViewModels(g, Service, this));
            this.Activities.Refill(ActivityCardViewModel.ToActivityCardViewModel(ac));

            var vm = new LunchManagementViewModel(this.Service);
            vm.Load();
            this.LunchManagementViewModel = vm;
        }

        private void AddAbsence()
        {
            this.AbsenceToAdd.Start = this.AbsenceToAdd.Start.Date.AddHours(this.StartOffset);
            this.AbsenceToAdd.End = this.AbsenceToAdd.End.Date.AddHours(this.EndOffset);

            var status = this.Service.IsAbsenceValid(this.AbsenceToAdd);
            if (status.IsValid)
            {
                this.Service.CreateAbsence(this.AbsenceToAdd);
                this.AbsenceToAdd = new AbsenceDto();
                this.Load();
            }
            else { ViewService.MessageBox.Warning(status.Error); }
        }

        private void AddPerson()
        {
            using (WaitingCursor.While)
            {
                this.Service.CreatePerson(this.PersonToAdd);
                this.PersonToAdd = new PersonDto();
                this.Load();
            }
        }

        private bool CanAddAbsence()
        {
            return this.AbsenceToAdd != null
                && this.AbsenceToAdd.Person != null
                && AbsenceToAdd.Start <= AbsenceToAdd.End
                && AbsenceToAdd.Start >= DateTime.Today.AddDays(-1);
        }

        private bool CanAddPerson()
        {
            return this.PersonToAdd != null
                && !string.IsNullOrWhiteSpace(this.PersonToAdd.Name);
        }

        #endregion Methods
    }
}