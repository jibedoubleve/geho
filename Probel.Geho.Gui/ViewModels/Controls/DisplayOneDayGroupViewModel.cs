﻿namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Data.BusinessLogic;

    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

    public class DisplayOneDayGroupViewModel : LoadeableViewModel
    {
        #region Fields

        private readonly DateTime CurrentDate;
        private readonly IScheduleService Service;

        private GroupDto group;
        private bool hasAfternoonBeneficiaries;
        private bool hasMorningBeneficiaries;

        #endregion Fields

        #region Constructors

        public DisplayOneDayGroupViewModel(IScheduleService srv
            , DateTime curentDate
            , GroupDto group
            , IEnumerable<PersonDto> morning
            , IEnumerable<PersonDto> afternoon)
        {
            this.CurrentDate = curentDate;
            this.Service = srv;
            this.Group = group;
            this.EducatorsMorning = new ObservableCollection<PersonDto>(morning);
            this.EducatorsAfternoon = new ObservableCollection<PersonDto>(afternoon);

            this.BeneficiariesMorning = new ObservableCollection<PersonDto>();
            this.BeneficiariesAfternoon = new ObservableCollection<PersonDto>();
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<PersonDto> BeneficiariesAfternoon
        {
            get;
            private set;
        }

        public ObservableCollection<PersonDto> BeneficiariesMorning
        {
            get;
            private set;
        }

        public ObservableCollection<PersonDto> EducatorsAfternoon
        {
            get;
            private set;
        }

        public ObservableCollection<PersonDto> EducatorsMorning
        {
            get;
            private set;
        }

        public GroupDto Group
        {
            get { return this.group; }
            set
            {
                this.group = value;
                this.OnPropertyChanged(() => Group);
            }
        }

        public bool HasAfternoonBeneficiaries
        {
            get { return this.hasAfternoonBeneficiaries; }
            set
            {
                this.hasAfternoonBeneficiaries = value;
                this.OnPropertyChanged(() => HasAfternoonBeneficiaries);
            }
        }

        public bool HasMorningBeneficiaries
        {
            get { return this.hasMorningBeneficiaries; }
            set
            {
                this.hasMorningBeneficiaries = value;
                this.OnPropertyChanged(() => HasMorningBeneficiaries);
            }
        }

        #endregion Properties

        #region Methods

        public override void Load()
        {
            var morning = this.Service.GetFreeBeneficiaries(this.Group, this.CurrentDate, isMorning: true);
            this.BeneficiariesMorning.Refill(morning);
            this.HasMorningBeneficiaries = morning.Count() > 0;

            var afternoon = this.Service.GetFreeBeneficiaries(this.Group, this.CurrentDate, isMorning: false);
            this.BeneficiariesAfternoon.Refill(afternoon);
            this.HasAfternoonBeneficiaries = afternoon.Count() > 0;
        }

        #endregion Methods
    }
}