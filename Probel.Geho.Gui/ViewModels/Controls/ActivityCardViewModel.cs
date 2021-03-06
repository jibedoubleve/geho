﻿namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.ObjectModel;

    using Probel.Geho.Services.Dto;
    using Probel.Geho.Services.InMemoryQuery;

    using Services.Entities;

    public class ActivityCardViewModel : BaseViewModel
    {
        #region Fields

        private bool hasBeneficiaries;

        #endregion Fields

        #region Constructors

        public ActivityCardViewModel(ActivityDto activity)
        {
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

        public bool HasBeneficiaries
        {
            get { return this.hasBeneficiaries; }
            set
            {
                this.hasBeneficiaries = value;
                this.OnPropertyChanged(() => HasBeneficiaries);
            }
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
    }
}