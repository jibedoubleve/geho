﻿namespace Probel.Geho.Gui.Views
{
    using System.Windows;
    using System.Windows.Controls;

    using Mvvm.DataBinding;

    using Probel.Geho.Gui.ViewModels;

    /// <summary>
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleManagerView : Page
    {
        #region Constructors

        public ScheduleManagerView(ScheduleManagerViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }

        #endregion Constructors
    }
}