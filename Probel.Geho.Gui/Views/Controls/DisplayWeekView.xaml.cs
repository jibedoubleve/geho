namespace Probel.Geho.Gui.Views.Controls
{
    using System.Windows.Controls;

    using ViewModels.Controls;

    /// <summary>
    /// Interaction logic for DisplayWeekView.xaml
    /// </summary>
    public partial class DisplayWeekView : UserControl
    {
        #region Constructors

        public DisplayWeekView(DisplayWeekViewModel vm)
            : this()
        {
            this.DataContext = vm;
        }

        public DisplayWeekView()
        {
            InitializeComponent();
        }

        #endregion Constructors
    }
}