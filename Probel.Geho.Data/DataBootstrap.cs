namespace Probel.Geho.Data
{
    using AutoMapper;

    using Probel.Geho.Data.Dto;
    using Probel.Geho.Data.Entities;

    public static class DataBootstrap
    {
        #region Methods

        public static void Initialise()
        {
            InitialiseAutoMapper();
        }

        private static void InitialiseAutoMapper()
        {
            Mapper.CreateMap<Person, PersonDto>();
            Mapper.CreateMap<PersonDto, Person>();

            Mapper.CreateMap<Group, GroupDto>();
            Mapper.CreateMap<GroupDto, Group>();

            Mapper.CreateMap<Absence, AbsenceDto>();
            Mapper.CreateMap<AbsenceDto, Absence>();

            Mapper.CreateMap<Week, WeekDto>();
            Mapper.CreateMap<WeekDto, Week>();

            Mapper.CreateMap<Day, DayDto>();
            Mapper.CreateMap<DayDto, Day>();

            Mapper.CreateMap<Activity, ActivityDto>();
            Mapper.CreateMap<ActivityDto, Activity>();

            Mapper.CreateMap<LunchTime, LunchTimeDto>();
            Mapper.CreateMap<LunchTimeDto, LunchTime>();
        }

        #endregion Methods
    }
}