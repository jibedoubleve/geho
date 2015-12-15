namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Probel.Geho.Data.BusinessLogic.Hr;
    using Probel.Geho.Gui.ViewModels;
    using Probel.Mvvm.DataBinding;

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