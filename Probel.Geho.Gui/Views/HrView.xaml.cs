namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Windows.Controls;

    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Gui.ViewModels;

    /// <summary>
    /// Interaction logic for HrManagerxaml.xaml
    /// </summary>
    public partial class HrView : Page
    {
        #region Constructors

        public HrView(HrViewModel vm)
        {
            if (vm == null) { throw new ArgumentNullException("ViewModel is not assigned", "vm"); }
            this.DataContext = vm;
            InitializeComponent();
        }

        #endregion Constructors
    }
}