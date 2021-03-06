﻿namespace Probel.Geho.Services.Business_Logic.Schedule
{
    using Probel.Geho.Services.Entities;

    internal class DayFiller
    {
        #region Methods

        public Week Fill(Week week)
        {
            for (int i = 0; i < 5; i++)
            {
                week.Days.Add(new Day()
                {
                    Date = week.Monday.AddDays(i),
                    IsMorning = true,
                });
                week.Days.Add(new Day()
                {
                    Date = week.Monday.AddDays(i),
                    IsMorning = false,
                });
            }
            return week;
        }

        #endregion Methods
    }
}