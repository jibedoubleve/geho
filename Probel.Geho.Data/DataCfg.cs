namespace Probel.Geho.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class DataCfg
    {
        #region Constructors

        static DataCfg()
        {
            var value = ConfigurationManager.AppSettings["EraseDatabase"];
            var parsed = false;

            bool.TryParse(value, out parsed);

            EraseDatabase = parsed;
        }

        #endregion Constructors

        #region Properties

        public static bool EraseDatabase
        {
            get; set;
        }

        #endregion Properties
    }
}