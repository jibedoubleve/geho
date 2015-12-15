namespace Probel.Geho.Gui.Views.Controls
{
    using System.Windows.Controls;

    using Mvvm.DataBinding;

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
            if (this.DataContext is ILoadeableViewModel)
            {
                var vm = this.GetViewModel<ILoadeableViewModel>();
                //vm.Load();
            }
        }

        #endregion Methods
    }
}