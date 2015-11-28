namespace Probel.Geho.Data.BusinessLogic.Hr
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Probel.Geho.Data.BusinessLogic.BusinessRules;
    using Probel.Geho.Data.Database;
    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Entities;
    using Helpers;
    public class HrService : IHrService
    {
        #region Methods

        public void CreateAbsence(PersonDto beneficiary, DateTime start, DateTime end)
        {
            this.CreateAbsence(beneficiary.Id, start, end);
        }

        public void CreateAbsence(int personId, DateTime start, DateTime end)
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
                if (!validator.Validate())
                {
                    throw new BusinessRuleException(validator.Error);
                }

                db.Absences.Add(absence);
                db.SaveChanges();
            }
        }

        public void CreateAbsence(AbsenceDto absence)
        {
            if (absence == null) { throw new ArgumentNullException("absence", "Absence not set"); }
            else if (absence.Person == null) { throw new ArgumentNullException("absence.Person", "Nobody was set to the absence"); }
            else if (absence.Start == null) { throw new ArgumentNullException("absence.Start", "No start date"); }
            else if (absence.End == null) { throw new ArgumentNullException("absence.End", "No end date"); }

            this.CreateAbsence(absence.Person.Id, absence.Start, absence.End);
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

        public void CreateGroup(string name)
        {
            var lowerName = name.ToLower();
            using (var db = new DataContext())
            {
                var exist = (from g in db.Groups
                             where g.Name.ToLower() == lowerName
                             select g).Count() > 0;
                if (exist) { throw new EntityAlreadyExistException(); }

                db.Groups.Add(new Group() { Name = name });
                db.SaveChanges();
            }
        }

        public void CreateGroup(GroupDto group)
        {
            this.CreateGroup(group.Name);
        }

        public void CreatePerson(PersonDto person)
        {
            using (var db = new DataContext())
            {
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
        public IEnumerable<AbsenceDto> GetAbsences()
        {
            using (var db = new DataContext())
            {
                var entities = (from a in db.Absences
                                            .Include(e => e.Person)
                                select a).ToList();

                var result = Mapper.Map<IEnumerable<Absence>, IEnumerable<AbsenceDto>>(entities);
                return result;
            }
        }

        public IEnumerable<ActivityDto> GetActivities()
        {
            using (var db = new DataContext())
            {
                var entities = (from a in db.Activities
                                            .Include(e => e.People)
                                select a)
                                .OrderBy(e => e.DayOfWeek)
                                .ToList();
                return Mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityDto>>(entities);
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
                              && p.Activities.Where(e => e.DayOfWeek == dayOfWeek && (e.MomentDay & md) != 0).Count() == 0
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
                var entity = db.People
                               .Include(e => e.Absences)
                               .Include(e => e.Activities)
                               .Include(e => e.Days)
                               .Include(e => e.Days.Select(f => f.Group))
                               .ToList();
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
                                 && p.Activities.Where(e => e.DayOfWeek == dayOfWeek && (e.MomentDay & md) != 0).Count() == 0
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
            if (absence == null) { throw new ArgumentNullException("absence", "Absence is null"); }

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
            if (person == null) { throw new ArgumentNullException("Person is not set", "person"); }
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
                db.SaveChanges();
            }
        }

        private static void NormaliseNameAndSurname(ref string name, ref string surname)
        {
            surname = (surname == null) ? string.Empty : surname.ToLower();
            name = name.ToLower() ?? string.Empty;
        }

        public void UpdatePerson(PersonDto person)
        {
            using (var db = new DataContext())
            {
                var e = db.People.Find(person.Id);
                e.Name = person.Name;
                e.Surname = person.Surname;
                db.SaveChanges();
            }
        }

        #endregion Methods
    }
}