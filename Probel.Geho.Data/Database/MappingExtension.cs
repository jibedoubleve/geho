namespace Probel.Geho.Data.Database
{
    using System.Collections.Generic;

    using AutoMapper;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Entities;

    internal static class MappingExtension
    {
        #region Methods

        public static Activity ToDto(this ActivityDto activity)
        {
            return Mapper.Map<ActivityDto, Activity>(activity);
        }

        public static IEnumerable<PersonDto> ToDto(this IEnumerable<Person> people)
        {
            return Mapper.Map<IEnumerable<Person>, IEnumerable<PersonDto>>(people);
        }

        public static PersonDto ToDto(this Person person)
        {
            return Mapper.Map<Person, PersonDto>(person);
        }

        public static GroupDto ToDto(this Group group)
        {
            return Mapper.Map<Group, GroupDto>(group);
        }

        public static AbsenceDto ToDto(this Absence absence)
        {
            return Mapper.Map<Absence, AbsenceDto>(absence);
        }

        public static Activity ToEntity(this ActivityDto activity)
        {
            return Mapper.Map<ActivityDto, Activity>(activity);
        }

        public static IEnumerable<Person> ToEntity(this IEnumerable<PersonDto> people)
        {
            return Mapper.Map<IEnumerable<PersonDto>, IEnumerable<Person>>(people);
        }

        public static Person ToEntity(this PersonDto person)
        {
            return Mapper.Map<PersonDto, Person>(person);
        }

        public static Group ToEntity(this GroupDto group)
        {
            return Mapper.Map<GroupDto, Group>(group);
        }

        public static Absence ToEntity(this AbsenceDto absence)
        {
            return Mapper.Map<AbsenceDto, Absence>(absence);
        }

        #endregion Methods
    }
}