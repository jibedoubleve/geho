namespace Probel.Geho.Gui.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// During the life time of this object, the mouse is showing the sandglass.
    /// As soon as this object is disposed, the mouse arrow is back to indicate
    /// the work is done.
    /// </summary>
    public class WaitingCursor : IDisposable
    {
        #region Constructors

        private WaitingCursor()
        {
            Mouse.OverrideCursor = Cursors.Wait;
        }

        #endregion Constructors

        #region Properties

        public static WaitingCursor While
        {
            get { return new WaitingCursor(); }
        }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            Mouse.OverrideCursor = null;
        }

        #endregion Methods
    }
}