namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Gui.Tools;
    using Probel.Mvvm.DataBinding;
    using Probel.Mvvm.Gui;

    public class AbsenceViewModel : ObservableObject
    {
        #region Fields

        private readonly ICommand deleteAbsenceCommand;
        private readonly ILoadeableViewModel ParentVm;
        private readonly IHrService Service;

        private AbsenceDto absence;

        #endregion Fields

        #region Constructors

        public AbsenceViewModel( IHrService service, ILoadeableViewModel parentVm)
        {
            this.ParentVm = parentVm;
            this.Service = service;
            this.deleteAbsenceCommand = new RelayCommand(DeleteAbsence, CanDeleteAbsence);
        }

        #endregion Constructors

        #region Properties

        public AbsenceDto Absence
        {
            get { return this.absence; }
            set
            {
                this.absence = value;
                this.OnPropertyChanged(() => Absence);
            }
        }

        public ICommand DeleteAbsenceCommand
        {
            get { return this.deleteAbsenceCommand; }
        }

        #endregion Properties

        #region Methods

        public static IEnumerable<AbsenceViewModel> ToViewModels(IEnumerable<AbsenceDto> absences, IHrService service, ILoadeableViewModel vm)
        {
            var result = new List<AbsenceViewModel>();
            foreach (var absence in absences)
            {
                result.Add(new AbsenceViewModel(service, vm) { Absence = absence });
            }
            return result;
        }

        private bool CanDeleteAbsence()
        {
            return this.Absence != null;
        }

        private void DeleteAbsence()
        {
            var yes = ViewService.MessageBox.Question(Messages.Msg_AskDeleteAbsence);
            if (yes)
            {
                using (WaitingCursor.While)
                {
                    this.Service.RemoveAbsence(this.Absence);
                    this.ParentVm.Load();
                }
            }
        }

        #endregion Methods
    }
}