namespace Probel.Geho.Gui.ViewModels
{
    using System;
    using System.Threading.Tasks;

    using Controls;

    using Mvvm.Gui;

    using Probel.Mvvm.DataBinding;

    public interface ILoadeableViewModel
    {
        #region Methods

        void Load();

        Task LoadAsync();

        #endregion Methods
    }

    public abstract class LoadeableViewModel : BaseViewModel, ILoadeableViewModel
    {
        #region Methods

        public abstract void Load();

        public async Task LoadAsync()
        {
            await Task.Run(() => this.Load());
        }

        #endregion Methods
    }
}