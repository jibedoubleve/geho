﻿using Probel.Geho.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probel.Geho.Data.Helpers
{
    /// <summary>
    /// Indicates whether the Moment in day is the morning or afternoon
    /// </summary>
    /// <remarks>
    /// http://www.alanzucconi.com/2015/07/26/enum-flags-and-bitwise-operators/
    /// </remarks>
    public static class MomentDayExtension
    {
        public static bool IsMorning(this MomentDay mid)
        {
            return (mid & MomentDay.Morning) != 0;
        }
    }
}
