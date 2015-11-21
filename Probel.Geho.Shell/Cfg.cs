namespace Probel.Geho.Shell
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Cfg
    {
        #region Constructors

        static Cfg()
        {
            DataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
        }

        #endregion Constructors

        #region Properties

        public static string DataDirectory
        {
            get; private set;
        }

        #endregion Properties
    }
}