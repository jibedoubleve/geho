namespace Probel.Geho.Gui.Views.PrintDocument
{
    using System.Windows.Controls;

    using ViewModels.PrintDocument;

    /// <summary>
    /// Interaction logic for PrintDay.xaml
    /// </summary>
    public partial class PrintDayView : UserControl
    {
        #region Constructors

        public PrintDayView(PrintDayViewModel viewmodel)
            : this()
        {
            this.DataContext = viewmodel;
        }

        public PrintDayView()
        {
            InitializeComponent();
        }

        #endregion Constructors
    }
}