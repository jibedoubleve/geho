﻿namespace Probel.Geho.Services.BusinessLogic
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Probel.Geho.Services.Database;
    using Probel.Geho.Services.Dto;

    public abstract class ServiceBase
    {
        #region Methods

        public IEnumerable<ActivityDto> GetActivities(bool includeDeactivated = false)
        {
            using (var db = new DataContext())
            {
                return (from a in db.Activities
                                    .Include(e => e.People)
                        where a.People.Where(p => !p.IsEducator).Count() > 0 //Group without educator should be ignored
                                      && ((!includeDeactivated && a.IsActive) || includeDeactivated)
                        select a).OrderBy(e => e.DayOfWeek)
                                 .ThenBy(e => e.MomentDay)
                                 .ThenBy(e => e.Name)
                                 .ToDto();
            }
        }

        public IEnumerable<LunchTimeDto> GetLunchTimes()
        {
            using (var db = new DataContext())
            {
                return (from l in db.LunchTimes
                                    .Include(e => e.People)
                        where l.People.Count > 0
                        orderby l.DayOfWeek ascending
                        select l).ToDto();
            }
        }

        #endregion Methods
    }
}