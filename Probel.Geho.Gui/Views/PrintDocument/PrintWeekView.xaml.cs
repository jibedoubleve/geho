namespace Probel.Geho.Gui.Views.PrintDocument
{
    using System.Windows.Controls;

    using ViewModels.Controls;

    /// <summary>
    /// Interaction logic for PrintWeekView.xaml
    /// </summary>
    public partial class PrintWeekView : UserControl
    {
        #region Constructors

        public PrintWeekView(DisplayWeekViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }

        #endregion Constructors
    }
}