namespace Probel.Geho.Gui.ViewModels.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Probel.Geho.Data.BusinessLogic;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Gui.Properties;
    using Probel.Geho.Gui.Tools;
    using Probel.Mvvm.DataBinding;
    using Probel.Mvvm.Gui;

    public class GroupViewModel : ObservableObject
    {
        #region Fields

        private readonly ICommand deleteGroupCommand;
        private readonly ILoadeableViewModel ParentVm;
        private readonly IHrService Service;

        private GroupDto group;

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

        #endregion Properties

        #region Methods

        public static IEnumerable<GroupViewModel> ToViewModels(IEnumerable<GroupDto> g, IHrService srv, ILoadeableViewModel parentVm)
        {
            var list = new List<GroupViewModel>();
            foreach (var item in g)
            {
                list.Add(new GroupViewModel(srv, parentVm) { Group = item, });
            }
            return list;
        }

        private bool CanDeleteGroup()
        {
            return this.Group != null;
        }

        private void DeleteGroup()
        {
            var yes = ViewService.MessageBox.Question(
                string.Format(Messages.Msg_AskDeleteGroup, this.Group.Name));
            if (yes)
            {
                using (WaitingCursor.While)
                {
                    this.Service.RemoveGroup(this.Group);
                    this.ParentVm.Load();
                }
            }
        }

        #endregion Methods
    }
}