namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System.Windows.Input;

    using Mvvm.Gui;
    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Services.BusinessLogic;
    using Probel.Geho.Services.Dto;

    public class GroupViewModel : BaseViewModel
    {
        #region Fields

        private readonly ICommand deleteGroupCommand;
        private readonly ILoadeableViewModel ParentVm;
        private readonly IHrService Service;

        private GroupDto group;
        private bool isSelected;

        #endregion Fields

        #region Constructors

        public GroupViewModel(IHrService service, ILoadeableViewModel parentVm)
        {
            this.Group = group;
            this.Service = service;
            this.ParentVm = parentVm;

            this.deleteGroupCommand = new RelayCommand(DeleteGroup, CanDeleteGroup);
        }

        #endregion Constructors

        #region Properties

        public ICommand DeleteGroupCommand
        {
            get { return this.deleteGroupCommand; }
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

        private bool CanDeleteGroup()
        {
            return this.Group != null;
        }

        private void DeleteGroup()
        {
            var yes = Notifyer.Question(
                string.Format(Messages.Msg_AskDeleteGroup, this.Group.Name));
            if (yes)
            {
                using (WaitingCursor.While)
                {
                    this.Service.RemoveGroup(this.Group);
                    this.ParentVm.Load();
                    this.Status.InfoFormat(Messages.Msg_GroupDeleted, this.Group.Name);
                }
            }
        }

        #endregion Methods
    }
}