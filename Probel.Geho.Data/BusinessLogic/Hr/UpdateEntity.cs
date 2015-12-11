namespace Probel.Geho.Data.BusinessLogic.Hr
{
    using System.Collections.Generic;
    using System.Linq;

    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Entities;

    internal class UpdateEntity
    {
        #region Fields

        private readonly DataContext db;

        #endregion Fields

        #region Constructors

        public UpdateEntity(DataContext db)
        {
            this.db = db;
        }

        #endregion Constructors

        #region Methods

        public void Update(IEnumerable<LunchTimeDto> lunches)
        {
            foreach (var lunch in lunches)
            {
                var l = db.LunchTimes.Where(e => e.DayOfWeek == lunch.DayOfWeek)
                                     .SingleOrDefault();

                var people = Attach(lunch, db);

                if (l == null) { CreateLunch(lunch, people); }
                else
                {
                    db.LunchTimes.Remove(l);
                    this.CreateLunch(lunch, people);
                }
            }
            db.SaveChanges();
        }

        private void CreateLunch(LunchTimeDto lunch, IList<Person> people)
        {
            var entity = new LunchTime()
            {
                People = people,
                DayOfWeek = lunch.DayOfWeek,
            };
            db.LunchTimes.Add(entity);
            db.SaveChanges();
        }

        private IList<Person> Attach(IEnumerable<PersonDto> people, DataContext db)
        {
            var result = new List<Person>();
            foreach (var p in people) { result.Add(db.People.Find(p.Id)); }

            return result;
        }

        private IList<Person> Attach(LunchTimeDto lunch, DataContext db)
        {
            return Attach(lunch.People, db);
        }

        #endregion Methods
    }
}