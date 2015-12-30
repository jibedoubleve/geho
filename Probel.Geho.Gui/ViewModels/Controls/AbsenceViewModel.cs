namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Windows.Input;

    using Mvvm.Gui;
    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    public class AbsenceViewModel : BaseViewModel
    {
        #region Fields

        private readonly ICommand deleteAbsenceCommand;
        private readonly ILoadeableViewModel ParentVm;
        private readonly IHrService Service;

        private AbsenceDto absence;

        #endregion Fields

        #region Constructors

        public AbsenceViewModel(IHrService service, ILoadeableViewModel parentVm)
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

        private bool CanDeleteAbsence()
        {
            return this.Absence != null;
        }

        private void DeleteAbsence()
        {
            var yes = Notifyer.Question(Messages.Msg_AskDeleteAbsence);
            if (yes)
            {
                using (WaitingCursor.While)
                {
                    this.Service.RemoveAbsence(this.Absence);
                    this.ParentVm.Load();
                    this.Status.Info(Messages.Msg_AbsenceDeleted);
                }
            }
        }

        #endregion Methods
    }
}