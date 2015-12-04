namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Probel.Geho.Data.Dto;
    using Probel.Mvvm.DataBinding;

    public class DisplayGroupViewModel : ObservableObject
    {
        #region Fields

        private string groupName;

        #endregion Fields

        #region Constructors

        public DisplayGroupViewModel(string name, IEnumerable<PersonDto> morning, IEnumerable<PersonDto> afternoon)
        {
            this.GroupName = name;
            this.EducatorsMorning = new ObservableCollection<PersonDto>(morning);
            this.EducatorsAfternoon = new ObservableCollection<PersonDto>(afternoon);
        }

        #endregion Constructors

        #region Properties

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

        public string GroupName
        {
            get { return this.groupName; }
            set
            {
                this.groupName = value;
                this.OnPropertyChanged(() => GroupName);
            }
        }

        #endregion Properties
    }
}