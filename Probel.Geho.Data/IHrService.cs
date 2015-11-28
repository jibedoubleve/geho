namespace Probel.Geho.Data.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Entities;

    using Probel.Geho.Data.Dto;

    public interface IHrService
    {
        #region Methods

        void CreateAbsence(PersonDto person, DateTime start, DateTime end);

        void CreateAbsence(int personId, DateTime start, DateTime end);

        void CreateAbsence(AbsenceDto absenceDto);

        void CreateActivity(DayOfWeek day
            , MomentDay momentDay
            , IEnumerable<PersonDto> people
            , string name);

        void CreateActivity(ActivityDto activity);

        void CreateGroup(string name);

        void CreateGroup(GroupDto group);

        void CreatePerson(PersonDto person);

        IEnumerable<AbsenceDto> GetAbsences();

        IEnumerable<ActivityDto> GetActivities();

        IEnumerable<PersonDto> GetBeneficiaries(string name, string surname = null);

        IEnumerable<PersonDto> GetBeneficiaries();

        IEnumerable<PersonDto> GetBeneficiariesWithoutActivities(DayOfWeek dayOfWeek, bool isMorning);

        IEnumerable<PersonDto> GetBeneficiariesWithoutGroup();

        PersonDto GetBeneficiary(int id);
        void UpdatePerson(PersonDto person);
        IEnumerable<PersonDto> GetEducators(string name, string surname = null);

        IEnumerable<PersonDto> GetEducators();

        IEnumerable<PersonDto> GetEducatorWithoutActivities(DayOfWeek dayOfWeek, bool isMorning);

        GroupDto GetGroup(string name);

        GroupDto GetGroup(int id);

        IEnumerable<GroupDto> GetGroups();

        PersonDto GetPerson(int id);

        IEnumerable<PersonDto> GetPersons();

        ValidationStatusDto IsAbsenceValid(AbsenceDto absence);

        void RemoveAbsence(int id);

        void RemoveAbsence(AbsenceDto Absence);

        void RemoveActivity(ActivityDto activity);

        void RemoveActivity(int id);

        void RemoveGroup(int id);

        void RemoveGroup(GroupDto group);

        void RemovePerson(PersonDto person);

        void RemovePerson(int id);

        void UpdateActivity(ActivityDto activity);

        void UpdateGroup(GroupDto group);

        #endregion Methods
    }
}