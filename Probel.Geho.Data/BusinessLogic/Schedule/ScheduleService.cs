namespace Probel.Geho.Data.BusinessLogic.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Probel.Geho.Data.Business_Logic.Schedule;
    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Entities;
    using Probel.Geho.Data.Helpers;

    public class ScheduleService : IScheduleService
    {
        #region Methods

        public void CreateWeek(DateTime dateInWeek)
        {
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
                                        .Include(e => e.Days.Select(f => f.Persons))
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
                        .Include(e => e.Days.Select(f => f.Persons))
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

        public IEnumerable<PersonDto> GetFreeEducators(DateTime date)
        {
            using (var db = new DataContext())
            {
                date = date.Date;
                var educators = (from e in db.People
                                 where e.IsEducator
                                 && e.Activities.Where(f => f.DayOfWeek == date.DayOfWeek).Count() == 0
                                 && e.Days.Where(f => f.Date == date).Count() == 0
                                 && e.Absences.Where(f => f.Start <= date && date <= f.End).Count() == 0
                                 select e);
                return educators.ToDto();
            }
        }

        public GroupDto GetGroup(int id)
        {
            using (var db = new DataContext())
            {
                var entity = (from g in db.Groups
                                          .Include(e => e.Persons)
                              where g.Id == id
                              select g).Single();
                return Mapper.Map<Group, GroupDto>(entity);
            }
        }

        public WeekDto GetWeek(DateTime dateInWeek)
        {
            using (var db = new DataContext())
            {
                var entity = GetWeekEntity(db, dateInWeek);
                return Mapper.Map<Week, WeekDto>(entity);
            }
        }

        public WeekDto GetWeek(int id)
        {
            using (var db = new DataContext())
            {
                var entity = (from w in db.Weeks.Include(e => e.Days)
                                          .Include(e => e.Days.Select(f => f.Persons))
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
                                            .Include(e => e.Days.Select(f => f.Persons))
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
                                      .Include(e => e.Days.Select(f => f.Persons))
                                      .Include(e => e.Days.Select(f => f.Group))
                    where w.Monday == monday
                    select w).FirstOrDefault();
        }

        #endregion Methods
    }
}