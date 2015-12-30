namespace Probel.Geho.Services.BusinessLogic.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Probel.Geho.Services.Business_Logic.Schedule;
    using Probel.Geho.Services.Database;
    using Probel.Geho.Services.Dto;
    using Probel.Geho.Services.Entities;
    using Probel.Geho.Services.Helpers;

    public class ScheduleService : ServiceBase, IScheduleService
    {
        #region Methods

        public void CreateWeek(DateTime dateInWeek)
        {
            if (WeekExists(dateInWeek)) { throw new EntityAlreadyExistException("This week is already in the database."); }

            var date = dateInWeek.GetMonday();
            using (var db = new DataContext())
            {
                var week = new Week() { Monday = date };
                db.Weeks.Add(week);
                db.SaveChanges();
            }
        }

        public void FeedDay(DateTime date, IEnumerable<PersonDto> educators, GroupDto group, bool isMorning)
        {
            var monday = date.GetMonday();
            using (var db = new DataContext())
            {
                var week = (from w in db.Weeks
                                        .Include(e => e.Days)
                                        .Include(e => e.Days.Select(f => f.People))
                                        .Include(e => e.Days.Select(f => f.Group))
                            where w.Monday == monday
                            select w).SingleOrDefault();
                if (week == null)
                {
                    CreateWeek(date);
                    FeedDay(date, educators, group, isMorning);
                    return;
                }

                var day = (from d in week.Days
                           where d.Date == date
                              && d.IsMorning == isMorning
                              && d.Group.Id == @group.Id
                           select d).SingleOrDefault();

                var dayManager = new DayManager(db, day, week, date);
                if (day == null) { dayManager.CreateEducatorForNewDay(group, educators, isMorning); }
                else { dayManager.UpdateEducatorForDay(educators, isMorning); }

                db.SaveChanges();
            }
        }

        public void FeedDay(DateTime date, GroupDto group, bool isMorning)
        {
            var monday = date.GetMonday();
            using (var db = new DataContext())
            {
                var week = (from w in db.Weeks
                        .Include(e => e.Days)
                        .Include(e => e.Days.Select(f => f.People))
                        .Include(e => e.Days.Select(f => f.Group))
                            where w.Monday == monday
                            select w).Single();

                var day = (from d in week.Days
                           where d.Date == date
                              && d.IsMorning == isMorning
                           select d).SingleOrDefault();

                var dayManager = new DayManager(db, day, week, date);
                if (day == null) { dayManager.CreateGroupForNewDay(group, isMorning); }
                else { dayManager.UpdateGroupForDay(group, isMorning); }

                db.SaveChanges();
            }
        }

        public IEnumerable<PersonDto> GetEducatorsBusyInDay(DateTime currentDay, bool isMorning)
        {
            using (var db = new DataContext())
            {
                var freeEducators = (from educ in db.People
                                                    .Include(e => e.Days.Select(g => g.Group))
                                     where educ.Days.Where(e => e.Date == currentDay.Date && e.IsMorning == isMorning).Count() > 0
                                        && educ.IsEducator
                                     select educ);
                var result = freeEducators.ToDto();

                foreach (var r in result)
                {
                    if (r.Days.Count() > 0)
                    {
                        var gn = (from d in r.Days
                                  where d.Date.Date == currentDay && d.IsMorning == isMorning
                                  select d.Group.Name);
                        if (gn.Count() > 0) { r.GroupNames = gn.Aggregate((e, f) => e + " - " + f); }
                    }
                    else { r.GroupNames = string.Empty; }
                }

                return result;
            }
        }

        public IEnumerable<PersonDto> GetFreeBeneficiaries(GroupDto group, DateTime currentDay, bool isMorning)
        {
            var md = isMorning ? MomentDay.Morning : MomentDay.Afternoon;
            var d = (isMorning)
                ? new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, 8, 1, 0)
                : new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, 12, 1, 0);

            using (var db = new DataContext())
            {
                var people = (from g in db.Groups
                                     .Include(e => e.People)
                                     .Include(e => e.People.Select(f => f.Absences))
                              where g.Id == @group.Id
                              select g.People.Where(p => !p.IsEducator
                                                       && p.Absences.Where(f => f.Start <= d && d <= f.End && !f.IsPresent)
                                                                    .Count() == 0
                                                       && p.Activities.Where(a => (a.DayOfWeek == d.DayOfWeek && (a.MomentDay & md) != 0)
                                                                               && (
                                                                                     db.People.Where(pe => pe.IsEducator
                                                                                                  && pe.Activities.Contains(a)
                                                                                                  && pe.Absences.Where(ab => ab.Start <= d && d <= ab.End && !ab.IsPresent).Count() != 0)
                                                                                              .Count()
                                                                                     != db.People.Where(pe => pe.IsEducator && pe.Activities.Contains(a)).Count()
                                                                                   )).Count() == 0))
                                        .SingleOrDefault();

                var dtoList = (people != null)
                   ? people.ToDto()
                   : new List<PersonDto>();

                /* Person is present if it has an "present absence" otherwise, he's absent */
                if (people != null)
                {
                    foreach (var dto in dtoList)
                    {
                        dto.IsPresent = (from a in db.Absences
                                         where a.Person.Id == dto.Id
                                            && a.Start <= d && d <= a.End
                                            && a.IsPresent
                                         select a).Count() > 0;
                    }
                }

                return dtoList;
            }
        }

        public IEnumerable<PersonDto> GetFreeEducators(DateTime currentDay, bool isMorning)
        {
            var md = isMorning ? MomentDay.Morning : MomentDay.Afternoon;
            var d = (isMorning)
                ? new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, 8, 1, 0)
                : new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, 12, 1, 0);

            currentDay = currentDay.Date;
            using (var db = new DataContext())
            {
                var people = (from p in db.People
                                          .Include(e => e.Activities)
                                          .Include(e => e.Absences)
                                          .Include(e => e.Days.Select(g => g.Group))
                              where p.IsEducator
                                 && p.Absences.Where(e => e.Start <= d && d <= e.End).Count() == 0
                                 && p.Activities.Where(e => e.DayOfWeek == currentDay.DayOfWeek && (e.MomentDay & md) != 0).Count() == 0
                              select p);

                var result = people.ToDto();

                return result;
            }
        }

        public GroupDto GetGroup(int id)
        {
            using (var db = new DataContext())
            {
                var entity = (from g in db.Groups
                                          .Include(e => e.People)
                              where g.Id == id
                              select g).Single();
                return Mapper.Map<Group, GroupDto>(entity);
            }
        }

        public IEnumerable<GroupDto> GetGroups()
        {
            using (var db = new DataContext())
            {
                return (from g in db.Groups
                                    .Include(e => e.People)
                                    .Include(e => e.People.Select(f => f.Absences))
                                    .Include(e => e.People.Select(f => f.Activities))
                        select g).ToDto();
            }
        }

        public IEnumerable<DateTime> GetMondays()
        {
            var thisMonday = DateTime.Today.GetMonday();
            using (var db = new DataContext())
            {
                var dates = (from w in db.Weeks
                             where w.Monday >= thisMonday
                             select w.Monday).ToList();
                return dates;
            }
        }

        public WeekDto GetWeek(DateTime dateInWeek)
        {
            using (var db = new DataContext())
            {
                var entity = GetWeekEntity(db, dateInWeek);
                return entity.ToDto();
            }
        }

        public WeekDto GetWeek(int id)
        {
            using (var db = new DataContext())
            {
                var entity = (from w in db.Weeks
                                          .Include(e => e.Days)
                                          .Include(e => e.Days.Select(f => f.People))
                                          .Include(e => e.Days.Select(f => f.Group))
                              where w.Id == id
                              select w).Single();
                return Mapper.Map<Week, WeekDto>(entity);
            }
        }

        public IEnumerable<WeekDto> GetWeeks()
        {
            using (var db = new DataContext())
            {
                var entities = (from w in db.Weeks
                                            .Include(e => e.Days)
                                            .Include(e => e.Days.Select(f => f.People))
                                            .Include(e => e.Days.Select(f => f.Group))
                                select w).OrderBy(e => e.Monday)
                                         .ToList();
                return Mapper.Map<IEnumerable<Week>, IEnumerable<WeekDto>>(entities);
            }
        }

        public bool WeekExists(DateTime date)
        {
            var monday = date.GetMonday();
            using (var db = new DataContext())
            {
                return (from w in db.Weeks
                        where w.Monday == monday
                        select w).Count() > 0;
            }
        }

        private Week GetWeekEntity(DataContext db, DateTime dateInWeek)
        {
            var monday = dateInWeek.GetMonday().Date;

            return (from w in db.Weeks.Include(e => e.Days)
                                      .Include(e => e.Days.Select(f => f.People))
                                      .Include(e => e.Days.Select(f => f.Group))
                    where w.Monday == monday
                    select w).FirstOrDefault();
        }

        #endregion Methods
    }
}