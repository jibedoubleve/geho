namespace Probel.Geho.Data.Business_Logic.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Entities;

    class DayManager
    {
        #region Fields

        private readonly DateTime date;
        private readonly Day day;
        private readonly DataContext db;
        private readonly Week week;

        #endregion Fields

        #region Constructors

        public DayManager(DataContext db, Day day, Week week, DateTime date)
        {
            this.db = db;
            this.week = week;
            this.day = day;
            this.date = date;
        }

        #endregion Constructors

        #region Methods

        public void CreateEducatorForNewDay(GroupDto group, IEnumerable<PersonDto> educators, bool isMorning)
        {
            var newDay = new Day()
            {
                Date = date,
                IsMorning = isMorning,
                Group = db.Groups.Find(group.Id),
            };

            foreach (var e in educators)
            {
                var eeduc = db.People.Find(e.Id);
                newDay.Persons.Add(eeduc);
            }

            week.Days.Add(newDay);
        }

        public void CreateGroupForNewDay(GroupDto group, bool isMorning)
        {
            var newDay = new Day() { Date = date, IsMorning = isMorning };

            var entity = db.Groups.Find(group.Id);
            newDay.Group = entity;
        }

        public void UpdateEducatorForDay(IEnumerable<PersonDto> educators, bool isMorning)
        {
            var list = day.Persons;

            while (list.Count > 0) { list.RemoveAt(0); }
            db.SaveChanges();

            foreach (var e in educators)
            {
                var entity = db.People.Find(e.Id);
                list.Add(entity);
            }
        }

        public void UpdateGroupForDay(GroupDto group, bool isMorning)
        {
            var entity = db.Groups.Find(group.Id);
            day.Group = entity;
        }

        #endregion Methods
    }
}