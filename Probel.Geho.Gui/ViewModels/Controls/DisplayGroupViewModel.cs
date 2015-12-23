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
        private int groupOrder;

        #endregion Fields

        #region Constructors

        public DisplayGroupViewModel(GroupDto group, IEnumerable<PersonDto> morning, IEnumerable<PersonDto> afternoon)
        {
            this.GroupName = group.Name;
            this.GroupOrder = group.Order;
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

        public int GroupOrder
        {
            get { return this.groupOrder; }
            set
            {
                this.groupOrder = value;
                this.OnPropertyChanged(() => GroupOrder);
            }
        }

        #endregion Properties
    }
}