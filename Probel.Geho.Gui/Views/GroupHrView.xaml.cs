namespace Probel.Geho.Gui.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using Probel.Geho.Gui.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// Interaction logic for GroupView.xaml
    /// </summary>
    public partial class GroupHrView : Page
    {
        #region Constructors

        public GroupHrView(GroupHrViewModel vm)
        {
            if (vm == null) { throw new ArgumentNullException("ViewModel is not assigned", "vm"); }
            InitializeComponent();
            this.DataContext = vm;
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "SelectedActivity") { this.RefreshActivityToUpdate(); }
            };
        }

        private void RefreshActivityToUpdate()
        {
            if (this.DataContext is GroupHrViewModel)
            {
                var vm = this.DataContext as GroupHrViewModel;

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
                        if (ci.Tag.ToString().ToLower() == ((bool)vm.SelectedActivity.IsMorning).ToString().ToLower())
                        {
                            cb_NoonUpdate.SelectedItem = ci;
                        }
                    }
                }
            }
        }

        #endregion Constructors

        #region Methods

        private void Selected_cb_Day(object sender, RoutedEventArgs e)
        {
            var cbi = cb_Day.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_Day.SelectedItem is ComboBoxItem)
            {
                var dow = Int32.Parse((string)cbi.Tag);
                var vm = this.DataContext as GroupHrViewModel;
                if (vm.ActivityToAdd != null) { vm.ActivityToAdd.DayOfWeek = (DayOfWeek)dow; }
            }
        }

        private void Selected_cb_Noon_Selected(object sender, RoutedEventArgs e)
        {
            var cbi = cb_Noon.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_Noon.SelectedItem is ComboBoxItem)
            {
                var isMorning = Boolean.Parse((string)cbi.Tag);
                var vm = this.DataContext as GroupHrViewModel;
                if (vm.ActivityToAdd != null) { vm.ActivityToAdd.IsMorning = isMorning; }
            }
        }

        #endregion Methods

        private void Selected_cb_DayUpdate(object sender, SelectionChangedEventArgs e)
        {
            var cbi = cb_DayUpdate.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_DayUpdate.SelectedItem is ComboBoxItem)
            {
                var dow = Int32.Parse((string)cbi.Tag);
                var vm = this.DataContext as GroupHrViewModel;
                if (vm.SelectedActivity != null) { vm.SelectedActivity.DayOfWeek = (DayOfWeek)dow; }
            }
        }

        private void Selected_cb_NoonUpdate(object sender, SelectionChangedEventArgs e)
        {
            var cbi = cb_NoonUpdate.SelectedItem as ComboBoxItem;
            if (this.DataContext is GroupHrViewModel && cb_NoonUpdate.SelectedItem is ComboBoxItem)
            {
                var isMorning = Boolean.Parse((string)cbi.Tag);
                var vm = this.DataContext as GroupHrViewModel;
                if (vm.SelectedActivity != null) { vm.SelectedActivity.IsMorning = (bool)isMorning; }
            }
        }
    }
}