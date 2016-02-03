namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Mvvm.Toolkit.DataBinding;
    using Mvvm.Toolkit.Events;

    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    using Properties;

    using Services.InMemoryQuery;

    using Tools;

    public class MedicalExamViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly IHrService Service;

        private AbsenceDto absenceToAdd;
        private IEnumerable<PersonDto> bufferPersons = new List<PersonDto>();
        private int endOffset = 12;
        private bool filterEducator;
        private int startOffset = 8;

        #endregion Fields

        #region Constructors

        public MedicalExamViewModel(IHrService service)
        {
            this.AddMedicalExamCommand = new RelayCommand(AddMedicalExam, CanAddMedicalExam);
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

        public ICommand AddMedicalExamCommand
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

        public override async void Load()
        {
            this.bufferPersons = await Task.Run(() => this.Service.GetPersons());
            this.FilterEducator = false;
            var me = await Task.Run(() => this.Service.GetAbsences(isPresent: true));
            this.MedicalExams.Refill(me.ToViewModels(Service, this));
        }

        private void AddMedicalExam()
        {
            this.AbsenceToAdd.End = this.AbsenceToAdd.Start;
            this.AbsenceToAdd.Start = this.AbsenceToAdd.Start.Date.AddHours(this.StartOffset);
            this.AbsenceToAdd.End = this.AbsenceToAdd.End.Date.AddHours(this.EndOffset);
            this.AbsenceToAdd.IsPresent = true;

            var status = this.Service.IsAbsenceValid(this.AbsenceToAdd);
            if (status.IsValid)
            {
                this.Service.CreateAbsence(this.AbsenceToAdd);
                this.AbsenceToAdd = new AbsenceDto();
                this.Load();
                this.Status.Info(Messages.Msg_MedicalExamAdded);
                AppContext.Messenger.Publish(this);
            }
            else { Notifyer.Warning(status.Error); }
        }

        private bool CanAddMedicalExam()
        {
            return this.AbsenceToAdd.Person != null
                && !string.IsNullOrWhiteSpace(this.AbsenceToAdd.Person.Name);
        }

        #endregion Methods
    }
}