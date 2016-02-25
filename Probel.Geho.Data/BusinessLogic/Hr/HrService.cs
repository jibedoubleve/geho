namespace Probel.Geho.Services.BusinessLogic.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Helpers;

    using Probel.Geho.Services.BusinessLogic.BusinessRules;
    using Probel.Geho.Services.Database;
    using Probel.Geho.Services.Dto;
    using Probel.Geho.Services.Entities;

    using Schedule;

    public class HrService : ServiceBase, IHrService
    {
        #region Methods

        public void CreateAbsence(PersonDto beneficiary, DateTime start, DateTime end, bool isPresent = false)
        {
            this.CreateAbsence(beneficiary.Id, start, end, isPresent);
        }

        public void CreateAbsence(int personId, DateTime start, DateTime end, bool isPresent = false)
        {
            using (var db = new DataContext())
            {
                var person = (from b in db.People
                              where b.Id == personId
                              select b).FirstOrDefault();
                if (person == null) { throw new EntityNotFountException(); }

                var absence = new Absence()
                {
                    Person = person,
                    End = end,
                    Start = start,
                    IsPresent = isPresent,
                };

                new AbsenceAdapter(absence)
                    .Validate()
                    .ClearOccupations(db);

                db.Absences.Add(absence);
                db.SaveChanges();
            }
        }

        public void CreateAbsence(AbsenceDto absence)
        {
            if (absence == null) { throw new ArgumentNullException("absence"); }
            else if (absence.Person == null) { throw new ArgumentNullException("absence.Person"); }
            else if (absence.Start == null) { throw new ArgumentNullException("absence.Start"); }
            else if (absence.End == null) { throw new ArgumentNullException("absence.End"); }

            this.CreateAbsence(absence.Person.Id, absence.Start, absence.End, absence.IsPresent);
        }

        public void CreateActivity(DayOfWeek day
            , MomentDay momentDay
            , IEnumerable<PersonDto> people
            , string name)
        {
            if (people == null) { people = new List<PersonDto>(); }

            using (var db = new DataContext())
            {
                var eList = new List<Person>();
                foreach (var e in people) { eList.Add(db.People.Attach(e.ToEntity())); }

                var activity = new Activity()
                {
                    People = eList,
                    MomentDay = momentDay,
                    DayOfWeek = day,
                    Name = name,
                };
                db.Activities.Add(activity);
                db.SaveChanges();
            }
        }

        public void CreateActivity(ActivityDto activity)
        {
            this.CreateActivity(activity.DayOfWeek, activity.MomentDay, activity.People, activity.Name);
        }

        public void CreateGroup(GroupDto group)
        {
            group.Name = group.Name.ToLower();
            using (var db = new DataContext())
            {
                var exist = (from g in db.Groups
                             where g.Name.ToLower() == @group.Name
                             select g).Count() > 0;
                if (exist) { throw new EntityAlreadyExistException(); }

                db.Groups.Add(group.ToEntity());
                db.SaveChanges();
            }
        }

        public void CreatePerson(PersonDto person)
        {
            if (person == null) { throw new ArgumentNullException("person"); }

            using (var db = new DataContext())
            {
                if (person.Surname == null) { person.Surname = string.Empty; }

                var entity = person.ToEntity();
                db.People.Add(entity);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieve all absences
        /// </summary>
        /// <returns>A list of absences</returns>
        /// <remarks>http://www.entityframeworktutorial.net/EntityFramework4.3/eager-loading-with-dbcontext.aspx</remarks>
        public IEnumerable<AbsenceDto> GetAbsences(bool isPresent = false)
        {
            using (var db = new DataContext())
            {
                var entities = (from a in db.Absences
                                            .Include(e => e.Person)
                                where a.IsPresent == isPresent
                                select a).ToList();

                var result = Mapper.Map<IEnumerable<Absence>, IEnumerable<AbsenceDto>>(entities);
                return result;
            }
        }

        public IEnumerable<ActivityDto> GetAdministrativeActivities()
        {
            using (var db = new DataContext())
            {
                var r = (from a in db.Activities
                                     .Include(e => e.People)
                         where a.People.Where(p => !p.IsEducator)
                                       .Count() == 0
                         select a).OrderBy(e => e.DayOfWeek)
                                  .ThenBy(e => e.MomentDay)
                                  .ThenBy(e => e.Name)

                                 .ToDto();
                return r;
            }
        }

        public IEnumerable<PersonDto> GetBeneficiaries(string name, string surname = null)
        {
            NormaliseNameAndSurname(ref name, ref surname);

            using (var db = new DataContext())
            {
                var result = (from e in db.People
                              where e.Surname.ToLower().Contains(surname)
                              && e.Name.ToLower().Contains(name)
                              && e.IsEducator == false
                              select e).ToList();
                return result.ToDto();
            }
        }

        public IEnumerable<PersonDto> GetBeneficiaries()
        {
            using (var db = new DataContext())
            {
                return (from b in db.People
                        where !b.IsEducator
                        orderby b.Name, b.Surname
                        select b).ToDto();
            }
        }

        /// <summary>
        /// Get free beneficiaries for the specified day and part of day. It does not take into account
        /// neither absences nor week schedule
        /// </summary>
        /// <param name="dayOfWeek">The day of week</param>
        /// <param name="isMorning"><c>True</c> for the morning. Otherwise, it is set for the afternoon</param>
        /// <returns>The free beneficiaries</returns>
        public IEnumerable<PersonDto> GetBeneficiariesWithoutActivities(DayOfWeek dayOfWeek, bool isMorning)
        {
            var md = isMorning ? MomentDay.Morning : MomentDay.Afternoon;
            using (var db = new DataContext())
            {
                var people = (from p in db.People.Include(e => e.Activities)
                              where !p.IsEducator
                              && p.Activities.Where(e => e.DayOfWeek == dayOfWeek
                                                     && (e.MomentDay & md) != 0
                                                     && e.IsActive).Count() == 0
                              select p).ToList();
                return people.ToDto();
            }
        }

        public IEnumerable<PersonDto> GetBeneficiariesWithoutGroup()
        {
            using (var db = new DataContext())
            {
                var free = (from b in db.People
                            where !b.IsEducator
                               && b.Group == null
                            select b);
                return free.ToDto();
            }
        }

        public PersonDto GetBeneficiary(int id)
        {
            using (var db = new DataContext())
            {
                var entity = db.People.Find(id);
                return entity.ToDto();
            }
        }

        public IEnumerable<PersonDto> GetEducators(string name, string surname = null)
        {
            NormaliseNameAndSurname(ref name, ref surname);

            using (var db = new DataContext())
            {
                var result = (from e in db.People
                              where e.Surname.ToLower().Contains(surname)
                              && e.Name.ToLower().Contains(name)
                              && e.IsEducator == true
                              select e).ToList();
                return result.ToDto();
            }
        }

        public IEnumerable<PersonDto> GetEducators()
        {
            using (var db = new DataContext())
            {
                var entity = (from p in db.People
                                          .Include(e => e.Absences)
                                          .Include(e => e.Activities)
                                          .Include(e => e.Days)
                                          .Include(e => e.Days.Select(f => f.Group))
                              where p.IsEducator
                              orderby p.Name, p.Surname
                              select p).ToList();
                return entity.ToDto();
            }
        }

        /// <summary>
        /// Get free educators for the specified day and part of day. It does not take into account
        /// neither absences nor week schedule
        /// </summary>
        /// <param name="dayOfWeek">The day of week</param>
        /// <param name="isMorning"><c>True</c> for the morning. Otherwise, it is set for the afternoon</param>
        /// <returns>The free educators</returns>
        public IEnumerable<PersonDto> GetEducatorWithoutActivities(DayOfWeek dayOfWeek, bool isMorning)
        {
            var md = isMorning ? MomentDay.Morning : MomentDay.Afternoon;
            using (var db = new DataContext())
            {
                var people = (from p in db.People.Include(e => e.Activities)
                              where p.IsEducator
                                 && p.Activities.Where(e => e.DayOfWeek == dayOfWeek
                                                        && (e.MomentDay & md) != 0
                                                        && e.IsActive).Count() == 0
                              select p).ToList();
                return people.ToDto();
            }
        }

        public GroupDto GetGroup(string name)
        {
            name = name.ToLower();
            using (var db = new DataContext())
            {
                var result = (from g in db.Groups
                              where g.Name.ToLower() == name
                              select g).FirstOrDefault();

                if (result == null) { return null; }

                db.Entry(result).Collection(e => e.People).Load();

                if (result == null) { throw new EntityNotFountException(); }
                else { return Mapper.Map<Group, GroupDto>(result); }
            }
        }

        public GroupDto GetGroup(int id)
        {
            using (var db = new DataContext())
            {
                return (from g in db.Groups
                        where g.Id == id
                        select g).Single()
                                 .ToDto();
            }
        }

        public IEnumerable<GroupDto> GetGroups()
        {
            using (var db = new DataContext())
            {
                var entities = (from g in db.Groups
                                            .Include(e => e.People)
                                orderby g.Order
                                select g).ToList();
                return Mapper.Map<IEnumerable<Group>, IEnumerable<GroupDto>>(entities);
            }
        }

        public PersonDto GetPerson(int id)
        {
            using (var db = new DataContext())
            {
                var entity = db.People.Find(id);
                return entity.ToDto();
            }
        }

        public IEnumerable<PersonDto> GetPersons()
        {
            using (var db = new DataContext())
            {
                var people = (from p in db.People
                                     .Include(e => e.Absences)
                                     .Include(e => e.Activities)
                              select p).ToList();
                return people.ToDto();
            }
        }

        public IEnumerable<PersonDto> GetTrainees()
        {
            using (var db = new DataContext())
            {
                return (from t in db.People
                        where t.IsTrainee
                        select t).ToDto();
            }
        }

        public ValidationStatusDto IsAbsenceValid(AbsenceDto absence)
        {
            var validator = new AbsenceValidator(absence.ToEntity());
            return validator.Validate()
                ? ValidationStatusDto.Valid()
                : ValidationStatusDto.Invalid(validator.Error);
        }

        public ValidationStatusDto IsValidAbsence(int personId, DateTime start, DateTime end)
        {
            using (var db = new DataContext())
            {
                var person = (from b in db.People
                              where b.Id == personId
                              select b).FirstOrDefault();
                if (person == null) { throw new EntityNotFountException(); }

                var absence = new Absence()
                {
                    Person = person,
                    End = end,
                    Start = start,
                };

                var validator = new AbsenceValidator(absence);
                return validator.Validate()
                    ? ValidationStatusDto.Valid()
                    : ValidationStatusDto.Invalid(validator.Error);

            }
        }

        public void RemoveAbsence(int id)
        {
            using (var db = new DataContext())
            {
                var entity = db.Absences.Find(id);
                if (entity != null)
                {
                    db.Absences.Remove(entity);
                    db.SaveChanges();
                }
            }
        }

        public void RemoveAbsence(AbsenceDto absence)
        {
            if (absence == null) { throw new ArgumentNullException("absence"); }

            this.RemoveAbsence(absence.Id);
        }

        public void RemoveActivity(ActivityDto activity)
        {
            this.RemoveActivity(activity.Id);
        }

        public void RemoveActivity(int id)
        {
            using (var db = new DataContext())
            {
                var activity = (from a in db.Activities
                                where a.Id == id
                                select a).Single();

                if (activity != null)
                {
                    new ActivityAdapter(activity).ClearForeignKeys(db);
                    db.Activities.Remove(activity);
                    db.SaveChanges();
                }
            }
        }

        public void RemoveGroup(int id)
        {
            using (var db = new DataContext())
            {
                var entity = (from g in db.Groups
                                          .Include(e => e.People)
                              where g.Id == id
                              select g).SingleOrDefault();

                if (entity != null)
                {
                    new GroupAdapter(entity).ClearForeignKeys(db);
                    db.Groups.Remove(entity);
                    db.SaveChanges();
                }
            }
        }

        public void RemoveGroup(GroupDto group)
        {
            this.RemoveGroup(group.Id);
        }

        public void RemovePerson(int id)
        {
            using (var db = new DataContext())
            {
                var person = db.People.Find(id);
                if (person == null) { return; }

                var groups = (from g in db.Groups
                              where g.People.Select(e => e.Id).Contains(id)
                              select g);
                foreach (var group in groups) { group.People.Remove(person); }

                var lunches = (from g in db.LunchTimes
                               where g.People.Select(e => e.Id).Contains(id)
                               select g);

                foreach (var lunch in lunches) { if (lunch.People != null) { lunch.People.Remove(person); } }

                var absences = (from a in db.Absences
                                where a.Person.Id == id
                                select a);
                db.Absences.RemoveRange(absences);

                var entity = db.People.Find(id);
                db.People.Remove(entity);
                db.SaveChanges();
            }
        }

        public void RemovePerson(PersonDto person)
        {
            if (person == null) { throw new ArgumentNullException("person"); }
            this.RemovePerson(person.Id);
        }

        public void UpdateActivity(ActivityDto activity)
        {
            using (var db = new DataContext())
            {
                var toRemove = db.Activities.Find(activity.Id);
                db.Activities.Remove(toRemove);

                var ePeople = new List<Person>();
                var toAdd = activity.ToEntity();

                foreach (var p in activity.People) { ePeople.Add(db.People.Find(p.Id)); }
                toAdd.People = ePeople;
                db.Activities.Add(toAdd);

                db.SaveChanges();
            }
        }

        public void UpdateGroup(GroupDto @group)
        {
            using (var db = new DataContext())
            {
                var eGroup = (from g in db.Groups.Include(e => e.People)
                              where g.Id == @group.Id
                              select g).FirstOrDefault();

                if (eGroup == null) { throw new EntityNotFountException(); }

                while (eGroup.People.Count() > 0) { eGroup.People.RemoveAt(0); }

                var benefs = new List<Person>();
                foreach (var benef in group.People)
                {
                    var b = db.People.Find(benef.Id);
                    eGroup.People.Add(b);
                }
                eGroup.Name = group.Name;
                eGroup.Order = group.Order;
                db.SaveChanges();
            }
        }

        public void UpdateLunch(IEnumerable<LunchTimeDto> lunches)
        {
            using (var db = new DataContext())
            {
                new UpdateEntity(db).Update(lunches);
            }
        }

        public void UpdatePerson(PersonDto person)
        {
            using (var db = new DataContext())
            {
                var e = db.People.Find(person.Id);
                e.Name = person.Name;
                e.Surname = person.Surname;
                e.IsTrainee = person.IsTrainee;
                e.IsEducator = person.IsEducator;
                db.SaveChanges();
            }
        }

        private static void NormaliseNameAndSurname(ref string name, ref string surname)
        {
            surname = (surname == null) ? string.Empty : surname.ToLower();
            name = name.ToLower() ?? string.Empty;
        }

        #endregion Methods
    }
}