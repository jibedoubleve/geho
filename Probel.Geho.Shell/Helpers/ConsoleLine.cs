namespace Probel.Geho.Shell.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConsoleJumper
    {
        #region Fields

        private readonly int JumpOffset;

        #endregion Fields

        #region Constructors

        public ConsoleJumper(int jumpOffset)
        {
            this.JumpOffset = jumpOffset;
            this.LastFix = Console.CursorTop;
        }

        #endregion Constructors

        #region Properties

        private int LastFix
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public void Fix()
        {
            this.LastFix = Console.CursorTop;
        }

        public void Jump()
        {
            Console.CursorTop = LastFix + JumpOffset;
        }

        #endregion Methods
    }
}