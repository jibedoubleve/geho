namespace Probel.Geho.Gui.Views
{
    using System.Windows;
    using System.Windows.Controls;

    using Probel.Geho.Gui.ViewModels;

    /// <summary>
    /// Interaction logic for ScheduleEditorView.xaml
    /// </summary>
    public partial class ScheduleEditorView : Page
    {
        #region Constructors

        public ScheduleEditorView(ScheduleEditorViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void Click_ClosePopup(object sender, RoutedEventArgs e)
        {
            addPopup.IsOpen = false;
        }

        private void Click_OpenPopup(object sender, RoutedEventArgs e)
        {
            addPopup.IsOpen = true;
        }

        #endregion Methods
    }
}