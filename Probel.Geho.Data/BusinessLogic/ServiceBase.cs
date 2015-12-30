namespace Probel.Geho.Services.BusinessLogic
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Probel.Geho.Services.Database;
    using Probel.Geho.Services.Dto;

    public abstract class ServiceBase
    {
        #region Methods

        public IEnumerable<ActivityDto> GetActivities()
        {
            using (var db = new DataContext())
            {
                return (from a in db.Activities
                                    .Include(e => e.People)
                        where a.People.Where(p => !p.IsEducator)
                                      .Count() > 0
                        select a).OrderBy(e => e.MomentDay)
                                 .OrderBy(e => e.DayOfWeek)
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