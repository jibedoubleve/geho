namespace Probel.Geho.Gui.Tools
{
    using System;
    using System.Collections.Generic;

    public static class LoopExtensions
    {
        #region Methods

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        #endregion Methods
    }
}