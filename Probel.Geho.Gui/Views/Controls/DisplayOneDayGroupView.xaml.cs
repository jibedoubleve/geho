namespace Probel.Geho.Gui.Views.Controls
{
    using System.Windows.Controls;

    using ViewModels;

    /// <summary>
    /// Interaction logic for DisplayOneDayGrouView.xaml
    /// </summary>
    public partial class DisplayOneDayGroupView : UserControl
    {
        #region Constructors

        public DisplayOneDayGroupView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(this.DataContext is ILoadeableViewModel)
            {
                var vm = this.DataContext as ILoadeableViewModel;
                //vm.Load();
            }
        }

        #endregion Methods
    }
}