namespace Probel.Geho.Gui.Views.PrintDocument
{
    using System.Windows.Controls;

    using ViewModels.Controls;
    using ViewModels.PrintDocument;

    /// <summary>
    /// Interaction logic for PrintLunchesView.xaml
    /// </summary>
    public partial class PrintLunchesView : UserControl
    {
        #region Constructors

        public PrintLunchesView(DisplayLunchViewModel vm)
        {
            this.DataContext = new PrintLunchesViewModel(vm);
            this.InitializeComponent();
        }

        #endregion Constructors
    }
}