namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Mvvm.Toolkit.DataBinding;

    using Probel.Geho.Gui.ViewModels;

    using Services.Entities;

    /// <summary>
    /// Interaction logic for GroupView.xaml
    /// </summary>
    public partial class GroupHrView : Page
    {
        #region Constructors

        public GroupHrView(GroupHrViewModel vm)
        {
            if (vm == null) { throw new ArgumentNullException("vm"); }
            InitializeComponent();
            this.DataContext = vm;
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedActivity") { this.RefreshActivityToUpdate(); }
            };
        }

        #endregion Constructors

        #region Methods

        private void RefreshActivityToUpdate()
        {
            if (this.DataContext is GroupHrViewModel)
            {
                var vm = this.GetViewModel<GroupHrViewModel>();

                //Select the day
                foreach (var item in cb_DayUpdate.Items)
                {
                    if (item is ComboBoxItem && vm.SelectedActivity != null)
                    {
                        var ci = (ComboBoxItem)item;
                        if (ci.Tag.ToString() == ((int)vm.SelectedActivity.DayOfWeek).ToString())
                        {
                            cb_DayUpdate.SelectedItem = ci;
                        }
                    }
                }
                //Select the moment
                foreach (var item in cb_NoonUpdate.Items)
                {
                    if (item is ComboBoxItem && vm.SelectedActivity != null)
                    {
                        var ci = (ComboBoxItem)item;
                        if (ci.Tag.ToString().ToLower() == ((int)vm.SelectedActivity.MomentDay).ToString().ToLower())
                        {
                            cb_NoonUpdate.SelectedItem = ci;
                        }
                    }
                }
            }
        }

        private void Selected_cb_Day(object sender, RoutedEventArgs e)
        {
            var cbi = cb_Day.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_Day.SelectedItem is ComboBoxItem)
            {
                var dow = Int32.Parse((string)cbi.Tag);
                var vm = this.GetViewModel<GroupHrViewModel>();
                if (vm.ActivityToAdd != null) { vm.ActivityToAdd.DayOfWeek = (DayOfWeek)dow; }
            }
        }

        private void Selected_cb_DayUpdate(object sender, SelectionChangedEventArgs e)
        {
            var cbi = cb_DayUpdate.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_DayUpdate.SelectedItem is ComboBoxItem)
            {
                var dow = Int32.Parse((string)cbi.Tag);
                var vm = this.GetViewModel<GroupHrViewModel>();
                if (vm.SelectedActivity != null) { vm.SelectedActivity.DayOfWeek = (DayOfWeek)dow; }
            }
        }

        private void Selected_cb_NoonUpdate(object sender, SelectionChangedEventArgs e)
        {
            var cbi = cb_NoonUpdate.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_NoonUpdate.SelectedItem is ComboBoxItem)
            {
                var value = int.Parse((string)cbi.Tag);
                var momentDay = (MomentDay)value;
                var vm = this.GetViewModel<GroupHrViewModel>();
                if (vm.SelectedActivity != null) { vm.SelectedActivity.MomentDay = momentDay; }
            }
        }

        private void Selected_cb_Noon_Selected(object sender, RoutedEventArgs e)
        {
            var cbi = cb_Noon.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_Noon.SelectedItem is ComboBoxItem)
            {
                var integer = int.Parse((string)cbi.Tag);
                var momentDay = (MomentDay)integer;
                var vm = this.GetViewModel<GroupHrViewModel>();
                if (vm.ActivityToAdd != null) { vm.ActivityToAdd.MomentDay = momentDay; }
            }
        }

        #endregion Methods
    }
}