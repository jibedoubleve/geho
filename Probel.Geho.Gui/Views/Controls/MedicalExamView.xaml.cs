namespace Probel.Geho.Gui.Views.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using Data.Dto;

    using Probel.Geho.Gui.ViewModels.Controls;
    using Probel.Mvvm.DataBinding;

    /// <summary>
    /// Interaction logic for MedicalExamView.xaml
    /// </summary>
    public partial class MedicalExamView : UserControl
    {
        #region Constructors

        public MedicalExamView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void SelectionChanged_cb_End(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is MedicalExamViewModel && cb_End != null && cb_End.SelectedItem != null)
            {
                var vm = this.GetViewModel<MedicalExamViewModel>();
                vm.EndOffset = (int)((ComboBoxItem)cb_End.SelectedItem).Tag;
            }
        }

        private void SelectionChanged_cb_Start(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is MedicalExamViewModel && cb_End != null && cb_Start.SelectedItem != null)
            {
                var vm = this.GetViewModel<MedicalExamViewModel>();
                vm.StartOffset = (int)((ComboBoxItem)cb_Start.SelectedItem).Tag;
            }
        }

        #endregion Methods
    }
}