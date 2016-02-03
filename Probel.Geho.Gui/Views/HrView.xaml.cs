namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Gui.ViewModels;

    /// <summary>
    /// Interaction logic for HrManagerxaml.xaml
    /// </summary>
    public partial class HrView : Page
    {
        #region Constructors

        public HrView(HrViewModel vm)
        {
            if (vm == null) { throw new ArgumentNullException("vm"); }
            InitializeComponent();
            this.DataContext = vm;
        }

        #endregion Constructors

        #region Methods

        private void cb_IsEducator_Checked(object sender, RoutedEventArgs e)
        {
            if (cb_IsEducator.IsChecked == false)
            {
                this.cb_IsTrainee.IsChecked = false;
            }
        }

        private void Click_CloselAddPersonnel(object sender, RoutedEventArgs e)
        {
            addPersonnelPopup.IsOpen = false;
        }

        private void Click_CloseManageAbsence(object sender, RoutedEventArgs e)
        {
            addAbsencePopup.IsOpen = false;
        }

        private void Click_ShowAddPersonnel(object sender, RoutedEventArgs e)
        {
            addPersonnelPopup.IsOpen = true;
        }

        private void Click_ShowManageAbsence(object sender, RoutedEventArgs e)
        {
            addAbsencePopup.IsOpen = true;
        }

        private void Click_ShowManageMedicalExams(object sender, RoutedEventArgs e)
        {
            this.addMedicalVisitPopup.IsOpen = true;
        }

        private void MedicalExamView_ControlClosing(object sender, EventArgs e)
        {
            this.addMedicalVisitPopup.IsOpen = false;
        }

        private void SelectionChanged_cb_End(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is HrViewModel && cb_End != null && cb_End.SelectedItem != null)
            {
                var vm = this.GetViewModel<HrViewModel>();
                vm.EndOffset = (int)((ComboBoxItem)cb_End.SelectedItem).Tag;
            }
        }

        private void SelectionChanged_cb_Start(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is HrViewModel && cb_End != null && cb_Start.SelectedItem != null)
            {
                var vm = this.GetViewModel<HrViewModel>();
                vm.StartOffset = (int)((ComboBoxItem)cb_Start.SelectedItem).Tag;
            }
        }

        #endregion Methods
    }
}