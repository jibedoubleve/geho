namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using Probel.Geho.Gui.ViewModels;

    /// <summary>
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : Page
    {
        #region Constructors

        public ScheduleView(ScheduleViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }

        #endregion Constructors
    }
}