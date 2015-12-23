namespace Probel.Geho.Gui.Views.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for GroupListItemView.xaml
    /// </summary>
    public partial class GroupListItemView : UserControl
    {
        #region Fields

        public static readonly DependencyProperty IsSelectedProperty = 
            DependencyProperty.Register(
                "IsSelected",
                typeof(bool),
                typeof(GroupListItemView));

        #endregion Fields

        #region Constructors

        public GroupListItemView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }

        #endregion Properties
    }
}