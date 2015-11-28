namespace Probel.Geho.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class StringExtension
    {
        #region Methods

        public static string CapitaliseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s)) { return string.Empty; }
            else { return char.ToUpper(s[0]) + s.Substring(1).ToLower(); }
        }

        #endregion Methods
    }
}