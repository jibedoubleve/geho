namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Data.Entities;

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
            else if (activity.People == null) { throw new ArgumentNullException("activity.People"); }

            this.Activity = activity;

            var beneficiaries = activity.People.GetBeneficiaries();
            Beneficiaries = new ObservableCollection<PersonDto>(beneficiaries);
            HasBeneficiaries = Beneficiaries.Count > 0;
            Beneficiaries.CollectionChanged += (s, e) => this.HasBeneficiaries = Beneficiaries.Count > 0;

            var educators = activity.People.GetEducators();
            Educators = new ObservableCollection<PersonDto>(educators);

            Name = Activity.Name;
            MomentDay = Activity.MomentDay;
            DayOfWeek = Activity.DayOfWeek;
        }

        #endregion Constructors

        #region Properties

        private bool hasBeneficiaries;
        public bool HasBeneficiaries
        {
            get { return this.hasBeneficiaries; }
            set
            {
                this.hasBeneficiaries = value;
                this.OnPropertyChanged(() => HasBeneficiaries);
            }
        }
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

        public MomentDay MomentDay
        {
            get { return this.Activity.MomentDay; }
            set
            {
                this.Activity.MomentDay = value;
                this.OnPropertyChanged(() => MomentDay);
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