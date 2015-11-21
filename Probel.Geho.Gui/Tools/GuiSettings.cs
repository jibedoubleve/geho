namespace Probel.Geho.Gui.Tools
{
    using System.Windows.Data;

    using Probel.Geho.Gui.Properties;

    public class GuiSettings : Binding
    {
        #region Constructors

        public GuiSettings()
        {
            Initialize();
        }

        public GuiSettings(string path)
            : base(path)
        {
            Initialize();
        }

        #endregion Constructors

        #region Methods

        private void Initialize()
        {
            this.Source = Settings.Default;
            this.Mode = BindingMode.OneWay;
        }

        #endregion Methods
    }
}