namespace Probel.Geho.Data.BusinessLogic
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Dto;

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
                                    .Include(e => e.Person)
                        select l).ToDto();
            }
        }

        #endregion Methods
    }
}