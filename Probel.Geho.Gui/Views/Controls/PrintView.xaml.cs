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

    public class PrintEventArgs : EventArgs
    {
        #region Constructors

        public PrintEventArgs(PrintView printView)
        {
            this.IsActivitiesPrinted = printView.IsActivitiesPrinted;
            this.IsFridayPrinted = printView.IsFridayPrinted;
            this.IsLunchPrinted = printView.IsLunchPrinted;
            this.IsMondayPrinted = printView.IsMondayPrinted;
            this.IsThursdayPrinted = printView.IsThursdayPrinted;
            this.IsTuesdayPrinted = printView.IsTuesdayPrinted;
            this.IsWednesdayPrinted = printView.IsWednesdayPrinted;
            this.IsWeekPrinted = printView.IsWeekPrinted;
        }

        #endregion Constructors

        #region Properties

        public bool IsActivitiesPrinted
        {
            get; private set;
        }

        public bool IsFridayPrinted
        {
            get; private set;
        }

        public bool IsLunchPrinted
        {
            get; private set;
        }

        public bool IsMondayPrinted
        {
            get; private set;
        }

        public bool IsThursdayPrinted
        {
            get; private set;
        }

        public bool IsTuesdayPrinted
        {
            get; private set;
        }

        public bool IsWednesdayPrinted
        {
            get; private set;
        }

        public bool IsWeekPrinted
        {
            get; private set;
        }

        #endregion Properties
    }

    /// <summary>
    /// Interaction logic for PrintView.xaml
    /// </summary>
    public partial class PrintView : UserControl
    {
        #region Constructors

        public PrintView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Events

        public event EventHandler Cancelled;

        public event EventHandler Printing;

        #endregion Events

        #region Properties

        public bool IsActivitiesPrinted
        {
            get
            {
                return this.cb_Activities.IsChecked.HasValue
                    && this.cb_Activities.IsChecked.Value;
            }
        }

        public bool IsFridayPrinted
        {
            get
            {
                return this.cb_Friday.IsChecked.HasValue
                    && this.cb_Friday.IsChecked.Value;
            }
        }

        public bool IsLunchPrinted
        {
            get
            {
                return this.cb_LunchTime.IsChecked.HasValue
                    && this.cb_LunchTime.IsChecked.Value;
            }
        }

        public bool IsMondayPrinted
        {
            get
            {
                return this.cb_Monday.IsChecked.HasValue
                    && this.cb_Monday.IsChecked.Value;
            }
        }

        public bool IsThursdayPrinted
        {
            get
            {
                return this.cb_Thursday.IsChecked.HasValue
                    && this.cb_Thursday.IsChecked.Value;
            }
        }

        public bool IsTuesdayPrinted
        {
            get
            {
                return this.cb_Tuesday.IsChecked.HasValue
                    && this.cb_Tuesday.IsChecked.Value;
            }
        }

        public bool IsWednesdayPrinted
        {
            get
            {
                return this.cb_Wednesday.IsChecked.HasValue
                    && this.cb_Wednesday.IsChecked.Value;
            }
        }

        public bool IsWeekPrinted
        {
            get
            {
                return this.cb_Week.IsChecked.HasValue
                    && this.cb_Week.IsChecked.Value;
            }
        }

        #endregion Properties

        #region Methods

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.OnCancelled();
        }

        private void Click_Printed(object sender, RoutedEventArgs e)
        {
            this.OnPrinting();
        }

        private void OnCancelled()
        {
            if (this.Cancelled != null) { Cancelled(this, EventArgs.Empty); }
        }

        private void OnPrinting()
        {
            if (this.Printing != null) { Printing(this, new PrintEventArgs(this)); }
        }

        #endregion Methods
    }
}