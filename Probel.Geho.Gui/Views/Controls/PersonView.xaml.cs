namespace Probel.Geho.Gui.Views.Controls
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

    /// <summary>
    /// Interaction logic for UserCardView.xaml
    /// </summary>
    public partial class PersonView : UserControl
    {
        #region Constructors

        public PersonView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            this.editPopup.IsOpen = true;
        }

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.editPopup.IsOpen = false;
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            this.editPopup.IsOpen = false;
        }
    }
}