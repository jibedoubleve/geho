namespace Probel.Geho.Gui.Views.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    using ViewModels.Controls;

    /// <summary>
    /// Interaction logic for PersonFlatBusyView.xaml
    /// </summary>
    public partial class PersonFlatBusyView : UserControl
    {
        #region Constructors

        public PersonFlatBusyView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void UserControl_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.cb_IsBusy.IsChecked = !this.cb_IsBusy.IsChecked;
        }

        #endregion Methods
    }
}