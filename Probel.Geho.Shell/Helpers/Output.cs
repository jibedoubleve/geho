namespace Probel.Geho.Shell.Helpers
{
    using System;

    public class Output
    {
        #region Fields

        private readonly ConsoleColor Colour;
        private readonly int Left;
        private readonly int Top;

        #endregion Fields

        #region Constructors

        private Output(ConsoleColor colour, int? left = null, int? top = null)
        {
            this.Colour = colour;
            this.Left = left.HasValue
                ? left.Value
                : Console.CursorLeft;

            this.Top = (top.HasValue)
                ? top.Value
                : Console.CursorTop;
        }

        #endregion Constructors

        #region Properties

        public static Output InDarkYellow
        {
            get { return new Output(ConsoleColor.DarkYellow); }
        }

        public static Output InRed
        {
            get { return new Output(ConsoleColor.Red); }
        }

        public static Output InWhite
        {
            get { return new Output(ConsoleColor.White); }
        }

        public static Output InYellow
        {
            get { return new Output(ConsoleColor.Yellow); }
        }

        public static int MaxCursorTop
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public static Output DarkYellow(int? left = null, int? top = null)
        {
            return new Output(ConsoleColor.DarkYellow, left, top);
        }

        public static void EmptyLine(int lines = 1, int? left = null, int? top = null)
        {
            var ttop = top.HasValue ? top.Value : Console.CursorTop;
            var lleft = left.HasValue ? left.Value : Console.CursorLeft;
            Console.SetCursorPosition(lleft, ttop);
            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine();
            }
        }

        public static Output In(ConsoleColor color, int? left = null, int? top = null)
        {
            return new Output(color, left, top);
        }

        public static void JumpLines(int lines)
        {
            Console.CursorTop += lines;
        }

        public static Output Red(int? left = null, int? top = null)
        {
            return new Output(ConsoleColor.Red, left, top);
        }

        public static void ResetMaxCursorTop()
        {
            MaxCursorTop = 0;
        }

        public static Output White(int? left = null, int? top = null)
        {
            return new Output(ConsoleColor.White, left, top);
        }

        public static Output Yellow(int? left = null, int? top = null)
        {
            return new Output(ConsoleColor.Yellow, left, top);
        }

        public void Write(string format, params object[] args)
        {
            Console.SetCursorPosition(Left, Top);
            Console.ForegroundColor = this.Colour;
            Console.Write(format, args);
            Console.ResetColor();
            if (Console.CursorTop > MaxCursorTop) { MaxCursorTop = Console.CursorTop; }
        }

        public void WriteLine(object format)
        {
            Console.SetCursorPosition(Left, Top);
            Console.ForegroundColor = this.Colour;
            Console.WriteLine(format);
            Console.ResetColor();
            if (Console.CursorTop > MaxCursorTop) { MaxCursorTop = Console.CursorTop; }
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.SetCursorPosition(Left, Top);
            Console.ForegroundColor = this.Colour;
            Console.WriteLine(format, args);
            Console.ResetColor();
            if (Console.CursorTop > MaxCursorTop) { MaxCursorTop = Console.CursorTop; }
        }

        public void WriteLine()
        {
            Console.SetCursorPosition(Left, Top);
            Console.WriteLine();
            if (Console.CursorTop > MaxCursorTop) { MaxCursorTop = Console.CursorTop; }
        }

        #endregion Methods
    }
}