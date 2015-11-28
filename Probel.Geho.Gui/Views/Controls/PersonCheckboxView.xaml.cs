namespace Probel.Geho.Gui.Views.Controls
{
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for PersonCheckboxView.xaml
    /// </summary>
    public partial class PersonCheckboxView : UserControl
    {
        #region Constructors

        public PersonCheckboxView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            cb_IsChecked.IsChecked = !cb_IsChecked.IsChecked;
        }

        #endregion Methods
    }
}