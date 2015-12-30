namespace Probel.Geho.Gui.Views.Controls
{
    using System;
    using System.Windows.Controls;

    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Gui.ViewModels.Controls;

    using Services.Entities;

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

        private void SelectionChanged_cb_Start(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is MedicalExamViewModel && cb_Start != null && cb_Start.SelectedItem != null)
            {
                var vm = this.GetViewModel<MedicalExamViewModel>();
                var when = (MomentDay)((ComboBoxItem)cb_Start.SelectedItem).Tag;
                switch (when)
                {
                    case MomentDay.Morning:
                        vm.StartOffset = 8;
                        vm.EndOffset = 12;
                        break;
                    case MomentDay.Afternoon:
                        vm.StartOffset = 12;
                        vm.EndOffset = 18;
                        break;
                    case MomentDay.AllDay:
                        vm.StartOffset = 8;
                        vm.EndOffset = 18;
                        break;
                    default:throw new NotSupportedException("This type of enumeration is not supported.");
                }
            }
        }

        #endregion Methods
    }
}