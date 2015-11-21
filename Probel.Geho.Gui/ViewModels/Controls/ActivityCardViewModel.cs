namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.InMemoryQuery;
    using Probel.Mvvm.DataBinding;

    public class ActivityCardViewModel : ObservableObject
    {
        #region Fields

        protected readonly ILoadeableViewModel ParentVm;
        protected readonly IHrService Service;

        #endregion Fields

        #region Constructors

        public ActivityCardViewModel(ActivityDto activity, IHrService service, ILoadeableViewModel parentVm)
        {
            this.ParentVm = parentVm;
            this.Service = service;

            if (activity == null) { throw new ArgumentNullException("activity"); }
            else if (activity.People == null) { throw new ArgumentNullException("activity.Persons"); }

            this.Activity = activity;

            var b = activity.People.GetBeneficiaries();
            Beneficiaries = new ObservableCollection<PersonDto>(b);

            var e = activity.People.GetEducators();
            Educators = new ObservableCollection<PersonDto>(e);

            Name = Activity.Name;
            IsMorning = Activity.IsMorning;
            DayOfWeek = Activity.DayOfWeek;
        }

        #endregion Constructors

        #region Properties

        public ActivityDto Activity
        {
            get;
            private set;
        }

        public ObservableCollection<PersonDto> Beneficiaries
        {
            get;
            private set;
        }

        public DayOfWeek DayOfWeek
        {
            get { return this.Activity.DayOfWeek; }
            set
            {
                this.Activity.DayOfWeek = value;
                this.OnPropertyChanged(() => DayOfWeek);
            }
        }

        public ObservableCollection<PersonDto> Educators
        {
            get;
            private set;
        }

        public bool IsMorning
        {
            get { return this.Activity.IsMorning; }
            set
            {
                this.Activity.IsMorning = value;
                this.OnPropertyChanged(() => IsMorning);
            }
        }

        public string Name
        {
            get { return this.Activity.Name; }
            set
            {
                this.Activity.Name = value;
                this.OnPropertyChanged(() => Name);
            }
        }

        #endregion Properties

        #region Methods

        public static IEnumerable<ActivityCardViewModel> ToActivityCardViewModel(IEnumerable<ActivityDto> activities, IHrService service, ILoadeableViewModel parentVm)
        {
            var list = new List<ActivityCardViewModel>();
            foreach (var a in activities)
            {
                list.Add(new ActivityCardViewModel(a, service, parentVm));
            }
            return list;
        }

        #endregion Methods
    }
}