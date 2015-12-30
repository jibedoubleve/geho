namespace Probel.Geho.Gui.Views.PrintDocument
{
    using System.Windows.Controls;

    using ViewModels.Controls;
    using ViewModels.PrintDocument;

    /// <summary>
    /// Interaction logic for PrintActivitiesViewModel.xaml
    /// </summary>
    public partial class PrintActivitiesView : UserControl
    {
        #region Constructors

        public PrintActivitiesView(ActivityGridViewModel vm)
        {
            this.DataContext = new PrintActivitiesViewModel(vm);
            InitializeComponent();
        }

        #endregion Constructors
    }
}