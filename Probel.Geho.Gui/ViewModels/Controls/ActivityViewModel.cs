namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Windows.Input;

    using Mvvm.Gui;
    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    public class ActivityViewModel : ActivityCardViewModel
    {
        #region Fields

        private readonly ICommand deleteActivityCommand;
        private readonly ILoadeableViewModel ParentVm;
        private readonly IHrService Service;

        private bool isSelected;

        #endregion Fields

        #region Constructors

        public ActivityViewModel(ActivityDto activity, IHrService service, ILoadeableViewModel parentVm)
            : base(activity)
        {
            this.ParentVm = parentVm;
            this.Service = service;
            this.deleteActivityCommand = new RelayCommand(DeleteActivity, CanDeleteActivity);
        }

        #endregion Constructors

        #region Properties

        public ICommand DeleteActivityCommand
        {
            get { return this.deleteActivityCommand; }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged(() => IsSelected);
            }
        }

        #endregion Properties

        #region Methods

        private bool CanDeleteActivity()
        {
            return true;
        }

        private void DeleteActivity()
        {
            var yes = Notifyer.Question(string.Format(Messages.Msg_AskDeleteActivity, this.Name));
            if (yes)
            {
                using (WaitingCursor.While)
                {
                    this.Service.RemoveActivity(this.Activity);
                    this.ParentVm.Load();
                    this.Status.InfoFormat(Messages.Msg_ActivityDeleted, this.Activity.Name);
                }
            }
        }

        #endregion Methods
    }
}