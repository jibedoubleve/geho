namespace Probel.Geho.Shell.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Display
    {
        #region Methods

        public static void NoResultFor<TEntity>(ConsoleColor colour = ConsoleColor.White)
        {
            var name = typeof(TEntity).Name.Replace("Dto", "").ToLower();
            Output.In(colour).WriteLine("No {0} to display.", name);
        }

        #endregion Methods
    }
}