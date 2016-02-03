namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Mvvm.Gui;
    using Mvvm.Toolkit.DataBinding;
    using Mvvm.Toolkit.Events;

    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;
    using Probel.Geho.Services.InMemoryQuery;

    using Properties;

    using Tools;

    public class HrViewModel : LoadeableViewModel, IEventHandler<MedicalExamViewModel>
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
            this.MedicalExams = new ObservableCollection<AbsenceViewModel>();
            this.FilteredPersons = new ObservableCollection<PersonDto>();
            this.Groups = new ObservableCollection<GroupViewModel>();
            this.Activities = new ObservableCollection<ActivityCardViewModel>();

            this.PersonToAdd = new PersonDto();
            this.AbsenceToAdd = new AbsenceDto();

            this.addPersonCommand = new RelayCommand(AddPerson, CanAddPerson);
            this.addAbsenceCommand = new RelayCommand(AddAbsence, CanAddAbsence);

            AppContext.Messenger.Subscribe(this);
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

        public ObservableCollection<AbsenceViewModel> MedicalExams
        {
            get;
            private set;
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

        public async void HandleEvent(MedicalExamViewModel context)
        {
            this.bufferPersons = await Task.Run(() => this.Service.GetPersons());
            this.FilterEducator = false;
            var me = await Task.Run(() => this.Service.GetAbsences(isPresent: true));
            this.MedicalExams.Refill(me.ToViewModels(Service, this));
        }

        public override void Load()
        {
            this.Status.Loading();
            bufferPersons = this.Service.GetPersons();

            var p = bufferPersons;
            var a = this.Service.GetAbsences();
            var v = this.Service.GetAbsences(isPresent: true);
            var g = this.Service.GetGroups();
            var ac = this.Service.GetActivities();

            if (FilterEducator) { this.FilteredPersons.Refill(this.bufferPersons.GetEducators()); }
            else { this.FilteredPersons.Refill(this.bufferPersons.GetBeneficiaries()); }

            this.Beneficiaries.Refill(p.GetBeneficiaries().ToViewModels(Service, this));
            this.Educators.Refill(p.GetEducators().ToViewModels(Service, this));
            this.Absences.Refill(a.ToViewModels(Service, this));
            this.MedicalExams.Refill(v.ToViewModels(Service, this));
            this.Groups.Refill(g.ToViewModels(Service, this));
            this.Activities.Refill(ac.ToViewModels());

            var vm = new LunchManagementViewModel(this.Service);
            vm.Load();
            this.MedicalExamViewModel.Load();
            this.LunchManagementViewModel = vm;
            this.Status.Ready();
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
                this.Status.Info(Messages.Msg_AbsenceCreated);
            }
            else { ErrorHandler.HandleWarning(status.Error); }
        }

        private void AddPerson()
        {
            using (WaitingCursor.While)
            {
                this.Service.CreatePerson(this.PersonToAdd);
                var name = this.PersonToAdd.Name;
                var surname = this.PersonToAdd.Surname;

                this.PersonToAdd = new PersonDto();
                this.Load();
                this.Status.InfoFormat(Messages.Msg_PersonAdded, name, surname);
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