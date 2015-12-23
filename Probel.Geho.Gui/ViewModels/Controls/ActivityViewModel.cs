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

    public class ActivityViewModel : ActivityCardViewModel
    {
        #region Fields

        private readonly ICommand deleteActivityCommand;
        private readonly ILoadeableViewModel ParentVm;
        private readonly IHrService Service;

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

        #endregion Properties

        #region Methods

        public static IEnumerable<ActivityViewModel> ToActivityViewModel(IEnumerable<ActivityDto> activities, IHrService service, ILoadeableViewModel parentVm)
        {
            var list = new List<ActivityViewModel>();
            foreach (var a in activities)
            {
                list.Add(new ActivityViewModel(a, service, parentVm));
            }
            return list;
        }

        private bool CanDeleteActivity()
        {
            return true;
        }

        private void DeleteActivity()
        {
            var yes = ViewService.MessageBox.Question(string.Format(Messages.Msg_AskDeleteActivity, this.Name));
            if (yes)
            {
                using (WaitingCursor.While)
                {
                    this.Service.RemoveActivity(this.Activity);
                    this.ParentVm.Load();
                    this.StatusBar.InfoFormat(Messages.Msg_ActivityDeleted, this.Activity.Name);
                }
            }
        }

        #endregion Methods
    }
}