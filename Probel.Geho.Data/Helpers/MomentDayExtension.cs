namespace Probel.Geho.Services.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Services.Entities;

    /// <summary>
    /// Indicates whether the Moment in day is the morning or afternoon
    /// </summary>
    /// <remarks>
    /// http://www.alanzucconi.com/2015/07/26/enum-flags-and-bitwise-operators/
    /// </remarks>
    public static class MomentDayExtension
    {
        #region Methods

        public static bool IsMorning(this MomentDay mid)
        {
            return (mid & MomentDay.Morning) != 0;
        }

        #endregion Methods
    }
}